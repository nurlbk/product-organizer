using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using proj1.DB;
using proj1.DB.Models;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;



namespace proj1.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase {
        private readonly ILogger<ApiController> _logger;
        public ApiController(ILogger<ApiController> logger, ApplicationDbContext dbContext) {
            _logger = logger;
            _dbContext = dbContext;
        }
        private readonly ApplicationDbContext _dbContext;


        [HttpPost("upload")]
        public async Task<IActionResult> UploadTable(IFormFile table) {

            try {
                if (table == null || table.Length == 0) {
                    return BadRequest("Файл не загружен или пуст.");
                }
                if (Path.GetExtension(table.FileName) != ".xlsx") {
                    return BadRequest("Неверное расширение файла. Необходимо .xlsx.");
                }


                var products = new List<Product>();

                using (var stream = new MemoryStream()) {
                    await table.CopyToAsync(stream);
                    stream.Position = 0;

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage(stream)) {
                        var worksheet = package.Workbook.Worksheets[0];

                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++) {

                            var product = new Product() {
                                Name = worksheet.Cells[row, 1].Text,
                                Measurment = worksheet.Cells[row, 2].Text,
                                UnitPrice = double.Parse(worksheet.Cells[row, 3].Text.Replace(',', '.')),
                                Quantity = int.Parse(worksheet.Cells[row, 4].Text)
                            };
                            products.Add(product);
                        }
                    }
                }
                
                _dbContext.Products.AddRange(products);
                await _dbContext.SaveChangesAsync();


            } catch (Exception ex) {
                _logger.LogError(ex.Message);
                return BadRequest("Произлошла ошибка обратитесь в поддержку");
            }


            return Ok("Файл успешно загружен и сохранен.");
        }


    }
}
