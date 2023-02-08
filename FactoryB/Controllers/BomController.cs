using FactoryB.Models;
using KDTVN_Shared.Helper;
using KDTVN_Shared.Models;
using KDTVN_Shared.Helper;
using KDTVN_Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Data;
using System.Diagnostics;

namespace FactoryB.Controllers
{
    public class BomController : BaseController<BomController>
    {
        private readonly ILogger<BomController> _logger;

        public BomController(ILogger<BomController> logger)
        {
            _logger = logger;
        }
        BomModel bm = new BomModel();

        public IActionResult Index()
        {
            return View();
        }
//lấy dữ liệu BOM
        [HttpPost]
        public IActionResult GetBom(BomModel bom)
        {
            var result = bm.GetBomModel(bom);
            return Json(result);
        }
//Xoa du lieu Bom
        [HttpDelete]
        public IActionResult DeleteBom([FromForm] BomModel bom)
        {
            var response = new BomModel().DeleteBomModel(bom);
            return new ObjectResult(new { type = response.Code == 1 ? "success" : "error", message = response.Msg });
        }
//Update dữ liệu  bom
        [HttpPut]
        public IActionResult Updatebom(BomModel bom)
        {
            bom.status = false;
            bom.last_modify_by = SignInAccount.username;
            var response = new BomModel().UpdateBomModel(bom);
            return new ObjectResult(new { type = response.Code == 1 ? "success" : "error", message = response.Msg });
        }
//UpLoad
        [HttpPost]
        public IActionResult UploadBom(IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                    return BadRequest("File is empty");

                // ReSharper disable once PossibleNullReferenceException
                if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                    return new ObjectResult(new { type = "error", message = "File extension is not supported" });

                var memoryStream = new MemoryStream();

                //file.CopyToAsync(memoryStream);
                file.CopyTo(memoryStream);

                using var package = new ExcelPackage(memoryStream);
                ExcelWorksheet inputSheet = package.Workbook.Worksheets["Input"];

                //convert to datatable
                DataTable dt = new DataTable();

                //thêm header
                //inputSheet.Dimension.End.Column = 9
                foreach (var firstRowCell in inputSheet.Cells[2, 1, 2, 8])
                {
                    dt.Columns.Add(firstRowCell.Text);
                }

                string[] additionalColHeader = { "status", "last_modify_by", "last_modify_at" };
                foreach (var item in additionalColHeader)
                {
                    dt.Columns.Add(item);
                }

                ////thêm content
                //var currentAccount = JsonConvert.DeserializeObject<AccountExtend>(HttpContext.Session.GetString("Account"));
                // Đọc tất cả data bắt đầu từ row thứ 3

                for (var rowNumber = 3; rowNumber <= inputSheet.Dimension.End.Row; rowNumber++)
                {
                    // Lấy 1 row trong excel để truy vấn
                    var row = inputSheet.Cells[rowNumber, 1, rowNumber, 5];
                    // tạo 1 row trong data table
                    var newRow = dt.NewRow();
                    foreach (var cell in row)
                    {
                        if (cell.Start.Column < 5)
                        {
                            newRow[cell.Start.Column - 1] = cell.Text.Trim();
                        }
                        else
                        {
                            try
                            {
                                newRow[cell.Start.Column - 1] = Int32.Parse(cell.Text.Trim());
                            }
                            catch
                            {
                                newRow[cell.Start.Column - 1] = 0;
                            }

                        }

                    }

                    //status
                    newRow["status"] = true;

                    //last_modify_by
                    newRow["last_modify_by"] = SignInAccount.username;

                    //last_modify_at
                    newRow["last_modify_at"] = DateTime.Now;

                    dt.Rows.Add(newRow);
                }

                
                //dt.Columns["cart_no"].SetOrdinal(6);
                dt.Columns["status"].SetOrdinal(5);
                //dt.Columns["qty_per_box"].SetOrdinal(6);

                /*
                Xử lý thêm picking mới
                1. Truncate bảng picking_list_new_upload
                */
                new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecQueryNonData("truncate table [dbo].[bom_new_upload]");

                /*
                2. Bulk insert dữ liệu vào bảng 
                */
                //thêm picking list mới
                QueryResult res = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).BulkCopy(dt, "[dbo].[bom_new_upload]");
                if (res.Code != 1)
                {
                    if (res.Msg.Contains("Violation of PRIMARY KEY"))
                    {
                        int start = res.Msg.IndexOf('(');
                        int stop = res.Msg.IndexOf(')');
                        string duplicate = res.Msg.Substring(start + 1, stop - start - 1);
                        return new ObjectResult(new { type = "error", message = "Có mã linh kiện bị lặp nhiều lần thông tin trong danh sách " + duplicate });
                    }
                    return new ObjectResult(new { type = "error", message = res.Msg });
                }

                /*
                3. Hậu xử lý dữ liệu sau khi insert 
                */
                res = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecProcedureDataQueryResult("[dbo].[sp_upload_bom]");

                return new ObjectResult(new { type = res.Code == 1 ? "success" : "error", message = res.Msg });
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { type = "error", message = ex.Message });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}