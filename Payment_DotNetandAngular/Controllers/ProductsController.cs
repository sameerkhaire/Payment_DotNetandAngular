using AutoMapper;
using ClosedXML.Excel;
using DbModel;
using DbModel.Interface;
using DbModel.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Payment_DotNetandAngular.DTO_s;
using System.Reflection;

namespace Payment_DotNetandAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepo<Product> _ProductRepo;
        private readonly IGenericRepo<ProductBrand> _BrandRepo;
        private readonly IGenericRepo<ProductType> _TypeRepo;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepo<Product> ProductRepo,IGenericRepo<ProductBrand> BrandRepo, IGenericRepo<ProductType> TypeRepo, IMapper mapper)
        {
            _ProductRepo = ProductRepo;
            _BrandRepo = BrandRepo;
            _TypeRepo = TypeRepo;
            _mapper = mapper;

        }
        //[HttpGet]
        //public IEnumerable<ProductReturnDto> GetProduct([FromQuery] Pagination pagination)
        //{
        //    var data = _ProductRepo.GetAllListAsync();
        //    int count = data.;
        //}

        [HttpGet("Brand")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _BrandRepo.GetAllListAsync());
        }

        [HttpGet("ProductType")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductType()
        {
            var dataList = await _TypeRepo.GetAllListAsync(); // Replace this with your actual data retrieval logic.

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");

                // Get the property names of the Person class
                PropertyInfo[] properties = typeof(ProductType).GetProperties();

                // Set headers in the first row dynamically
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = properties[i].Name;
                }

                // Fill the data starting from the second row
                int row = 2;
                foreach (var person in dataList)
                {
                    for (int i = 0; i < properties.Length; i++)
                    {
                        var propertyValue = properties[i].GetValue(person);
                        worksheet.Cell(row, i + 1).Value = (XLCellValue)propertyValue;
                    }
                    row++;
                }

                // Generate a unique filename
                string fileName = $"Export_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                // Save the Excel workbook to a memory stream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    // Set the stream position to the beginning
                    stream.Seek(0, SeekOrigin.Begin);

                    // Return the Excel file as a FileStreamResult
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        
            
        }
    }
}
