﻿using _20GRPED.MVC1.A15.OneToMany.Models;
using _20GRPED.MVC1.A15.OneToMany.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _20GRPED.MVC1.A15.OneToMany.Controllers
{
    public class CarroController : Controller
    {
        private readonly ICarroService _carroService;

        public CarroController(
            ICarroService carroService)
        {
            _carroService = carroService;
        }
        // GET: Carro
        public ActionResult Index()
        {
            var carros = _carroService.GetAll();
            return View(carros);
        }

        // GET: Carro/Details/5
        public ActionResult Details(int id)
        {
            var carro = _carroService.GetById(id);
            return View(carro);
        }

        // GET: Carro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Carro carro)
        {
            try
            {
                _carroService.Add(carro);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Carro/Edit/5
        public ActionResult Edit(int id)
        {
            var carro = _carroService.GetById(id);
            return View(carro);
        }

        // POST: Carro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Carro carro)
        {
            try
            {
                _carroService.Update(id, carro);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Carro/Delete/5
        public ActionResult Delete(int id)
        {
            var carro = _carroService.GetById(id);
            return View(carro);
        }

        // POST: Carro/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _carroService.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}