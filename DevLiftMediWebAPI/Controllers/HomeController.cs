using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevLiftMediWebAPI.Models;
using System.Net.Http;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DevLiftMediWebAPI.Controllers
{
    public class HomeController : Controller
    {
        private ITodoItem  itodo { get; set; }
       
        public HomeController(ITodoItem todo) => itodo = todo;
        // GET: /<controller>/
        IEnumerable<TodoItem> tasks_todo = null;
        string Baseurl = "http://localhost:7000/api/todo";
        public ViewResult Index() {
            //Hosted web API REST Service base url  

               
            using (var client = new HttpClient()) {
                
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                //HTTP GET
                var responseTask = client.GetAsync(Baseurl);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Storing the response details recieved from web api
                    var readTask = result.Content.ReadAsAsync<IList<TodoItem>>();
                    readTask.Wait();
                    tasks_todo = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    tasks_todo = Enumerable.Empty<TodoItem>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(tasks_todo);
        }

        /* [HttpPost]
         public IActionResult AddTodoItem(TodoItem item)
         {

             itodo.AddItem(item);
             return RedirectToAction("Index");
         }*/

        [HttpPost]
        public ActionResult AddTodoItem(TodoItem item)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<TodoItem>(Baseurl, item);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(item);
        }
    

        public ActionResult Edit(int id)
        {
            TodoItem item = null;
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                //HTTP GET
                var responseTask = client.GetAsync("http://localhost:7000/api/todo/"+id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<TodoItem>();
                    readTask.Wait();

                    item = readTask.Result;
                }
            }

            return View(item) ;
        }
        [HttpPost]
        public ActionResult Edit(TodoItem item)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:7000/api/todo/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<TodoItem>("http://localhost:7000/api/todo/", item);
                putTask.Wait();


                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(item);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("http://localhost:7000/api/todo/"+ id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}





