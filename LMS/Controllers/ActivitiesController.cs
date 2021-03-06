﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Data;
using LMS.Models;

namespace LMS.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aktivitets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Activities.Include(a => a.ActivityType).Include(a => a.Module);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Aktivitets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktivitet = await _context.Activities
                .Include(a => a.ActivityType)
                .Include(a => a.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aktivitet == null)
            {
                return NotFound();
            }

            return View(aktivitet);
        }

        // GET: Aktivitets/Create
        public IActionResult Create()
        {
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Id");
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id");
            return View();
        }

        // POST: Aktivitets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,StartTime,EndTime,ModuleId,ActivityTypeId")] Aktivitet aktivitet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aktivitet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Id", aktivitet.ActivityTypeId);
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", aktivitet.ModuleId);
            return View(aktivitet);
        }

        // GET: Aktivitets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktivitet = await _context.Activities.FindAsync(id);
            if (aktivitet == null)
            {
                return NotFound();
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Id", aktivitet.ActivityTypeId);
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", aktivitet.ModuleId);
            return View(aktivitet);
        }

        // POST: Aktivitets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartTime,EndTime,ModuleId,ActivityTypeId")] Aktivitet aktivitet)
        {
            if (id != aktivitet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aktivitet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AktivitetExists(aktivitet.Id))
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
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Id", aktivitet.ActivityTypeId);
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", aktivitet.ModuleId);
            return View(aktivitet);
        }

        // GET: Aktivitets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktivitet = await _context.Activities
                .Include(a => a.ActivityType)
                .Include(a => a.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aktivitet == null)
            {
                return NotFound();
            }

            return View(aktivitet);
        }

        // POST: Aktivitets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aktivitet = await _context.Activities.FindAsync(id);
            _context.Activities.Remove(aktivitet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AktivitetExists(int id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }
    }
}
