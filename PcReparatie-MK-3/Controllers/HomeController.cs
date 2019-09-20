using PcReparatie_MK_2.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PcReparatie_MK_2.Controllers
{
    public class HomeController : Controller
    {
        DataBase db = new DataBase();
        public ActionResult Index()
        {
            IEnumerable<Reparaty> reparaties = db.Reparaties.AsEnumerable<Reparaty>();
            return View(reparaties);
        }

        [HttpGet]
        public ActionResult Index(string sortParams)
        {
            IEnumerable<Reparaty> reparaties = db.Reparaties.AsEnumerable<Reparaty>();
            switch (sortParams)
            {
                case "Titel":
                    reparaties = reparaties.OrderBy(reparatie => reparatie.Titel);
                    break;
                case "Klant":
                    reparaties = reparaties.OrderBy(reparatie => reparatie.Klanten.AchterNaam);
                    break;
                case "Datum":
                    reparaties = reparaties.OrderBy(reparatie => reparatie.StartDatum);
                    break;
                case "Status":
                    reparaties = reparaties.OrderBy(
                        reparatie => reparatie.Status == "In afwachting" ? 1:
                        reparatie.Status == "In behandeling"? 2:
                        reparatie.Status == "Wachten op onderdelen"? 3:4) ;
                    break;
            }


            return View(reparaties);
        }

        public ActionResult OrderEdit(int? id)
        {
            Reparaty tempReparatie = db.Reparaties.Where(e => e.Id == id).FirstOrDefault();
            return View(tempReparatie);
        }

        public ActionResult OrderDetails(int id)
        {
            Reparaty tempReparatie = db.Reparaties.Where(e => e.Id == id).FirstOrDefault();
            return View(tempReparatie);
        }

        public ActionResult OrderDelete(int id)
        {
            Reparaty tempReparatie = db.Reparaties.Where(e => e.Id == id).FirstOrDefault();
            return View(tempReparatie);
        }

        public ActionResult OrderCreate()
        {
            return View();
        }

        public ActionResult KlantCreate()
        {
            return View();
        }

        public ActionResult MedewerkerCreate()
        {
            return View();
        }

        public ActionResult OnderdeelCreate(int id)
        {
            Gebruikt tempGebruikt = new Gebruikt();
            tempGebruikt.ReparatieId = id;
            return View(tempGebruikt);
        }

        public ActionResult OnderdeelDelete(int id)
        {
            Gebruikt tempOnderdeel = db.Gebruikts.Where(e => e.Id == id).FirstOrDefault();
            return View(tempOnderdeel);
        }
        [HttpPost]
        public ActionResult OrderEdit(Reparaty tempReparaty)
        {
            var entity = db.Reparaties.SingleOrDefault(Reparatie => Reparatie.Id == tempReparaty.Id);
            tempReparaty.MedewerkerID = int.Parse(Request.Form["MedewerkeridEdit"]);
            decimal tempLoon = 0;
            bool success = decimal.TryParse(Request.Form["ArbeidsLoonbox"].Replace('.',','), out tempLoon);
            entity.Arbeidsloon = success ? tempLoon : 0;
            entity.Beschrijving = tempReparaty.Beschrijving;
            entity.Gebruikts = tempReparaty.Gebruikts;
            entity.Procedure = tempReparaty.Procedure;
            entity.Status = tempReparaty.Status;
            entity.Titel = tempReparaty.Titel;
            entity.MedewerkerID = tempReparaty.MedewerkerID;
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult OrderDelete(Reparaty tempReparaty)
        {
            db.Reparaties.Remove(db.Reparaties.Where(e => e.Id == tempReparaty.Id).First());
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult OrderCreate(Reparaty tempReparaty)
        {
            tempReparaty.KlantID = int.Parse(Request.Form["Klantenid"]);
            tempReparaty.MedewerkerID = int.Parse(Request.Form["Medewerkerid"]);
            tempReparaty.Arbeidsloon = 0;
            db.Reparaties.Add(tempReparaty);          
            db.SaveChanges();
            
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult KlantCreate(Klanten tempKlant)
        {
            tempKlant.RolId = 1;
            db.Klantens.Add(tempKlant);
            db.SaveChanges();
            return Redirect(Session["KlantRefURL"].ToString());
        }

        [HttpPost]
        public ActionResult MedewerkerCreate(Medewerker tempMedewerker)
        {
            tempMedewerker.RolId = 2;
            db.Medewerkers.Add(tempMedewerker);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult OnderdeelCreate(Gebruikt tempGebruikt)
        {

            decimal tempPrijs = 0;
            bool success = decimal.TryParse(Request.Form["OnderdeelPrijsbox"].Replace('.', ','), out tempPrijs);
            tempGebruikt.PrijsOnderdeel = success ? tempPrijs : 0;
            db.Gebruikts.Add(tempGebruikt);
            db.SaveChanges();
            return RedirectToAction("OrderEdit", "Home", new { id = tempGebruikt.ReparatieId });
        }
        [HttpPost]
        public ActionResult OnderdeelDelete(Gebruikt tempGebruikt)
        {
            Gebruikt tempOnderdeel = db.Gebruikts.Where(e => e.Id == tempGebruikt.Id).First();
            db.Gebruikts.Remove(db.Gebruikts.Where(e => e.Id == tempOnderdeel.Id).First());
            db.SaveChanges();          
            return RedirectToAction("OrderEdit", "Home", new { id = tempOnderdeel.ReparatieId });
        }
    }
    
}