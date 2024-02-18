﻿using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVCNetCoreKurumsalSiteProje.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _context;

        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return BadRequest();

            }
            var categories = _context.Categories.Include(p => p.Posts).FirstOrDefault(k => k.Id == id); //Kategoriler tablosundan adres çubuğundan gelen id ile eşleşen kategoriyi bul, include metoduyla o kategorinin postlarını sql join ile çek
            if (categories == null)
                return NotFound();

            return View(categories);
        }
    }
}
