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
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            var patients = GetPatientFromAPI();
            return View(patients);
        }
        private List<Patient> GetPatientFromAPI()
        {
            var resultList = new List<Patient>();
            try
            {
                var client = new HttpClient();
                var getDataTask = client.GetAsync("http://localhost:65189/api/patients")
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<List<Patient>>();
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
        public ViewResult GetPatient()
        {
            return View();
        }

        // GET: patient/Details/5
        [HttpPost]
        public ActionResult GetPatient(int id)
        {
            var patient = GetPatientId(id);
            return View(patient);
        }

        private Patient GetPatientId(int id)
        {
            Patient resultList = null;
            try
            {
                var client = new HttpClient();
                var getDataTask = client.GetAsync("http://localhost:65189/api/patients/" + id)
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<Patient>();
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

        // GET: patient/Create
        public ActionResult AddPatient()
        {
            return View();
        }

        // POST: patient/Create
        [HttpPost]
        public ActionResult AddPatient(FormCollection formData)
        {
            Patient resultList = new Patient
            {
                Id = Convert.ToInt32(formData["Id"]),
                FirstName = formData["FirstName"],
                LastName = formData["LastName"],
                DateOfBirth = Convert.ToDateTime(formData["DateOfBirth"]).Date
            };

            try
            {
                var client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(resultList), Encoding.UTF8, "application/json");

                var getDataTask = client.PostAsync("http://localhost:65189/api/patients/", content)
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<Patient>();
                            readResult.Wait();
                            //  resultList = readResult.Result;
                        }
                    });
                getDataTask.Wait();
                List<Patient> updatedListData = GetPatientFromAPI();
                return View(updatedListData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT: api/appointments/5
        public ActionResult UpdatePatient(int id)
        {
            Patient resultList = null;
            try
            {
                var client = new HttpClient();
                var getDataTask = client.GetAsync("http://localhost:65189/api/patients/" + id)
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<Patient>();
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
        public ActionResult UpdatePatient(FormCollection formData)
        {
            Patient patDataNew = new Patient
            {
                Id = Convert.ToInt32(formData["Id"]),
                FirstName = formData["FirstName"],
                LastName = formData["LastName"],
                DateOfBirth = Convert.ToDateTime(formData["DateOfBirth"]).Date
            };

            try
            {
                var client = new HttpClient();

                StringContent content = new StringContent(JsonConvert.SerializeObject(patDataNew), Encoding.UTF8, "application/json");

                var getDataTask = client.PutAsync("http://localhost:65189/api/patients", content)
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            //   resultlistData = GetPatientFromAPI();
                            ViewBag.Result = "Success";
                            var readResult = result.Content.ReadAsAsync<Patient>();
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


        // POST: patient/Delete/5
        public ActionResult DeletePatient(int id)
        {
            try
            {
                var client = new HttpClient();
                var getDataTask = client.DeleteAsync("http://localhost:65189/api/patients/" + id)
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            ViewBag.Result1 = "Deleted";
                        }
                    });
                getDataTask.Wait();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetDeviceInfo()
        {
            string resultString = "";
            try
            {
                var client = new HttpClient();
                var getDataTask = client.GetAsync("http://localhost:65189/api/GetDeviceInfo")
                    .ContinueWith(response =>
                    {
                        var result = response.Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var readResult = result.Content.ReadAsAsync<string>();
                            readResult.Wait();
                            resultString = readResult.Result;
                        }
                    });
                getDataTask.Wait();

                return resultString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}