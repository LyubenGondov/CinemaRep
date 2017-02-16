using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemaSite.Context;
using CinemaSite.Models;
using MovieApplication.Models;

namespace CinemaSite.Controllers
{
    public class TicketsController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Movies);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "MovieName");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,MovieName,IsPromotional,PlaceInTheHall,Row,MovieId,Halls")] Ticket ticket)
        {

            var movie = db.Movies.Find(ticket.MovieId);

            if (movie != null)
            {

                if (ModelState.IsValid)
                {

                    int rowNumber = int.Parse(ticket.Row);
                    int placeNumber = int.Parse(ticket.PlaceInTheHall);
                    DateTime endDate = Convert.ToDateTime(movie.EndDate);

                    if ((endDate > DateTime.Now) && (placeNumber <= 20) && (rowNumber <= 10) && (placeNumber > 0) &&
                        (rowNumber > 0))
                    {

                        db.Tickets.Add(ticket);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        ModelState.AddModelError("", "In each Hall there are only 10 rows and 20 places!");
                        ViewBag.MovieId = new SelectList(db.Movies, "Id", "MovieName", ticket.MovieId);
                        return View(ticket);
                    }

                }
                else
                {
                    ViewBag.MovieId = new SelectList(db.Movies, "Id", "MovieName", ticket.MovieId);
                    return View(ticket);
                }
            }
            else
            {
                return HttpNotFound();
            }

        }




        // GET: Tickets/Edit/5
            public
            ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "MovieName", ticket.MovieId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MovieName,IsPromotional,PlaceInTheHall,Row,MovieId,Halls")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "MovieName", ticket.MovieId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
