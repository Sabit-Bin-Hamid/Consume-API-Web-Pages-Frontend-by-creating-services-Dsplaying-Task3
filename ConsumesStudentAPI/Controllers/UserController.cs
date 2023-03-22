using ConsumesStudentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;
using System.Text.Unicode;

namespace ConsumesStudentAPI.Controllers
{
    public class UserController : Controller
    {

        Uri baseAddress = new Uri("http://localhost:5056/api");
        HttpClient client = new HttpClient();
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            try
            {
                List<UserViewModel> modelList = new List<UserViewModel>(); 
                HttpResponseMessage response = client.GetAsync(client.BaseAddress+"/User").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data=response.Content.ReadAsStringAsync().Result;
                    modelList = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
                    return View(modelList);
                }
                return View();
            }
            catch (Exception ex) { throw ex; }
            
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(UserViewModel model)
        {
            try
            {
                //convert js object into json string
                string data = JsonConvert.SerializeObject(model);
                //creating string content
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/User", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch(Exception ex) { throw ex; }  
            
        }



        public IActionResult Edit(Guid id)
        {
            try
            {
                UserViewModel model = new UserViewModel();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/User/"+id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<UserViewModel>(data);
                    
                }
                return View("Create", model);
            }
            catch (Exception ex) { throw ex; }

        }


        [HttpPost]
        public IActionResult Edit(UserViewModel model)
        {
            try
            {
                string data=JsonConvert.SerializeObject(model); 
                StringContent content=new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/User/" + model.Id,content).Result;
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");

                }
                return View("Create", model);
            }
            catch (Exception ex) { throw ex; }

        }

        
        public IActionResult Delete(Guid id)
        {
            //HTTP DELETE
            var deleteTask = client.DeleteAsync(client.BaseAddress+"/User/"+id);
            deleteTask.Wait();
            var response=deleteTask.Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
