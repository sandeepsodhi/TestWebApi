using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    [EnableCors(origins: "http://localhost:65212", headers: "*", methods: "*")]
    public class PatientsController : ApiController
    {


        public static List<Patient> patientList;

        public PatientsController()
        {
        }

        // GET: api/patients
        public List<Patient> Get()
        {
            if (patientList == null)
            {
                patientList = new List<Patient>
                {
                    new Patient { Id = 101, FirstName = "First", LastName = "FirstLastName", DateOfBirth = Convert.ToDateTime("2018-04-11").Date, Appointments = new List<int>(){ 1 } },
                    new Patient { Id = 102, FirstName = "Second", LastName = "SecondLastName", DateOfBirth = Convert.ToDateTime("2016-07-15").Date, Appointments = new List<int>(){ 2 } },
                    new Patient { Id = 103, FirstName = "Third", LastName = "ThirdLastName", DateOfBirth = Convert.ToDateTime("2019-05-12").Date, Appointments = new List<int>(){ 3 } },
                    new Patient { Id = 104, FirstName = "Forth", LastName = "ForthLastName", DateOfBirth = Convert.ToDateTime("2020-02-26").Date, Appointments = new List<int>(){ 4 } },
                    new Patient { Id = 105, FirstName = "Fifth", LastName = "FifthLastName", DateOfBirth = Convert.ToDateTime("2018-01-12").Date, Appointments = new List<int>(){ 5 } }
                };
            }
            return patientList;
        }

        public Patient Get(int id)
        {
            var patientListTemp = patientList.FirstOrDefault((p) => p.Id == id);
            return patientListTemp;
        }

        // POST: api/patients
        public void Post([FromBody]Patient patientData)
        {
            patientList.Add(patientData);
        }

        // PUT: api/patients/5
        public Patient Put([FromBody]Patient patientData)
        {
            var patientDatatemp = patientList.FirstOrDefault((p) => p.Id == patientData.Id);
            int index = patientList.IndexOf(patientDatatemp);
            if (patientDatatemp != null)
            {
                patientList.FirstOrDefault((p) => p.Id == patientData.Id).FirstName = patientData.FirstName;
                patientList.FirstOrDefault((p) => p.Id == patientData.Id).LastName = patientData.LastName;
            }


            return patientList[index];
        }

        // DELETE: api/patients/5
        public void Delete(int id)
        {
            var patientItem = patientList.FirstOrDefault((p) => p.Id == id);
            patientList.Remove(patientItem);
        }

        [Route("api/GetDeviceInfo")]
        public string GetDeviceInfo()
        {
            var platformUser = HttpContext.Current.Request.UserAgent;

            //HttpBrowserCapabilities capability = HttpContext.Current.Request.Browser;
            //var BrowserName = capability.Browser;
            //var version = capability.Version;
            //var platform = capability.Platform;

            return platformUser;
        }
    }
}
