using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using System.Threading.Tasks;
using WRP3.Domain.Entities;
using WRP3.IServices.Common;

namespace WRP3.API.Controllers
{
    [Authorize, AuthorizeForScopes(ScopeKeySection = "APIScopes:UserAccess")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductTestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductTest productTest)
        {
            if (productTest == null) return BadRequest();

            return Ok(await _unitOfWork.ProductTest.Add(productTest));
        }

        [HttpGet]
        public IActionResult GetAllProductTest()
        {
            return Ok(_unitOfWork.ProductTest.GetAll());
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductTest productTest)
        {
            if (productTest == null) return BadRequest();

            return Ok(await _unitOfWork.ProductTest.Update(productTest.Id, productTest));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();

            return Ok(await _unitOfWork.ProductTest.Delete(id));
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0) return BadRequest();

            return Ok(await _unitOfWork.ProductTest.Get(id));
        }
    }
}
