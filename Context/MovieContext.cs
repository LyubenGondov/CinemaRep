using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CinemaSite.Migrations;
using CinemaSite.Models;
using MovieApplication.Models;

namespace CinemaSite.Context
{
    public class MovieContext :DbContext
    {
        public MovieContext() :base("MovieDb")
        {
            Database.SetInitializer<MovieContext>(new MigrateDatabaseToLatestVersion<MovieContext,Configuration>());
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}