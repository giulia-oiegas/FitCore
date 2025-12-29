using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitCore.Data;
using FitCore.Data.Models;

namespace FitCore.Web.Pages.Trainers
{
    public class IndexModel : PageModel
    {
        private readonly FitCore.Data.FitCoreContext _context;

        public IndexModel(FitCore.Data.FitCoreContext context)
        {
            _context = context;
        }

        public IList<Trainer> Trainer { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public async Task OnGetAsync()
        {
            //luam toti antrenorii
            var trainers = from t in _context.Trainers
                           select t;

            //daca exista text in bara de cautare, filtram
            if (!string.IsNullOrEmpty(SearchString))
            {
                trainers = trainers.Where(s => s.Name.Contains(SearchString) || s.Specialization.Contains(SearchString));
            }

            //executam query-ul
            Trainer = await trainers.ToListAsync();
        }
    }
}
