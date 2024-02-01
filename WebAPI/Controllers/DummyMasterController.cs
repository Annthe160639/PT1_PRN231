using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyMasterController : ControllerBase
    {
        private readonly PRN292_SU17_1Context context;

        public DummyMasterController(PRN292_SU17_1Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllMaster()
        {
            return Ok(context.DummyMasters.ToList());
        }
    }
}
