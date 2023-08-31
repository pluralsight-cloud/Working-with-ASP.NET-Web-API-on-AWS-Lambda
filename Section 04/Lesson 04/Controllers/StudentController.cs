using Microsoft.AspNetCore.Mvc;
using WebApiRdsApp.Data;

namespace WebApiRdsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly SchoolContext _schoolContext;

        public StudentController (SchoolContext schoolContext)
        {
            _schoolContext = schoolContext; 
        }

        [HttpGet]
        public ActionResult Get(int take = 10, int skip = 0)
        {
            return Ok(_schoolContext.Students.OrderBy(i => i.StudentId).Skip(skip).Take(take));
        }
    }
}
