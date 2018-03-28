using mvc1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc1.Controllers
{
    public class addoreditController : Controller
    {
        //
        // GET: /addoredit/

        [HttpGet]
        public ActionResult register()
        {
            register reg = new register();

            return View(reg);

        }
        [HttpPost]
        public ActionResult register(register reg)
        {
            string fileName = Path.GetFileNameWithoutExtension(reg.ImageFile.FileName);
            string extension = Path.GetExtension(reg.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            reg.photo = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/") + fileName);
            reg.ImageFile.SaveAs(fileName);
            using (mvctestEntities1 db = new mvctestEntities1())
            {
                db.registers.Add(reg);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.saved = "saved";
            return View("register", new register());

        }
        [HttpGet]

        public ActionResult Index()
        {
            mvctestEntities1 db = new mvctestEntities1();
            var item = (from d in db.registers select d);
            return View(item);



        }
        public ActionResult Details(int id)
        {
            using (mvctestEntities1 db = new mvctestEntities1())
            {
                return View(db.registers.Where(x => x.Rid == id).FirstOrDefault());
            }
        }
        public ActionResult Edit(int id)
        {
            using (mvctestEntities1 db = new mvctestEntities1())
            {
                return View(db.registers.Where(x => x.Rid == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult Edit(register reg)
        {
            string fileName = Path.GetFileNameWithoutExtension(reg.ImageFile.FileName);
            string extension = Path.GetExtension(reg.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            reg.photo = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/") + fileName);
            reg.ImageFile.SaveAs(fileName);
            using (mvctestEntities1 db = new mvctestEntities1())
            {

                db.Entry(reg).State = EntityState.Modified;
                db.SaveChanges();
            }

            ViewBag.saved = "saved";
            return View("register", new register());

        }


        public ActionResult Delete(int id)
        {
            using (mvctestEntities1 db = new mvctestEntities1())
            {
                return View(db.registers.Where(x => x.Rid == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (mvctestEntities1 db = new mvctestEntities1())
                {
                    register reg = db.registers.Where(x => x.Rid == id).FirstOrDefault();
                    db.registers.Remove(reg);
                    db.SaveChanges();


                }
                return RedirectToAction("view");

            }
            catch
            {
                return View();
            }
        }

       
    }
}
      

    

	
