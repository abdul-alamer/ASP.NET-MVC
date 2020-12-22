using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using System.Net.Http;
using static DataLibrary.BusinessLogic.PatientProccess;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Http.Headers;
using System.Web.Helpers;
using System.Text.Json;
using Newtonsoft.Json;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult FindPatient()
        {


            var data = LoadPatient();
            List<PatientModel> Patients = new List<PatientModel>();


            foreach (var row in data)
            {
                Patients.Add(new PatientModel// from sln
                {
                    PatientId = row.PatientId,
                    Name = row.Name,
                    Age = row.Age,
                    Dosage = row.Dosage,
                 
                });
            }
            return View(Patients);
        }


        [HttpGet]
        public ActionResult SearchPatient()
        {
            ViewBag.Message = "Search Patients";
          

            return View();
        }



        public ActionResult SearchPatient(string name )
        {
            PatientModel Patient = new PatientModel();


            List<string> record = new List<string>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44380/");/*api/search/patientByName?name=*/
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/search/patientByName?name="+name).Result;
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                Patient = JsonConvert.DeserializeObject<PatientModel>(products);
            }
             
            
            return View(Patient);

        }







        public ActionResult SignUp()
        {
            ViewBag.Message = "Employee Sign Up";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(PatientModel model)
        {
            if (ModelState.IsValid)
            {
               //  int recordCreate = CreatePatient(model.Name.ToLower(), model.Age, model.Dosage);
               // DBTesting();
                WebClient client = new WebClient();
                string result = client.DownloadString("https://localhost:44380/api/Register?name="+ model.Name.ToLower()+"&age="+ model.Age+"&dosage="+ model.Dosage);
                if (result == null) 
                        return RedirectToAction("Contact");
                else
                return RedirectToAction("Index");
            }
            return View();
        }
        public void DBTesting()
        {
            
            for (int i=0; i<500000; i++)
            {
                Random random = new Random();
                string name = GenerateNames().ToLower();
                int age = random.Next(0,66);
                random = new Random();
                int dosage = random.Next(0, 110);
                WebClient client = new WebClient();
                string result = client.DownloadString("https://localhost:44380/api/Register?name=" +name + "&age=" + age + "&dosage=" + dosage);
            }



        }



        public string GenerateNames()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var  finalString = new String(stringChars);

            return finalString;


        }
    }
}