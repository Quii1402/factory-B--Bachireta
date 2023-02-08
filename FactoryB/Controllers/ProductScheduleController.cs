using FactoryB.Models;
using KDTVN_Shared.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Data;

namespace FactoryB.Controllers
{
    public class ProductScheduleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetProduct(ProductScheduleModel input)
        {
            var result = new ProductScheduleModel().Get(input);
            return Json(result);
        }

        [HttpPost]
        public IActionResult UpdateStatusComplete(int id)
        {
            var qr = new ProductScheduleModel().UpdateStatusComplete(id);
            return new ObjectResult(new { type = qr.Code == 1 ? "success" : "error", message = qr.Msg });
        }

        [HttpPost]
        public IActionResult UpdateStatusProduct(int id)
        {
            var qr = new ProductScheduleModel().UpdateStatusProduct(id);
            return new ObjectResult(new { type = qr.Code == 1 ? "success" : "error", message = qr.Msg });
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
                foreach (var firstRowCell in inputSheet.Cells[1, 1, 1, 4])
                {
                    dt.Columns.Add(firstRowCell.Text);
                }

                dt.Columns.Add("status");

                ////thêm content
                // Đọc tất cả data bắt đầu từ row thứ 3

                for (var rowNumber = 3; rowNumber <= inputSheet.Dimension.End.Row; rowNumber++)
                {

                    // Lấy 1 row trong excel để truy vấn
                    var row = inputSheet.Cells[rowNumber, 1, rowNumber, 4];
                    // tạo 1 row trong data table
                    var newRow = dt.NewRow();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }

                    dt.Rows.Add(newRow);
                }

                /*
                Xử lý thêm location mới
                1. Truncate bảng product_schedule
                */
                new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).ExecQueryNonData("truncate table [dbo].[product_schedule]");

                /*
                2. Bulk insert dữ liệu vào bảng 
                */
                //thêm product_schedule mới
                QueryResult res = new SQLHelper(DBConnection.KDTVN_FACT_B_BACHIRETA).BulkCopy(dt, "[dbo].[product_schedule]");
                return new ObjectResult(new { type = res.Code == 1 ? "success" : "error", message = res.Msg });

            }
            catch (Exception ex)
            {
                return new ObjectResult(new { type = "error", message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult GetProductItem(string line_code)
        {
            var result = new ProductScheduleModel().GetProductItem(line_code);
            return Json(result);
        }

    }
}