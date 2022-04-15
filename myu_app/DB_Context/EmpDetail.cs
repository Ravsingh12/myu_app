using System;
using System.Collections.Generic;

#nullable disable

namespace MyApp.DB_Context
{
    public partial class EmpDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public long Mobile { get; set; }
        public string City { get; set; }
    }
}
