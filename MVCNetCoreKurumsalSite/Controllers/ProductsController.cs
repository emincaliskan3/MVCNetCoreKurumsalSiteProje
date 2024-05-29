using Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;
using System.Net.Http.Headers;

namespace MVCNetCoreKurumsalSiteProje.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICategoryService _categoryService; // 1. de sağ click > ampul > generate constructor diyoruz.
        private readonly DatabaseContext _dbContext; // sağ click ampul > add > parameters to menüsüyle birden fazla injection ı ekliypruz!!!
        private readonly IService<Product> _service; // Generic olan Iservice metodumuz 1 class nesnesiyle çalışır. Bu şekilde tüm entity classlarımızı göndererek aynı servis ile db işlemlerini repository pattern sayesinde gerçekleştirebiliyoruz.
        public ProductsController(ICategoryService categoryService, DatabaseContext dbContext, IService<Product> service)
        {
            _categoryService = categoryService;
            _dbContext = dbContext;
            _service = service;
        }

        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return BadRequest();

            }
            var kategori = _categoryService.GetCategoryByProducts(id.Value);
            if (kategori is null)
            {
                return NotFound();
            }
            return View(kategori);
        }
        public async Task<IActionResult> DetailAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var model = await _dbContext.Products.Where(x => x.Id == id && x.IsActive).Include(x => x.Category).FirstOrDefaultAsync();
            if (model is null)
            {
                return NotFound();
            }
            //var model = _service.Get(id.Value);
            return View(model);
        }
    }
}
