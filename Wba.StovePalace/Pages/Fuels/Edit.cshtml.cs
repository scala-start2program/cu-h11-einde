﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wba.StovePalace.Data;
using Wba.StovePalace.Helpers;
using Wba.StovePalace.Models;

namespace Wba.StovePalace.Pages.Fuels
{
    public class EditModel : PageModel
    {
        private readonly Wba.StovePalace.Data.StoveContext _context;

        public EditModel(Wba.StovePalace.Data.StoveContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Fuel Fuel { get; set; } = default!;
        public Availability Availability { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                return RedirectToPage("../Stoves/Index");
            }

            if (id == null || _context.Fuel == null)
            {
                return NotFound();
            }

            var fuel =  await _context.Fuel.FirstOrDefaultAsync(m => m.Id == id);
            if (fuel == null)
            {
                return NotFound();
            }
            Fuel = fuel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                return RedirectToPage("../Stoves/Index");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Fuel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuelExists(Fuel.Id))
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

        private bool FuelExists(int id)
        {
          return _context.Fuel.Any(e => e.Id == id);
        }
    }
}
