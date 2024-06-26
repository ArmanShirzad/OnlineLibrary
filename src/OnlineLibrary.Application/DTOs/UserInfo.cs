using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.DTOs
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool isAuthenticated{ get; set; }

    }
}

