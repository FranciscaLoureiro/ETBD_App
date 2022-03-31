﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ETBD.Data.Entities;
using ETBDApp.Data;

namespace ETBD.Pages.MyProfile
{
    public class CreateModel : PageModel
    {
        private readonly ETBDApp.Data.ApplicationDbContext _context;

        public CreateModel(ETBDApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Profile Profile { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Profiles.Add(Profile);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
