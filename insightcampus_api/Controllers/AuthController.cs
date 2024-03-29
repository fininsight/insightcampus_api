﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly UserInterface _user;
        private readonly RoleUserInterface _roleuser;
        private string user_id = "";

        public AuthController(IConfiguration config, UserInterface user, RoleUserInterface roleuesr)
        {
            _config = config;
            _user = user;
            _roleuser = roleuesr;
        }

        [HttpPost("join")]
        public IActionResult Join([FromBody]UserModel userModel)
        {
            userModel.reg_dt = DateTime.Now;
            _user.Join(userModel);
            return Ok(userModel);
        }


        [HttpPost("login")]
        public async Task<LoginResultDto> Login([FromBody]UserModel userModel)
        {

            var userMatched = await _user.UserExists(userModel);
            if (userMatched == null)
            {
                return new LoginResultDto
                {
                    result = false,
                    message = "아이디나 패스워드가 일치하지 않습니다."
                };
            }

            userMatched = _user.PasswordCheck(userModel, userMatched);
            if (userMatched == null)
            {
                return new LoginResultDto
                {
                    result = false,
                    message = "아이디나 패스워드가 일치하지 않습니다."
                };
            }

            /*
            if (!_user.check_email_yn(userModel))
            {
                return new LoginResultDto
                {
                    result = false,
                    message = "no verification"
                };
            }
            */

            // generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);            

            
            var claims = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, userMatched.user_seq.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, userMatched.user_id)                    
            });            
            
            var roles = await _roleuser.Select(userMatched.user_seq);
             
            foreach (var role in roles)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, role.role_nm));
            }            

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
       
            return new LoginResultDto
            {
                result = true,
                message = tokenString
            };
        }

        [HttpPost("familyLogin")]
        public async Task<LoginResultDto> familyLogin([FromBody] UserModel userModel)
        {
    
            TeacherModel teacher = new TeacherModel
            {
                teacher_seq = int.Parse(userModel.user_id),
                passwd = userModel.user_pw
            };

            teacher = await _user.FamilyExists(teacher);

            if (teacher == null)
            {
                return new LoginResultDto
                {
                    result = false,
                    message = "아이디나 패스워드가 일치하지 않습니다."
                };
            }
            
            // generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);


            var claims = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, teacher.teacher_seq.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, teacher.teacher_seq.ToString())
            });

            claims.AddClaim(new Claim(ClaimTypes.Role, "family"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new LoginResultDto
            {
                result = true,
                message = tokenString
            };
        }

        [Authorize]
        [HttpGet("login_test")]
        public IActionResult test1()
        {            
            return Ok("로그인이 잘 되어있습니다");
        }

        [Authorize(Roles = "admin")]
        [HttpGet("role_test")]
        public IActionResult test2()
        {
            return Ok("admin 롤이 있습니다.");
        }

        [HttpGet("test")]
        public IActionResult test()
        {
            return Ok("로그인이 잘 되어있습니다");
        }

        //[HttpGet()]
        //public async Task<string> Get([FromBody] UserModel userModel)
        //{
        //    user_id = userModel.user_id;
        //    return user_id;
        //}
    }
}
