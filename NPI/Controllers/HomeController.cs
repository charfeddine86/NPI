using Microsoft.AspNetCore.Mvc;
using NPI.Metier.Interfaces;
using NPI.Models;
using System.Diagnostics;

namespace NPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private INPIMetier _nPIMetier;

        public HomeController(INPIMetier nPIMetier,ILogger<HomeController> logger)
        {
            _logger = logger;
            _nPIMetier = nPIMetier;
        }

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

      

        [HttpGet]
        public JsonResult GetCalcule(string formule)
        {
            try
            {
                var data = new { message = _nPIMetier.Calculer(formule), status = "success" };
                return Json(data);
            }
            catch (Exception e)
            {
                return Json(new { message = "Une erreur est survenue", error = e.Message, status = "error" });
            }
        }
    }
}
