using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemaSite.Context;
using MovieApplication.Models;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;
using CinemaSite.Models;

namespace CinemaSite.Controllers
{
    public class MovieController : Controller
    {

        public ActionResult Index()
        {
            using (var db = new MovieContext())
            {

                return View(db.Movies.ToList());
            }
        }

        [HttpGet]
        public ActionResult AddMovie()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddMovie(Movie movie)
        {
            
            var db = new MovieContext();
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);

                db.SaveChanges();

                return RedirectToAction("SuccessfulResult");
            }
            else
            {
                return View("UnsuccessfulResult");
            }

        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            using (var db = new MovieContext())
            {
                var movies = db.Movies.Find(id);

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (movies != null)
                {
                    return View(movies);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteMovie(int? id)
        {
            using (var db = new MovieContext())
            {
                var movies = db.Movies.Find(id);
                db.Movies.Remove(movies);
                db.SaveChanges();
                return RedirectToAction("SuccessfulResult");
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var db = new MovieContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tickets = new SelectList(db.Movies, "Id", "MovieName", movie.Tickets);
            return View(movie);
        }


        [HttpPost, ActionName("Edit")]
        public ActionResult EditMovie(Movie movie)
        {
            var db = new MovieContext();
            if (ModelState.IsValid)
            {
                db.Movies.AddOrUpdate(movie);

                db.SaveChanges();
                return RedirectToAction("SuccessfulResult");
            }
            ViewBag.Tickets = new SelectList(db.Movies, "Id", "MovieName", movie.Tickets);
            return View(movie);
        }

        public ActionResult SuccessfulResult()
        {
            return View("Successful");
        }




    }
}