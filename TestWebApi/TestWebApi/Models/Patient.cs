using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApi.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<int> Appointments { get; set; }
    }
}