using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAppHWFinal.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        public CrudController()
        {

        }
        
        [HttpPost("create")]
        public IActionResult Create()
        {
            return Ok();
        }
        
        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok();
        }
        
        [HttpPut("update")]
        public IActionResult Update()
        {
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}
