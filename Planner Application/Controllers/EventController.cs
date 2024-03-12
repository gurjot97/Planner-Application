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
    //this class is the Event controller which helps with Event CRUD operations
    public class EventController : Controller
    {
        private readonly PlannerApplicationDBContext _context;

        public EventController(PlannerApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
              return _context.EventDB != null ? 
                          View(await _context.EventDB.ToListAsync()) :
                          Problem("Entity set 'PlannerApplicationDBContext.EventDB'  is null.");
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventDB == null)
            {
                return NotFound();
            }

            var eventViewModel = await _context.EventDB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventViewModel == null)
            {
                return NotFound();
            }

            return View(eventViewModel);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            //helps setup the priority dropdown list
            List<SelectListItem> priorityList = new()
            {
                new SelectListItem { Value = "0", Text = "Low" },
                new SelectListItem { Value = "1", Text = "Medium" },
                new SelectListItem { Value = "2", Text = "High" }
            };

            ViewBag.priorityList = priorityList;

            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Date,City,Weather,Temperature,Location,Priority")] EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                //helps store the appropriate priority value into the database
                String[] priorityValues = { "Low", "Medium", "High" };
                eventViewModel.Priority = priorityValues[int.Parse(eventViewModel.Priority)];

                //retrieves OpenWeatherAPI singleton object instance
                OpenWeatherAPI WeatherApi = OpenWeatherAPI.GetInstance();

                //retrieves forecast data from Open Weather API based on city parameter
                Task<OpenWeatherResponse> response = WeatherApi.RequestData(eventViewModel.City);

                //helps assign the appropriate weather and temperature for the user inputted date
                if (eventViewModel.Date.Date <= DateTime.Now.Date)
                {
                    eventViewModel.Weather = response.Result.List[0].Weather[0].main;
                    eventViewModel.Temperature = double.Parse(response.Result.List[0].Main.temp);
                }
                else if (eventViewModel.Date.Date == DateTime.Now.AddDays(1).Date)
                {
                    eventViewModel.Weather = response.Result.List[8].Weather[0].main;
                    eventViewModel.Temperature = double.Parse(response.Result.List[8].Main.temp);
                }
                else if (eventViewModel.Date.Date == DateTime.Now.AddDays(2).Date)
                {
                    eventViewModel.Weather = response.Result.List[15].Weather[0].main;
                    eventViewModel.Temperature = double.Parse(response.Result.List[15].Main.temp);
                }
                else if (eventViewModel.Date.Date == DateTime.Now.AddDays(3).Date)
                {
                    eventViewModel.Weather = response.Result.List[24].Weather[0].main;
                    eventViewModel.Temperature = double.Parse(response.Result.List[24].Main.temp);
                }
                else if (eventViewModel.Date.Date >= DateTime.Now.AddDays(4).Date)
                {
                    eventViewModel.Weather = response.Result.List[32].Weather[0].main;
                    eventViewModel.Temperature = double.Parse(response.Result.List[32].Main.temp);
                }

                //retrieves happy quote
                Quote happyQuote = new HappyQuote();
                string happyQuoteResponse = happyQuote.TemplateMethod();

                //assigns happy quote
                eventViewModel.Quote = happyQuoteResponse;

                _context.Add(eventViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventViewModel);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventDB == null)
            {
                return NotFound();
            }

            var eventViewModel = await _context.EventDB.FindAsync(id);
            if (eventViewModel == null)
            {
                return NotFound();
            }

            //helps setup the priority dropdown list
            List<SelectListItem> priorityList = new()
            {
                new SelectListItem { Value = "0", Text = "Low" },
                new SelectListItem { Value = "1", Text = "Medium"},
                new SelectListItem { Value = "2", Text = "High" }
            };

            //helps find and selects the priority dropdown list value that user stored in the database
            for (int i = 0; i < priorityList.Count; ++i) {

                if (priorityList[i].Text == eventViewModel.Priority) {
                    priorityList[i].Selected = true; 
                }
            }

            ViewBag.priorityList = priorityList;

            return View(eventViewModel);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Date,City,Weather,Temperature,Location,Priority,Quote")] EventViewModel eventViewModel)
        {
            if (id != eventViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //helps store the appropriate priority value into the database
                    String[] priorityValues = { "Low", "Medium", "High" };
                    eventViewModel.Priority = priorityValues[int.Parse(eventViewModel.Priority)];

                    //retrieves OpenWeatherAPI singleton object instance
                    OpenWeatherAPI WeatherApi = OpenWeatherAPI.GetInstance();

                    //retrieves forecast data from Open Weather API based on city parameter
                    Task<OpenWeatherResponse> response = WeatherApi.RequestData(eventViewModel.City);

                    //helps assign the appropriate weather and temperature for the user inputted date
                    if (eventViewModel.Date.Date <= DateTime.Now.Date)
                    {
                        eventViewModel.Weather = response.Result.List[0].Weather[0].main;
                        eventViewModel.Temperature = double.Parse(response.Result.List[0].Main.temp);
                    }
                    else if (eventViewModel.Date.Date == DateTime.Now.AddDays(1).Date)
                    {
                        eventViewModel.Weather = response.Result.List[8].Weather[0].main;
                        eventViewModel.Temperature = double.Parse(response.Result.List[8].Main.temp);
                    }
                    else if (eventViewModel.Date.Date == DateTime.Now.AddDays(2).Date)
                    {
                        eventViewModel.Weather = response.Result.List[15].Weather[0].main;
                        eventViewModel.Temperature = double.Parse(response.Result.List[15].Main.temp);
                    }
                    else if (eventViewModel.Date.Date == DateTime.Now.AddDays(3).Date)
                    {
                        eventViewModel.Weather = response.Result.List[24].Weather[0].main;
                        eventViewModel.Temperature = double.Parse(response.Result.List[24].Main.temp);
                    }
                    else if (eventViewModel.Date.Date >= DateTime.Now.AddDays(4).Date)
                    {
                        eventViewModel.Weather = response.Result.List[32].Weather[0].main;
                        eventViewModel.Temperature = double.Parse(response.Result.List[32].Main.temp);
                    }

                    _context.Update(eventViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventViewModelExists(eventViewModel.Id))
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
            return View(eventViewModel);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventDB == null)
            {
                return NotFound();
            }

            var eventViewModel = await _context.EventDB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventViewModel == null)
            {
                return NotFound();
            }

            return View(eventViewModel);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EventDB == null)
            {
                return Problem("Entity set 'PlannerApplicationDBContext.EventDB'  is null.");
            }
            var eventViewModel = await _context.EventDB.FindAsync(id);
            if (eventViewModel != null)
            {
                _context.EventDB.Remove(eventViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventViewModelExists(int id)
        {
          return (_context.EventDB?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
