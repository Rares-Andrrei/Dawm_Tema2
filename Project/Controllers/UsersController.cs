using Core.Dtos;
using Core.Services;
using DataLayer.Dtos;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private UserService userService { get; set; }
        private StudentService studentService { get; set; }


        public UsersController(UserService userService, StudentService studentService)
        {
            this.userService = userService;
            this.studentService = studentService;
        }

        [HttpPost("/register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterDto payload)
        {
            userService.Register(payload);
            return Ok();
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto payload)
        {
            var jwtToken = userService.Validate(payload);

            return Ok(new { token = jwtToken });
        }

        [HttpGet("Student-getGrades")]
        [Authorize(Roles = "Student")]
        public ActionResult<GradesByStudent> GetStudentGrades()
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"];
            var tokenString = authorizationHeader.ToString().Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString);
            var userId = token.Claims.First(claim => claim.Type == "userId").Value;

            int studentd = userService.GetStudentId(int.Parse(userId));
            var result = studentService.GetStudentGrades(studentd);

            return Ok(result);
        }

        [HttpGet("Teacher-getGrades")]
        [Authorize(Roles = "Teacher")]
        public ActionResult<AllStudentsGradesDto> GetAllStudentGrades()
        {
            var result = studentService.GetAllStudentGrades();
            return Ok(result);
        }

    }
}
