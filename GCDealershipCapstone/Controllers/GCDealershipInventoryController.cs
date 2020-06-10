using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GCDealershipCapstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GCDealershipCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GCDealershipInventoryController : ControllerBase
    {
        private readonly GCDealershipInventoryContext _context;

        public GCDealershipInventoryController(GCDealershipInventoryContext context)
        {
            _context = context;
        }

        //GET: api/GCDealershipInventory/
        [HttpGet]
        public async Task<ActionResult<List<Inventory>>> GetInventory()
        {
            var inventory = await _context.Inventory.ToListAsync();
            return inventory;
        }

        //GET: api/GCDealershipInventory/FilterBy/{make}/{model}/{style}/{year}/{color}/{mileage}
        [Route("[action]/{make}/{model}/{style}/{year}/{color}/{mileage}")]
        [HttpGet]
        public async Task<ActionResult<List<Inventory>>> FilterBy(string make, string model, string style, int year, string color, int mileage)
        {
            IEnumerable<Inventory> inventory = await _context.Inventory.ToListAsync();
            IEnumerable<Inventory> filtered = inventory;
            if (make != "0")
            {
                IEnumerable<Inventory> ofMake = inventory.Where(x => x.Make.ToLower() == make.ToLower()).ToList();
                filtered = (IEnumerable<Inventory>)ofMake.Intersect(filtered);
            }
            if (model != "0")
            {
                IEnumerable<Inventory> ofModel = inventory.Where(x => x.Model.ToLower().Contains(model.ToLower())).ToList();
                filtered = (IEnumerable<Inventory>)ofModel.Intersect(filtered);
            }
            if (style != "0")
            {
                IEnumerable<Inventory> ofStyle = inventory.Where(x => x.Style.ToLower().Contains(style.ToLower())).ToList();
                filtered = (IEnumerable<Inventory>)ofStyle.Intersect(filtered);
            }
            if (year != 0)
            {
                IEnumerable<Inventory> ofYear = inventory.Where(x => x.Year == year).ToList();
                filtered = (IEnumerable<Inventory>)ofYear.Intersect(filtered);
            }
            if (color != "0")
            {
                IEnumerable<Inventory> ofColor = inventory.Where(x => x.Color.ToLower().Contains(color.ToLower())).ToList();
                filtered = (IEnumerable<Inventory>)ofColor.Intersect(filtered);
            }
            if (mileage != 0)
            {
                IEnumerable<Inventory> ofMileage = inventory.Where(x => x.Mileage < mileage).ToList();
                filtered = (IEnumerable<Inventory>)ofMileage.Intersect(filtered);
            }
            List<Inventory> results = filtered.ToList<Inventory>();
            return results;
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<Inventory>> GetCar(int id)
        {
            List<Inventory> allInventory = await _context.Inventory.ToListAsync();
            Inventory car = new Inventory();

            foreach (Inventory c in allInventory)
            {
                if(c.Id == id)
                {
                    car = c;
                }
            }
            return car;
        }

    }
}
