using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestConsumeWebApi.ApiDTO
{
    public class Appointment
    {
        public int Id;
        public int PatientId;
        public DateTime AppointmentTime;
        public string Notes;
    }
}