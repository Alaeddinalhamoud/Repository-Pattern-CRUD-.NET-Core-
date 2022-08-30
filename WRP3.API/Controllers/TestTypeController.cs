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
    public class TestTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TestTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TestType testType)
        {
            if (testType == null) return BadRequest();

            return Ok(await _unitOfWork.TestType.Add(testType));
        }

        [HttpGet]
        public IActionResult GetAllTestType()
        {
            return Ok(_unitOfWork.TestType.GetAll());
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TestType testType)
        {
            if (testType == null) return BadRequest();

            return Ok(await _unitOfWork.TestType.Update(testType.Id, testType));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();

            return Ok(await _unitOfWork.TestType.Delete(id));
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0) return BadRequest();

            return Ok(await _unitOfWork.TestType.Get(id));
        }
    }
}
