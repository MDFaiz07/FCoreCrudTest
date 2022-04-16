using System;
using System.Collections.Generic;

#nullable disable

namespace FCoreCrudTest.DB_Folder
{
    public partial class LoginTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
