using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Planner_Application.Models;
using static Planner_Application.OpenWeatherAPI;

namespace Planner_Application.Controllers
{
    //this class is the Task controller which helps with Task CRUD operations
    public class TaskController : Controller
    {
        private readonly PlannerApplicationDBContext _context;

        public TaskController(PlannerApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Task
        public async Task<IActionResult> Index()
        {
              return _context.TaskDB != null ? 
                          View(await _context.TaskDB.ToListAsync()) :
                          Problem("Entity set 'PlannerApplicationDBContext.TaskDB'  is null.");
        }

        // GET: Task/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaskDB == null)
            {
                return NotFound();
            }

            var taskViewModel = await _context.TaskDB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskViewModel == null)
            {
                return NotFound();
            }

            return View(taskViewModel);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Date,City")] TaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                //retrieves OpenWeatherAPI singleton object instance
                OpenWeatherAPI WeatherApi = OpenWeatherAPI.GetInstance();

                //retrieves forecast data from Open Weather API based on city parameter
                Task<OpenWeatherResponse> response = WeatherApi.RequestData(taskViewModel.City);

                //helps assign the appropriate weather and temperature for the user inputted date
                if (taskViewModel.Date.Date <= DateTime.Now.Date)
                {
                    taskViewModel.Weather = response.Result.List[0].Weather[0].main;
                    taskViewModel.Temperature = double.Parse(response.Result.List[0].Main.temp);
                }
                else if (taskViewModel.Date.Date == DateTime.Now.AddDays(1).Date)
                {
                    taskViewModel.Weather = response.Result.List[8].Weather[0].main;
                    taskViewModel.Temperature = double.Parse(response.Result.List[8].Main.temp);
                }
                else if (taskViewModel.Date.Date == DateTime.Now.AddDays(2).Date)
                {
                    taskViewModel.Weather = response.Result.List[15].Weather[0].main;
                    taskViewModel.Temperature = double.Parse(response.Result.List[15].Main.temp);
                }
                else if (taskViewModel.Date.Date == DateTime.Now.AddDays(3).Date)
                {
                    taskViewModel.Weather = response.Result.List[24].Weather[0].main;
                    taskViewModel.Temperature = double.Parse(response.Result.List[24].Main.temp);
                }
                else if (taskViewModel.Date.Date >= DateTime.Now.AddDays(4).Date)
                {
                    taskViewModel.Weather = response.Result.List[32].Weather[0].main;
                    taskViewModel.Temperature = double.Parse(response.Result.List[32].Main.temp);
                }

                //retrieves success quote
                Quote successQuote = new SuccessQuote();
                string successQuoteResponse = successQuote.TemplateMethod();

                //assigns success quote
                taskViewModel.Quote = successQuoteResponse;


                _context.Add(taskViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskViewModel);
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaskDB == null)
            {
                return NotFound();
            }

            var taskViewModel = await _context.TaskDB.FindAsync(id);
            if (taskViewModel == null)
            {
                return NotFound();
            }

            return View(taskViewModel);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Date,City,Weather,Temperature,Quote")] TaskViewModel taskViewModel)
        {
            if (id != taskViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //retrieves OpenWeatherAPI singleton object instance
                    OpenWeatherAPI WeatherApi = OpenWeatherAPI.GetInstance();

                    //retrieves forecast data from Open Weather API based on city parameter
                    Task<OpenWeatherResponse> response = WeatherApi.RequestData(taskViewModel.City);

                    //helps assign the appropriate weather and temperature for the user inputted date
                    if (taskViewModel.Date.Date <= DateTime.Now.Date)
                    {
                        taskViewModel.Weather = response.Result.List[0].Weather[0].main;
                        taskViewModel.Temperature = double.Parse(response.Result.List[0].Main.temp);
                    }
                    else if (taskViewModel.Date.Date == DateTime.Now.AddDays(1).Date)
                    {
                        taskViewModel.Weather = response.Result.List[8].Weather[0].main;
                        taskViewModel.Temperature = double.Parse(response.Result.List[8].Main.temp);
                    }
                    else if (taskViewModel.Date.Date == DateTime.Now.AddDays(2).Date)
                    {
                        taskViewModel.Weather = response.Result.List[16].Weather[0].main;
                        taskViewModel.Temperature = double.Parse(response.Result.List[16].Main.temp);
                    }
                    else if (taskViewModel.Date.Date == DateTime.Now.AddDays(3).Date)
                    {
                        taskViewModel.Weather = response.Result.List[24].Weather[0].main;
                        taskViewModel.Temperature = double.Parse(response.Result.List[24].Main.temp);
                    }
                    else if (taskViewModel.Date.Date >= DateTime.Now.AddDays(4).Date)
                    {
                        taskViewModel.Weather = response.Result.List[32].Weather[0].main;
                        taskViewModel.Temperature = double.Parse(response.Result.List[32].Main.temp);
                    }

                    _context.Update(taskViewModel);
                    await _context.SaveChangesAsync();

                    if (id == null || _context.TaskDB == null)
                    {
                        return NotFound();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskViewModelExists(taskViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taskViewModel);
        }

        // GET: Task/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaskDB == null)
            {
                return NotFound();
            }

            var taskViewModel = await _context.TaskDB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskViewModel == null)
            {
                return NotFound();
            }

            return View(taskViewModel);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaskDB == null)
            {
                return Problem("Entity set 'PlannerApplicationDBContext.TaskDB'  is null.");
            }
            var taskViewModel = await _context.TaskDB.FindAsync(id);
            if (taskViewModel != null)
            {
                _context.TaskDB.Remove(taskViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskViewModelExists(int id)
        {
          return (_context.TaskDB?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
