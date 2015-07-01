using Test.Products.Database;
using Test.Products.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Test.Products.Controllers
{

    public class ProductController : Controller
    {
        private readonly ProductsDBContext _dbContext = new ProductsDBContext();

        /* Shows list of products */
        public ActionResult Index()
        {

            var products = _dbContext.Products.ToList();

            return View(products);
        }

        /* Shows an individual page for each product */
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var products = _dbContext.Products.SingleOrDefault(x => x.ID == id);

            if (id == null)
                return HttpNotFound();

            return View(products);
        }

        /* Form to create new product */
        [Authorize]
        public ActionResult Create()
        {
            return View("CreateOrEdit");
        }

        /* Create new product */
        [HttpPost]
        [Authorize]
        public ActionResult Create(Product model, HttpPostedFileBase file)
        {
            /* Upload image */
            if (file != null && file.ContentLength > 0)
            {
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file.FileName); //Image is saved as current timestamp

                var dir = Server.MapPath("~/Content/Pictures/");
                var directory = new DirectoryInfo(dir);

                if (directory.Exists == false)
                {
                    directory.Create();
                }

                var path = Path.Combine(dir, fileName);

                file.SaveAs(path);
                model.Image = fileName;

            }

            model.Slug = model.Title.ToLower().Replace(" ", "-"); // Creates a slug

            _dbContext.Products.Add(model);
            _dbContext.SaveChanges();

            ViewBag.Message = "Product created";

            return View("CreateOrEdit");
        }

        /* Form to edit product */
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = _dbContext.Products.SingleOrDefault(x => x.ID == id);
            product = product ?? new Product();

            ViewBag.Edit = true;

            return View("CreateOrEdit", product);
        }

        /* Update product with the new values */
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Product model, HttpPostedFileBase file)
        {
            ViewBag.Edit = true;

            if (model.ID == 0)
            {
                ModelState.AddModelError("ID", "ID can't be 0. Maybe you want to create instead?");
                return View("CreateOrEdit");
            }

            Product f = _dbContext.Products.FirstOrDefault(x => x.ID == model.ID);
            f.Title = model.Title;
            f.Price = model.Price;
            f.About = model.About;
            f.Slug = model.Title.ToLower().Replace(" ", "-");

            if (file != null && file.ContentLength > 0)
            {
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file.FileName);

                var dir = Server.MapPath("~/Content/Pictures/");
                var directory = new DirectoryInfo(dir);

                if (directory.Exists == false)
                {
                    directory.Create();
                }

                var path = Path.Combine(dir, fileName);

                file.SaveAs(path);
                f.Image = fileName;

            }

            _dbContext.SaveChanges();

            ViewBag.Message = "Product saved";

            return View("CreateOrEdit");
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = _dbContext.Products.SingleOrDefault(x => x.ID == id);
            product = product ?? new Product();

            return View("Delete", product);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(Product model)
        {

            if (!ModelState.IsValid)
                return View("Delete");

            if (model.ID == 0)
            {
                ModelState.AddModelError("ID", "ID can't be 0.");
                return View("Delete");
            }

            var product = _dbContext.Products.Find(model.ID);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            ViewBag.Message = "Product removed";

            return View("Delete");
        }
    }
}