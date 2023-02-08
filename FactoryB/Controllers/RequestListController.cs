using Dapper;
using FactoryB.Models;
using KDTVN_Shared.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace FactoryB.Controllers
{
    public class RequestListController : BaseController<RequestListController>
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public IActionResult UploadRequestList(IFormFile file)
        {
            IActionResult result;

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
                ////thêm content
                //var currentAccount = JsonConvert.DeserializeObject<AccountExtend>(HttpContext.Session.GetString("Account"));
                // Đọc tất cả data bắt đầu từ row thứ 3
                try
                {

                    for (var rowNumber = 3; rowNumber <= inputSheet.Dimension.Rows; rowNumber++)
                    {

                        if (inputSheet.Cells[rowNumber, 1].Value != null)
                        {
                            // Lấy 1 row trong excel để truy vấn
                            var item_code = inputSheet.Cells[rowNumber, 1].Value.ToString().Trim();
                            //var line_code = inputSheet.Cells[rowNumber, 2].Value.ToString().Trim();
                            var require_qty = Convert.ToDecimal(inputSheet.Cells[rowNumber, 4].Value.ToString().Trim());
                            var last_modify_by = SignInAccount.username;
                            var description = inputSheet.Cells[rowNumber, 3].Value.ToString().Trim();

                            inputSheet.Cells[rowNumber, 5].Value = new RequestListModel().Insert(new RequestListModel
                            {
                                item_code = item_code,
                                request_qty = require_qty,
                                //line_code = line_code,
                                description = description,
                                last_modify_by = last_modify_by
                            }).Msg;
                        }
                        else
                        {
                            break;
                        }

                    }
                }
                catch (Exception ex)
                {

                }

                package.Save();

                byte[] bytes = package.GetAsByteArray();

                result = new ObjectResult(new
                {
                    type = "success",
                    message = JsonConvert.SerializeObject(bytes)
                });
            }
            catch (Exception ex)
            {

                result = new ObjectResult(new { type = "error", message = ex.Message });
            }
            return result;
        }


        public IActionResult DeleteRequest(int id)
        {
            var qr = new RequestListModel().Delete(id);
            return new ObjectResult(new { type = qr.Code == 1 ? "success" : "error", message = qr.Msg });
        }

        [HttpPost]
        public IActionResult GetRequest()
        {
            var result = new RequestListModel().Get();
            return Json(result);
        }

        [HttpPost]
        public IActionResult GetBomModelByProductItem(BomModel bom) 
        {
            if (!String.IsNullOrEmpty(bom.product_item))
            {
                var result = new BomModel().GetBomModelByProductItem(bom);
                return Json(result);
            }
            return Json(new List<BomModel>());
        }

        public IActionResult Upload(IFormFile file)
        {
            IActionResult result;

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
                ExcelWorksheet inputSheet = package.Workbook.Worksheets["sheet1"];
                ////thêm content
                //var currentAccount = JsonConvert.DeserializeObject<AccountExtend>(HttpContext.Session.GetString("Account"));
                // Đọc tất cả data bắt đầu từ row thứ 3
                try
                {

                    for (var rowNumber = 3; rowNumber <= inputSheet.Dimension.Rows; rowNumber++)
                    {

                        if (inputSheet.Cells[rowNumber, 1].Value != null)
                        {
                            // Lấy 1 row trong excel để truy vấn
                            var line = inputSheet.Cells[rowNumber, 2].Value.ToString().Trim();
                            var item_code = inputSheet.Cells[rowNumber, 1].Value.ToString().Trim();
                            var request_qty = Convert.ToInt32(inputSheet.Cells[rowNumber, 3].Value.ToString().Trim());
                            var last_modify_by = SignInAccount.username;

                            inputSheet.Cells[rowNumber, 4].Value = new RequestListModel().Insert(new RequestListModel
                            {
                                item_code = item_code,
                                request_qty = request_qty,
                                line_code = line,
                                last_modify_by = last_modify_by
                            }).Msg;
                        }
                        else
                        {
                            break;
                        }

                    }
                }
                catch (Exception ex)
                {

                }


                package.Save();

                byte[] bytes = package.GetAsByteArray();

                result = new ObjectResult(new
                {
                    type = "success",
                    message = JsonConvert.SerializeObject(bytes)
                });
            }
            catch (Exception ex)
            {

                result = new ObjectResult(new { type = "error", message = ex.Message });
            }
            return result;


        }

        [HttpPost]
        public IActionResult InsertRequest(RequestListModel input)
        {
            input.last_modify_by = SignInAccount.username;
            var qr = new RequestListModel().Insert(input);
            return new ObjectResult(new { type = qr.Code == 1 ? "success" : "error", message = qr.Msg });
        }

    }

}
