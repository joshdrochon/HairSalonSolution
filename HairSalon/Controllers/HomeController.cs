using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalonProject.Models;
using HairSalonProject;
using System;

namespace HairSalonProject.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet("/")]
        public ActionResult Index()
        {
            return View(Stylist.GetAll());
        }

        [HttpGet("/stylists/new")]
        public ActionResult NewStylistForm()
        {
            return View();
        }

        [HttpPost("/stylists")]
        public ActionResult Create()
        {
            Stylist newStylist = new Stylist
            (Request.Form["stylist-name"],
             Request.Form["stylist-email"],
             Request.Form["stylist-startdate"]);

             newStylist.Save();

             List<Stylist> allStylists = Stylist.GetAll();

            return View("Index", allStylists);
        }
        //
        // [HttpPost("/stylists/delete")]
        // public ActionResult DeleteAll()
        // {
        //     Stylist.ClearAll();
        //     return View();
        // }
        //
        // [HttpGet("/styles/{id}")]
        // public ActionResult StylistDetails(int id)
        // {
        //     Stylist stylist = Stylist.Find(id);
        //     return View(Stylist);
        // }

    }
}
