using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Threading.Tasks;
using WRP3.Domain.Entities;
using WRP3.IServices.Common;

namespace WRP3.API.Controllers
{
    [Authorize, RequiredScope("access_as_user")]
    [ApiController]
    [Route("api/[controller]")]
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
            if (product == null) return BadRequest();

            return Ok(await _unitOfWork.Product.Add(product));
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            return Ok(_unitOfWork.Product.GetAll());
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            if (product == null) return BadRequest();

            return Ok(await _unitOfWork.Product.Update(product.Id, product));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();

            return Ok(await _unitOfWork.Product.Delete(id));
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0) return BadRequest();

            return Ok(await _unitOfWork.Product.Get(id));
        }
    }
}
