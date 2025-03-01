using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mission08_Group1.Models;
using SQLitePCL;

namespace Mission07_Group.Controllers
{
    public class HomeController : Controller
    {
        private iTaskRepo _repo;

        public HomeController(iTaskRepo temp)
        {
            _repo = temp;
        }

        // Index Get
        public IActionResult Index()
        {
            var toTask = _repo.Tasks.ToList();
            return View(toTask);
        }

        // Add task get 
        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            // Populate the dropdown list
            ViewBag.Category = _repo.Categories;

            if (id == null)
            {
                return View("AddEdit", new ToTask()); // If no id, return a new task
            }

            var task = _repo.Tasks.Find(t => t.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // Add task post
        [HttpPost]
        public IActionResult AddEdit(ToTask response)
        {
            if (ModelState.IsValid)
            {
                _repo.AddTask(response); // Correct method for adding tasks
                _repo.SaveChanges(); // Save changes

                return View("Confirmation", response);
            }
            else // If it has invalid data
            {
                ViewBag.Category = _repo.Categories
                    .OrderBy(x => x.CategoryName)
                    .ToList();

                return View(response);
            }
        }

        // TaskDescription Edit Get
        public IActionResult Edit(int id)
        {
            var taskToEdit = _repo.Tasks.Single(x => x.TaskId == id);

            ViewBag.Category = _repo.Categories;
            return View("AddEdit", taskToEdit);
        }

        // TaskDescription Edit Post
        [HttpPost]
        public IActionResult Edit(ToTask form)
        {
            _repo.UpdateTask(form); // Corrected method call
            _repo.SaveChanges(); // Save the changes

            return RedirectToAction("Index");
        }

        // Delete TaskDescription Get
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var taskToDelete = _repo.Tasks.Single(x => x.TaskId == id);
            return View("Delete", taskToDelete);
        }

        // Delete TaskDescription Post
        [HttpPost]
        public IActionResult Delete(ToTask form)
        {
            _repo.DeleteTask(form); // Correct method for deleting
            _repo.SaveChanges(); // Save changes
            return RedirectToAction("Index");
        }
    }
}
