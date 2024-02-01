using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyDetailController : ControllerBase
    {
        private readonly PRN292_SU17_1Context context;

        public DummyDetailController(PRN292_SU17_1Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllDummyDetail()
        {
            return Ok(context.DummyDetails.Include(p => p.Master).ToList());
        }

        [HttpGet]
        [Route("{DetailName}")]
        public IActionResult GetDetailByName(string? DetailName)
        {
            if (DetailName == null)
            {
                return Ok(context.DummyDetails.Include(p => p.Master).ToList());
            }
            List<DummyDetail> details = context.DummyDetails.Include(p => p.Master).Where(p => p.DetailName.ToLower().Contains(DetailName.ToLower())).ToList();
            return Ok(details);
        }

        [HttpGet]
        [Route("Master/{master}")]
        public IActionResult GetDetailByMasterName(string? master)
        {
            if (master == null)
            {
                return Ok(context.DummyDetails.Include(p => p.Master).ToList());
            }
            List<DummyDetail> details = context.DummyDetails.Include(p => p.Master).Where(p => p.Master.MasterName.Equals(master)).ToList();
            return Ok(details);
        }

    }
}
