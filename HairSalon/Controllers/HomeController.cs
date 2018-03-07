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

        //Stylist Controllers

        [HttpGet("/stylists/new")]
        public ActionResult NewStylistForm()
        {
            return View();
        }

        [HttpPost("/stylists")]
        public ActionResult CreateStylist()
        {
            Stylist newStylist = new Stylist
            (Request.Form["stylist-name"],
             Request.Form["stylist-email"],
             Request.Form["stylist-startdate"]);

             newStylist.Save(); //must save to database  for getAll method to grab it

             IEnumerable<String> checkedSpecialties = Request.Form["stylist-specialty"];

             foreach(var specialty in checkedSpecialties)
             {
                Specialty thisSpecialty = Specialty.Find(Int32.Parse(specialty));
                thisSpecialty.AddStylist(newStylist);
             }

             List<Stylist> allStylists = Stylist.GetAll();

            return View("Index", allStylists);
        }

        [HttpGet("/stylists/{id}/update")]
        public ActionResult UpdateStylistForm(int id)
        {
          Stylist thisStylist = Stylist.Find(id);
          return View(thisStylist);
        }

        [HttpPost("/stylists/{id}/update")]
        public ActionResult UpdateStylist(int id)
        {
          Stylist thisStylist = Stylist.Find(id);
          thisStylist.EditName(Request.Form["new-name"]);
          return RedirectToAction("Index");
        }

        [HttpPost("/stylists/delete")]
        public ActionResult DeleteAllStylists()
        {
            Stylist.DeleteAll();
            return View(); //if not defined, attempts to map method name to a page w/ the same name
        }

        [HttpPost("/stylists/{id}/delete")]
        public ActionResult DeleteStylist(int id)
        {
            Stylist thisStylist = Stylist.Find(id);
            thisStylist.Delete();
            return View();
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult StylistDetails(int id)
        {
            Stylist stylist = Stylist.Find(id);
            return View(stylist);
        }

        //Specialty Controllers

        [HttpGet("/specialties/new")]
        public ActionResult NewSpecialtyForm()
        {
          return View(); //returns NewSpecialtyForm
        }

        [HttpPost("/specialties")]
        public ActionResult CreateSpecialty()
        {
            Specialty newSpecialty = new Specialty
            (Request.Form["specialty-title"],
             Request.Form["specialty-description"]);

             newSpecialty.Save();

             List<Specialty> allSpecialties = Specialty.GetAll();

             return View("AllSpecialties", allSpecialties);
        }

        [HttpGet("/specialties/{id}")]
        public ActionResult SpecialtyDetails(int id)
        {
          Specialty specialty = Specialty.Find(id);
          return View(specialty);
        }

        [HttpGet("/specialties")]
        public ActionResult AllSpecialties()
        {
          return View(Specialty.GetAll());
        }

        [HttpPost("/specialties/delete")]
        public ActionResult DeleteAllSpecialties()
        {
          Specialty.DeleteAll();
          return View();
        }


        //Client Controllers

        [HttpGet("/clients/new")]
        public ActionResult NewClientForm()
        {
            List<Stylist> allStylists = Stylist.GetAll();

            return View(allStylists);
        }

        [HttpPost("/clients")]
        public ActionResult CreateClient()
        {
            Client newClient = new Client
            (Request.Form["client-name"],
             Request.Form["client-email"],
             Request.Form["client-first-appt"],
             int.Parse(Request.Form["client-stylist"]));

             newClient.Save(); //must save to database  for getAll method to grab it

             List<Client> allClients = Client.GetAll();

             return View("AllClients", allClients);
        }

        [HttpGet("/clients/{id}/update")]
        public ActionResult UpdateClientForm(int id)
        {
          Client thisClient = Client.Find(id);
          return View(thisClient);
        }

        [HttpPost("/clients/{id}/update")]
        public ActionResult UpdateClient(int id)
        {
          Client thisClient = Client.Find(id);
          thisClient.Edit(Request.Form["new-name"], Request.Form["new-email"], Request.Form["new-first-appt"]);
          return RedirectToAction("Index");
        }

        [HttpPost("/clients/delete")]
        public ActionResult DeleteAllClients()
        {
            Client.DeleteAll();
            return View(); //if not defined, attempts to map method name to a page w/ the same name
        }

        [HttpGet("/clients/{id}")]
        public ActionResult ClientDetails(int id)
        {
            Client client = Client.Find(id);
            return View(client);
        }

        [HttpGet("/clients")]
        public ActionResult AllClients()
        {
            return View(Client.GetAll());
        }


    }
}
