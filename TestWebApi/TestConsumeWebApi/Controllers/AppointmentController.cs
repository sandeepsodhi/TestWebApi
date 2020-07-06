using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TestConsumeWebApi.ApiDTO;

namespace TestConsumeWebApi.Controllers
{
    public class AppointmentController : Controller
    {
        public ActionResult Index()
        {
            var appointment = GetAppointmentFromAPI();
            return View(appointment);
        }

        private List<Appointment> GetAppointmentFromAPI()
        {
            var resultList = new List<Appointment>();
            try
            {
                var client = new HttpClient();
                var getDataTask = client.GetAsync("http://localhost:65189/api/appointments")
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<List<Appointment>>();
                            readResult.Wait();
                            resultList = readResult.Result;
                        }
                    });
                getDataTask.Wait();
                return resultList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ViewResult GetAppointment()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetAppointment(int id)
        {
            var appointment = GetAppointmentId(id);
            return View(appointment);
        }

        private Appointment GetAppointmentId(int id)
        {
            Appointment resultList = null;
            try
            {
                var client = new HttpClient();
                var getDataTask = client.GetAsync("http://localhost:65189/api/appointments/" + id)
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<Appointment>();
                            readResult.Wait();
                            resultList = readResult.Result;
                        }
                    });
                getDataTask.Wait();
                return resultList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AddAppointment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAppointment(FormCollection formData)
        {
            Appointment resultList = new Appointment
            {
                Id = Convert.ToInt32(formData["Id"]),
                PatientId = Convert.ToInt32(formData["PatientId"]),
                Notes = formData["Notes"],
                AppointmentTime = Convert.ToDateTime(formData["AppointmentTime"]).Date
            };

            try
            {
                var client = new HttpClient();

                StringContent content = new StringContent(JsonConvert.SerializeObject(resultList), Encoding.UTF8, "application/json");

                var getDataTask = client.PostAsync("http://localhost:65189/api/appointments/", content)
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<Appointment>();
                            readResult.Wait();
                            resultList = readResult.Result;
                        }
                    });
                getDataTask.Wait();
                List<Appointment> updatedListData = GetAppointmentFromAPI();
                return View(updatedListData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult UpdateAppointment(int id)
        {
            Appointment resultList = null;
            try
            {
                var client = new HttpClient();
                var getDataTask = client.GetAsync("http://localhost:65189/api/appointments/" + id)
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<Appointment>();
                            readResult.Wait();
                            resultList = readResult.Result;
                        }
                    });
                getDataTask.Wait();
                return View(resultList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult UpdateAppointment(FormCollection formData)
        {
            Appointment patDataNew = new Appointment
            {
                Id = Convert.ToInt32(formData["Id"]),
                PatientId = Convert.ToInt32(formData["PatientId"]),
                Notes = formData["Notes"],
                AppointmentTime = Convert.ToDateTime(formData["AppointmentTime"]).Date
            };

            try
            {
                var client = new HttpClient();

                StringContent content = new StringContent(JsonConvert.SerializeObject(patDataNew), Encoding.UTF8, "application/json");

                var getDataTask = client.PutAsync("http://localhost:65189/api/appointments", content)
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            //   resultlistData = GetPatientFromAPI();
                            ViewBag.Result = "Success";

                            var readResult = result.Content.ReadAsAsync<Appointment>();
                            readResult.Wait();
                            patDataNew = readResult.Result;
                        }
                    });
                getDataTask.Wait();
                return View(patDataNew);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DeleteAppointment(int id)
        {
            try
            {
                var client = new HttpClient();
                var getDataTask = client.DeleteAsync("http://localhost:65189/api/appointments/" + id)
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.Result1 = "Deleted";
                        }
                    });
                getDataTask.Wait();
                ViewBag.Result1 = "Deleted";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}