using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20GRPED.MVC1.A15.OneToMany.Models;
using _20GRPED.MVC1.A15.OneToMany.Repositories;
using _20GRPED.MVC1.A15.OneToMany.Repositories.Implementations;
using _20GRPED.MVC1.A15.OneToMany.Services;
using _20GRPED.MVC1.A15.OneToMany.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace _20GRPED.MVC1.A15.OneToMany.Controllers
{
    public class PessoaController : Controller
    {
        private readonly ICarroService _carroService;
        private readonly IPessoaService _pessoaService;

        public PessoaController(
            ICarroService carroService,
            IPessoaService pessoaService,
            CallCountScoped callCountScoped,
            CallCountSingleton callCountSingleton,
            CallCountTransient callCountTransient)
        {
            callCountScoped.Count++;
            callCountSingleton.Count++;
            callCountTransient.Count++;

            _carroService = carroService;
            _pessoaService = pessoaService;
        }

        // GET: Pessoa
        public ActionResult Index()
        {
            var pessoas = _pessoaService.GetAll();
            return View(pessoas);
        }

        // GET: Pessoa/Details/5
        public ActionResult Details(int id)
        {
            var pessoa = _pessoaService.GetByIdWithCarros(id);
            return View(pessoa);
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pessoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PessoaCarroCreateAggregateViewModel pessoaCarroCreateAggregateViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                _pessoaService.Add(
                    new Pessoa {Nome = pessoaCarroCreateAggregateViewModel.NomePessoa},
                    new Carro
                    {
                        Modelo = pessoaCarroCreateAggregateViewModel.ModeloCarro,
                    });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(int id)
        {
            var pessoa = _pessoaService.GetById(id);
            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pessoa pessoa)
        {
            try
            {
                // TODO: Add update logic here
                _pessoaService.Update(id, pessoa);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(int id)
        {
            var pessoa = _pessoaService.GetById(id);
            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pessoa pessoa)
        {
            try
            {
                // TODO: Add delete logic here
                _pessoaService.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult TransferCar(int carroId, int pessoaId)
        {
            var carroUpdated = _carroService.GetById(carroId);
            if (carroUpdated == null)
                return View("CarroNaoEncontrado", pessoaId);

            carroUpdated.PessoaId = pessoaId;

            _carroService.Update(carroId, carroUpdated);
            return View("Details");
        }
    }
}