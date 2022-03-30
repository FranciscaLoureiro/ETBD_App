using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ETBD.Data.Entities;
using ETBDApp.Data;

namespace ETBD.Pages.MyActions
{
    public class IndexModel : PageModel
    {
        private readonly ETBDApp.Data.ApplicationDbContext _context;

        public IndexModel(ETBDApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Action> Action { get;set; }

        public async Task OnGetAsync()
        {
            Action = await _context.Actions.ToListAsync();
        }
    }
}
