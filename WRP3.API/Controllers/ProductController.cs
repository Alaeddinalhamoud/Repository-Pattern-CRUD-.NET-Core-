using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WRP3.Domain.Entities;
using WRP3.IServices.Common;

namespace WRP3.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            if (product == null)
            {
                return NotFound();
            }

            var model = await _unitOfWork.Product.Add(product);
            return Ok(model);
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            return Ok(_unitOfWork.Product.GetAll());
        }

    }
}
