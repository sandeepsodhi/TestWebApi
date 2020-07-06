using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestConsumeWebApi.ApiDTO
{
    public class Patient
    {
        public int Id;
        public string FirstName;
        public string LastName;
        public DateTime DateOfBirth;
        public List<int> Appointments;
    }
}