using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Weather.Contexts;
using Weather.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using QuickType;
namespace Weather.Controllers
{
    public class CityController : Controller
    {
        private readonly CityContext _context;
        private IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HomeController> _logger;
        protected string APIkey="7923029bc80eb9ace037f21f499fe803";
        public CityController(CityContext context, ILogger<HomeController> logger,
        IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        // GET: City
        public async Task<IActionResult> Index(string searchString)
        {
            var Cities = from m in _context.Cities
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Cities = Cities.Where(s => s.name.Contains(searchString));
            }

            return View(await Cities.ToListAsync());
        }

        // GET: City/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .FirstOrDefaultAsync(m => m.id == id);
            if (city == null)
            {
                return NotFound();
            } 
            string host="http://api.openweathermap.org";
            string uri=host+"/data/2.5/weather?units=metric&id="+id+"&appid="+APIkey;
            var client = _httpClientFactory.CreateClient();
            var response= await client.GetAsync(uri);
            string str=await response.Content.ReadAsStringAsync();
            var data = Welcome.FromJson(str);           
            ViewData["temp"]=data.Main.Temp;
            ViewData["humidity"]=data.Main.Humidity;
            return View(city);
        }

        // GET: City/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: City/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,state,country,lon,lat")] City city)
        {
            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: City/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        // POST: City/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,state,country,lon,lat")] City city)
        {
            if (id != city.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.id))
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
            return View(city);
        }

        // GET: City/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .FirstOrDefaultAsync(m => m.id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.id == id);
        }
    }
}
