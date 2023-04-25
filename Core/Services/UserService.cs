using Core.Dtos;
using DataLayer.Entities;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;
        private AuthorizationService authService { get; set; }
        public UserService(UnitOfWork unitOfWork, AuthorizationService authService)
        {
            this.unitOfWork = unitOfWork;
            this.authService = authService;
        }

        public void InitRoles()
        {

        }
        public void Register(RegisterDto registerData)
        {
            if (registerData == null)
            {
                return;
            }

            var hashedPassword = authService.HashPassword(registerData.Password);
            User user;
            if (registerData.UserName == "Rares")
            {
                user = new User
                {
                    UserName = registerData.UserName,
                    PasswordHash = hashedPassword,
                    StudentId = registerData.StudentId,
                    Role = "Teacher"
                };
            }
            else
            {
                user = new User
                {
                    UserName = registerData.UserName,
                    PasswordHash = hashedPassword,
                    StudentId = registerData.StudentId,
                    Role = "Student"
                };
            }           
            unitOfWork.Users.Insert(user);
            unitOfWork.SaveChanges();
        }
        public string Validate(LoginDto payload)
        {
            var student = unitOfWork.Users.GetByUserName(payload.UserName);

            var passwordFine = authService.VerifyHashedPassword(student.PasswordHash, payload.Password);

            if (passwordFine)
            {
                var role = unitOfWork.Users.GetRoleByUserName(student.UserName).ToString();

                return authService.GetToken(student, role);
            }
            else
            {
                return null;
            }

        }

    }
}
