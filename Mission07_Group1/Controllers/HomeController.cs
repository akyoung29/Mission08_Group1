using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission08_Group1.Models;
using SQLitePCL;

namespace Mission07_Group.Controllers
{

    public class HomeController : Controller
    {
        private Mission08_Group1.Models.TasksContext _context;

        public HomeController(Mission08_Group1.Models.TasksContext tasks)
        {
            _context = tasks;
        }

        // Index Get
        public IActionResult Index()
        {
            var tasks = _context.Tasks
                .ToList();
            return View(tasks);
        }

        // Task Form Get
        [HttpGet]
        public IActionResult TaskForm()
        {
            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.Category)
                .ToList();
            return View("TaskForm", new Task());
        }

        //Task Form Post
        [HttpPost]
        public IActionResult TaskForm(Task response)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(response); //Add record to the database
                _context.SaveChanges();

                return View("Confirmation", response);
            }
            else //If it has invalid data
            {
                ViewBag.Categories = _context.Categories
                .OrderBy(x => x.Category)
                .ToList();

                return View(response);
            }
        }

        // Task Edit Get
        public IActionResult Edit(int id)
        {
            var taskToEdit = _context.Tasks
                .Single(x => x.TaskId == id);

            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.Category)
                .ToList();

            return View("TaskForm", taskToEdit);
        }

        //Task Edit Post
        [HttpPost]
        public IActionResult Edit(TasksContext form)
        {
            _context.Update(form);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Delete Task Get
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var taskToDelete = _context.Tasks
                .Single(x => x.TaskId == id);

            return View("Delete", taskToDelete);
        }

        // Delete Task Post
        [HttpPost]
        public IActionResult Delete(Task form)
        {
            _context.Tasks.Remove(form);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        // Add or Edit task get 
        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            // Populate the dropdown list
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "CategoryName");

            if (id == null)
            {
                return View(new TaskModel()); // If no id, return a new task need a task model
            }

            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // Add or Edit task post
        [HttpPost]
        public IActionResult AddEdit(TaskModel task) // not sure which model is using for add/edit.cshtml
        {
            if (ModelState.IsValid)
            {
                if (task.Id == 0)
                {
                    _context.Tasks.Add(task); // Add new task
                }
                else
                {
                    _context.Tasks.Update(task); // Update existing task
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate ViewBag in case of validation error
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "CategoryName");
            return View(task);
        }
        
    }
}
