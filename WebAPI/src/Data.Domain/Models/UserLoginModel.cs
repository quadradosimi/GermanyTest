using System;

namespace Data.Domain.Models
{
    public class UserLoginModel : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}