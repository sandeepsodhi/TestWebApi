using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWebApi.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentTime { get; set; }
        [StringLength(200)]
        public string Notes { get; set; }
    }
}