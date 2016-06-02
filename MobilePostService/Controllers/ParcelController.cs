using MobilePostService.Models;
using MobilePostService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobilePostService.Controllers
{
    public class ParcelController : Controller
    {
        private ParcelRepository _parcelRepo;

        public ParcelController()
        {
            _parcelRepo = new ParcelRepository();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Witaj w serwisie MobilePost";
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult List()
        {
            var parcels = _parcelRepo.GetAllParcels();
            return View(parcels);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Parcel model)
        {
            if (ModelState.IsValid)
            {
                model.RegistrationDate = DateTime.Now;

                _parcelRepo.Add(model);
                _parcelRepo.SaveChanges();

                TempData["Message"] = "Zgłoszenie zostało zarejestrowane.";
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        
        public ActionResult ApiClient()
        {
            return View();
        }
    }
}
