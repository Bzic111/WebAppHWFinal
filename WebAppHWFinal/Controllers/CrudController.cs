using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAppHWFinal.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ValuesHolder _holder;
        public CrudController(ValuesHolder holder)
        {
            _holder = holder;
        }
        
        [HttpPost("create/{str}")]
        public IActionResult Create([FromQuery] string str)
        {
            _holder.Add(str);
            return Ok();
        }
        
        [HttpGet("read/{num}")]
        public IActionResult Read([FromQuery] int num)
        {
            string temp = _holder.Get(num)!;
            if (temp is not null)
                return Ok(temp);
            else
                return NotFound();
        }
        
        [HttpPut("update/{num}/{str}")]
        public IActionResult Update([FromQuery] string str, [FromQuery] int num)
        {
            if (_holder.Update(num,str))
                return Ok("updated");
            else
                return NotFound();
        }

        [HttpDelete("delete/{num}")]
        public IActionResult Delete([FromQuery] int num)
        {
            if (_holder.Delete(num))
                return Ok("Deleted");
            else
                return NotFound();
        }
    }
}
