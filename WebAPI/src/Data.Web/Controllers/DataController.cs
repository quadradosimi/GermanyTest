using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data.Domain.Interfaces;
using Data.Domain.Models;
using Data.Web.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Data.Web.Controllers
{
     [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly DataModelService _dataService;
        private readonly IRepository<DataModel> _dataRepository;
        private readonly IConfiguration _config;

        public DataController(DataModelService dataService,
            IRepository<DataModel> dataRepository,
            IConfiguration config)
        {
            _dataService = dataService;
            _dataRepository = dataRepository;
            _config = config;
        }

         [HttpGet]
         public IEnumerable<DataModelDTO> GetData()
         {
             var data = _dataRepository.GetAll();
            
            var resultado = data.Select(d => new DataModelDTO{ Id = d.Id, Year = d.Year, Description= d.Description });

            return resultado;
         }

         [HttpGet("{id}")]
         public  ActionResult<DataModel> GetData(int id)
         {
             var data = _dataRepository.GetById(id);
             if (data == null)
             {
                 return NotFound(new { message = $"Data id={id} not found." });
             }
             return data;
         }

        [HttpPost()]
        [Authorize]
        public async Task<IActionResult> CreateData([FromBody] IEnumerable<DataModel> data)
        {
            await _dataRepository.Save(data);

            return Ok();
        }

        [HttpPost("getToken")]
        public IActionResult Login([FromBody] UserLoginModel user)
        {
            var root = _config.GetSection("JWT");
            var userName = root.GetSection("UserName").Value;
            var password = root.GetSection("Password").Value;

            if (user.Username == userName && user.Password == password)
            {
                var token = GenerateJwtToken(user.Username);
                return Ok(new { token });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //jwt
            var secret = _config.GetSection("JWT");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret.GetSection("Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddDays(365000),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}