using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todolist2.infrastructure;
using todolist2.Models;

namespace todolist2.Controllers
{
    public class todoController : Controller
    {
        private readonly todo_context context;

        public todoController (todo_context context)
        {
            this.context = context;
        }
        //Get/
        public async Task<ActionResult> Index()
        {
            IQueryable<todo_list> items = from i in context.todo_list 
                                          orderby i.id 
                                          select i;
            List<todo_list> todoList = await items.ToListAsync();
            return View(todoList);
        }
        public IActionResult Create() => View();

        //POST /todo/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(todo_list item)
        {
            if(ModelState.IsValid)
            {
                context.Add(item);
                await context.SaveChangesAsync();

                TempData["success"] = "new task has been added successfully";

                return RedirectToAction("Index");   
            }
            return View(item);
        }

        public async Task<ActionResult> Edit(int id)
        {
            todo_list item = await context.todo_list.FindAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        //POST /todo/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(todo_list item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();

                TempData["success"] = "new task has been edited successfully";

                return RedirectToAction("Index");
            }
            return View(item);
        }
        public async Task<ActionResult> Delete(int id)
        {
            todo_list item = await context.todo_list.FindAsync(id);
            if (item == null)
                TempData["Error"] = "You are trying to delete itrm that doesn't exist";
            else
            {
                context.todo_list.Remove(item);
                await context.SaveChangesAsync();

                TempData["success"] = "The item has been deleted ";

            }
            return RedirectToAction("Index");
        }
    }
}
