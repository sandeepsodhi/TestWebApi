using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    public class AppointmentsController : ApiController
    {
        public static List<Appointment> appointmentList;
        public AppointmentsController()
        {

        }

        // GET: api/appointments
        public List<Appointment> Get()
        {
            if (appointmentList == null)
            {
                appointmentList = new List<Appointment>
                {
                    new Appointment { Id = 1001, PatientId = 1, AppointmentTime = Convert.ToDateTime("2020-06-30").Date, Notes = "This is the appointment of patient with patient id 1" },
                    new Appointment { Id = 1002, PatientId = 2, AppointmentTime = Convert.ToDateTime("2019-05-25").Date, Notes = "This is the appointment of patient with patient id 2" },
                    new Appointment { Id = 1003, PatientId = 3, AppointmentTime = Convert.ToDateTime("2018-09-21").Date, Notes = "This is the appointment of patient with patient id 3" },
                    new Appointment { Id = 1004, PatientId = 4, AppointmentTime = Convert.ToDateTime("2019-01-28").Date, Notes = "This is the appointment of patient with patient id 4" },
                    new Appointment { Id = 1005, PatientId = 5, AppointmentTime = Convert.ToDateTime("2017-02-15").Date, Notes = "This is the appointment of patient with patient id 5" }
                };
            }
            return appointmentList;
        }

        // GET: api/patients/5
        public Appointment Get(int id)
        {
            var appointmentListTemp = appointmentList.FirstOrDefault((a) => a.Id == id);
            return appointmentListTemp;
        }

        public void Post([FromBody]Appointment appointmentData)
        {
            appointmentList.Add(appointmentData);
        }

        // PUT: api/patients/5
        public Appointment Put([FromBody]Appointment appointmentData)
        {
            var appointmentDataTemp = appointmentList.FirstOrDefault((a) => a.Id == appointmentData.Id);
            int index = appointmentList.IndexOf(appointmentDataTemp);
            if (appointmentDataTemp != null)
            {
                appointmentList.FirstOrDefault((p) => p.Id == appointmentData.Id).PatientId = appointmentData.PatientId;
                appointmentList.FirstOrDefault((p) => p.Id == appointmentData.Id).Notes = appointmentData.Notes;
                appointmentList.FirstOrDefault((p) => p.Id == appointmentData.Id).AppointmentTime = appointmentData.AppointmentTime.Date;
            }
            return appointmentList[index];
        }

        // DELETE: api/patients/5
        public void Delete(int id)
        {
            var item = appointmentList.FirstOrDefault((a) => a.Id == id);
            appointmentList.Remove(item);
        }
    }
}
