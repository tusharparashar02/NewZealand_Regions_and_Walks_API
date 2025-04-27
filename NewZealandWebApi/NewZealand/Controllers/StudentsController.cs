using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NewZealand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents(){
            string[] studentNames = new string[] {"Jhon", "jane", "Mark", "Emily"};
            return Ok(studentNames);
        }
        
    }
}