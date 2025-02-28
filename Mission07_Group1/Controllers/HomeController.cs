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
        private Mission08_Group1.Models.TasksContext _context;

        public HomeController(Mission08_Group1.Models.TasksContext temp)
        {
            _context = temp;
        }

        // Index Get
        public IActionResult Index()
        {
            var toTask = _context.ToTask
                .ToList();
            return View(toTask);
        }

        // Add task get 
        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            // Populate the dropdown list
            ViewBag.Cateogry = _context.Category;

            if (id == null)
            {
                return View("AddEdit", new ToTask()); // If no id, return a new task need a task model
            }

            var task = _context.ToTask.Find(id);
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
                _context.ToTask.Add(response); //Add record to the database
                _context.SaveChanges();

                return View("Confirmation", response);
            }
            else //If it has invalid data
            {
                ViewBag.Category = _context.Category
                .OrderBy(x => x.CategoryName)
                .ToList();

                return View(response);
            }
        }

        // TaskDescription Edit Get
        public IActionResult Edit(int id)
        {
            var taskToEdit = _context.ToTask
                .Single(x => x.TaskId == id);

            ViewBag.Category = _context.Category
                .OrderBy(x => x.CategoryName)
                .ToList();

            return View("AddEdit", taskToEdit);
        }

        //TaskDescription Edit Post
        [HttpPost]
        public IActionResult Edit(ToTask form)
        {
            _context.Update(form);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Delete TaskDescription Get
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var taskToDelete = _context.ToTask
                .Single(x => x.TaskId == id);

            return View("Delete", taskToDelete);
        }

        // Delete TaskDescription Post
        [HttpPost]
        public IActionResult Delete(ToTask form)
        {
            _context.ToTask.Remove(form);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
