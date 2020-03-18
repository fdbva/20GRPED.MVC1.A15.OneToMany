using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20GRPED.MVC1.A15.OneToMany.Models;
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
        private readonly IPessoaService _pessoaService;

        public PessoaController(
            IConfiguration configuration)
        {
            _pessoaService = new PessoaService(
                new PessoaRepository(configuration),
                new CarroService(
                    new CarroRepository(configuration)));
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
            return View();
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pessoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PessoaCarroAggregateViewModel pessoaCarroAggregateViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                _pessoaService.Add(
                    new Pessoa {Nome = pessoaCarroAggregateViewModel.NomePessoa},
                    new Carro
                    {
                        Modelo = pessoaCarroAggregateViewModel.ModeloCarro,
                        PessoaId = pessoaCarroAggregateViewModel.PessoaIdCarro
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
            return View();
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pessoa pessoa)
        {
            try
            {
                // TODO: Add update logic here

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
            return View();
        }

        // POST: Pessoa/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pessoa pessoa)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}