using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {

        [Route("/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("/categories")]
        public ActionResult Categories()
        {
            List<Category> allCategories = Category.GetAll();
            return View(allCategories);
        }

        [HttpGet("/categories/new")]
        public ActionResult CategoryForm()
        {
            return View();
        }

        [HttpPost("/categories")]
        public ActionResult AddCategory()
        {
            Category newCategory = new Category(Request.Form["category-name"]);
            List<Category> allCategories = Category.GetAll();
            return View("Categories", allCategories);
        }

        [HttpGet("/categories/{id}")]
        public ActionResult CategoryDetail(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category selectedCategory = Category.Find(id);
            List<Task> categoryTasks = selectedCategory.GetTasks();
            model.Add("category", selectedCategory);
            model.Add("tasks", categoryTasks);
            return View(model);
        }

        [HttpGet("/categories/{id}/tasks/new")]
        public ActionResult CategoryTaskForm(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category selectedCategory = Category.Find(id);
            List<Task> allTasks = selectedCategory.GetTasks();
            model.Add("category", selectedCategory);
            model.Add("tasks", allTasks);
            return View(model);
        }

        [HttpPost("/tasks")]
        public ActionResult AddTask()
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category selectedCategory = Category.Find(Int32.Parse(Request.Form["category-id"]));
            List<Task> categoryTasks = selectedCategory.GetTasks();
            string taskDescription = Request.Form["task-description"];
            Task newTask = new Task(taskDescription);
            categoryTasks.Add(newTask);
            model.Add("tasks", categoryTasks);
            model.Add("category", selectedCategory);
            return View("CategoryDetail", model);
        }

        [HttpGet("/tasks/{id}")]
        public ActionResult TaskDetail(int id)
        {
            Task task = Task.Find(id);
            return View(task);
        }
    }
}
