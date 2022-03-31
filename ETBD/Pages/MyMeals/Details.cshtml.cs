using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ETBD.Data.Entities;
using ETBDApp.Data;

namespace ETBD.Pages.MyMeals
{
    public class DetailsModel : PageModel
    {
        private readonly ETBDApp.Data.ApplicationDbContext _context;

        public DetailsModel(ETBDApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Meal Meal { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Meal = await _context.Meals
                .Include(m => m.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Meal == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
