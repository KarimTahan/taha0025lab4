﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using taha0025lab4.Models;

namespace taha0025lab4.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext _movieContext;

        public HomeController(MovieContext context)
        {
            _movieContext = context;
        }

        public IActionResult Index()
        {
            return View(_movieContext.Movies.ToList());
        }

        public IActionResult AddMovie()
        {
            return View();
        }

        public IActionResult CreateMovie(Movie movie)
        {
            _movieContext.Movies.Add(movie);
            _movieContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult EditMovie(int id)
        {
            var movieToUpdate = (from m in _movieContext.Movies where m.MovieId == id select m).FirstOrDefault();
            return View(movieToUpdate);
        }

        public IActionResult ModifyMovie(Movie movie)
        {
            var id = Convert.ToInt32(Request.Form["MovieId"]);

            var movieToUpdate = (from m in _movieContext.Movies where m.MovieId == id select m).FirstOrDefault();
            movieToUpdate.Title = movie.Title;
            movieToUpdate.SubTitle = movie.SubTitle;
            movieToUpdate.Rating = movie.Rating;
            movieToUpdate.Description = movie.Description;
            movieToUpdate.Year = movie.Year;

            _movieContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteMovie(int id)
        {
            var movieToDelete = (from m in _movieContext.Movies where m.MovieId == id select m).FirstOrDefault();
            _movieContext.Movies.Remove(movieToDelete);
            _movieContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
