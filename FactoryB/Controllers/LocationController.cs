using FactoryB.Models;
using KDTVN_Shared.Helper;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Data;

namespace FactoryB.Controllers
{
    public class LocationController : BaseController<LocationController>
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetLocaton()
        {
            var result = new LocationModel().GetAll();
            return Json(result);
        }

        [HttpPost]
        public IActionResult GetLocationCode(LocationModel location)
        {
            var result = new LocationModel().GetLocationCode(location);
            return Json(result);
        }

        [HttpPost]
        public IActionResult InsertLocation(LocationModel input)
        {
            QueryResult qr = new QueryResult();
            input.last_modify_by = SignInAccount.username;
            qr = new LocationModel().InsertLocation(input);
            return new ObjectResult(new { type = qr.Code == 1 ? "success" : "error", message = qr.Msg });
        }

        [HttpPost]
        public IActionResult UpdateLocation(LocationModel input)
        {
            //QueryResult qr = new QueryResult();
            input.last_modify_by = SignInAccount.username;
            var qr = new LocationModel().UpdateLocation(input);

            return new ObjectResult(new { type = qr.Code == 1 ? "success" : "error", message = qr.Msg });
        }

        [HttpDelete]
        public IActionResult DeleteLocation(LocationModel input)
        {
            var response = new LocationModel().DeleteLocation(input);
            return new ObjectResult(new { type = response.Code == 1 ? "success" : "error", message = response.Msg });
        }

        public IActionResult Upload(IFormFile file)
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
                ExcelWorksheet inputSheet = package.Workbook.Worksheets["Sheet1"];

                //convert to datatable
                DataTable dt = new DataTable();

                //thêm header
                //inputSheet.Dimension.End.Column = 4
                foreach (var firstRowCell in inputSheet.Cells[1, 1, 1, 3])
                {
                    dt.Columns.Add(firstRowCell.Text);
                }

                string[] additionalColHeader = {"status", "last_modify_by", "last_modify_at" };
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
                    var row = inputSheet.Cells[rowNumber, 1, rowNumber, 3];
                    // tạo 1 row trong data table
                    var newRow = dt.NewRow();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }
                    newRow["status"] = 1;

                    //create_by
                    newRow["last_modify_by"] = SignInAccount.username;

                    //create_at
                    newRow["last_modify_at"] = DateTime.Now;
                   
                    dt.Rows.Add(newRow);
                }

                /*
                Xử lý thêm location mới
                1. Truncate bảng location_list_new_upload
                */
                new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecQueryNonData("truncate table dbo.location_new_upload");

                /*
                2. Bulk insert dữ liệu vào bảng 
                */
                //thêm location list mới
                QueryResult res = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).BulkCopy(dt, "dbo.location_new_upload");
                if (res.Code == -1)
                {
                    if (res.Msg.Contains("Violation of PRIMARY KEY"))
                    {
                        int start = res.Msg.IndexOf('(');
                        int stop = res.Msg.IndexOf(')');
                        string duplicate = res.Msg.Substring(start + 1, stop - start - 1);
                        return new ObjectResult(new { type = "error", message = "Có mã giá bị lặp nhiều lần thông tin trong danh sách " + duplicate });
                    }
                    return new ObjectResult(new { type = "error", message = res.Msg });
                }
                else
                {
                    /*
                    3. Hậu xử lý dữ liệu sau khi insert 
                    */
                    QueryResult result = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecProcedureDataQueryResult("sp_upload_location_list");

                    return new ObjectResult(new { type = result.Code == 1 ? "success" : "error", message = result.Msg });
                }

            }
            catch (Exception ex)
            {
                return new ObjectResult(new { type = "error", message = ex.Message });
            }
        }
    }
}

