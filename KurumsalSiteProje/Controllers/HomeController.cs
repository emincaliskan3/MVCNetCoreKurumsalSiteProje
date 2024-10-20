using Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using MVCNetCoreKurumsalSiteProje.Models;
using System.Diagnostics;

namespace MVCNetCoreKurumsalSiteProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel()
            {

                Slides = _context.Slides.ToList(),
                Categories = _context.Categories.Where(p => p.IsActive && p.IsHome).ToList(),
                Posts = _context.Posts.Where(p => p.IsActive && p.IsHome).ToList(),



            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [Route("hakkimizda")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Route("iletisim")]
        public ActionResult Contact()
        {


            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Contacts.Add(contact);
                    var sonuc = _context.SaveChanges();
                    if (sonuc > 0)
                    {
                        TempData["Mesaj"] = "<div class = 'alert alert-success'>Te�ekk�rler.. Mesaj�n�z Bize Ula�t�..</div>";

                    }
                }
                catch (Exception)
                {

                    TempData["Mesaj"] = "<div class = 'alert alert-danger'>Hata Olu�tu! Mesaj�n�z G�nderilemedi!</div>";
                }
            }


            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
