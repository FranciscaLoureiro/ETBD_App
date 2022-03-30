using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ETBD.Data.Entities;
using ETBDApp.Data;

namespace ETBD.Pages.MyActions
{
    public class EditModel : PageModel
    {
        private readonly ETBDApp.Data.ApplicationDbContext _context;

        public EditModel(ETBDApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Action Action { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Action = await _context.Actions.FirstOrDefaultAsync(m => m.Id == id);

            if (Action == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Action).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActionExists(Action.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ActionExists(int id)
        {
            return _context.Actions.Any(e => e.Id == id);
        }
    }
}
