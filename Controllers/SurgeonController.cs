﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Web.WebPages.Html;
using WIRKDEVELOPER.Areas.Identity.Data;
using WIRKDEVELOPER.Models;
namespace WIRKDEVELOPER.Controllers
{
    public class SurgeonController: Controller
    {
        private readonly ApplicationDBContext _Context;

        public SurgeonController(ApplicationDBContext applicationDBContext)
        {
            _Context = applicationDBContext;
        }
        public IActionResult CreatePrescription()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        private IActionResult CreatePrescription( Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                _Context.prescriptions.Add(prescription);
                _Context.SaveChanges();
                return RedirectToAction("");
            }
            return View(prescription);
        }
        public IActionResult CreatePrescriptionItem()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePrescriptionItem(PrescriptionItem prescriptionItem)
        {
            if (ModelState.IsValid)
            {
                _Context.prescriptionItems.Add(prescriptionItem);
                _Context.SaveChanges();
                return RedirectToAction("");
            }
            return View(prescriptionItem);
        }
    } 
}
