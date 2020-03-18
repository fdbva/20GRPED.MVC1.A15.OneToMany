using _20GRPED.MVC1.A15.OneToMany.Models;
using _20GRPED.MVC1.A15.OneToMany.Repositories;
using System;
using System.Collections.Generic;

namespace _20GRPED.MVC1.A15.OneToMany.Services.Implementations
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ICarroService _carroService;

        public PessoaService(
            IPessoaRepository pessoaRepository,
            ICarroService carroService)
        {
            _pessoaRepository = pessoaRepository;
            _carroService = carroService;
        }

        public int Add(Pessoa pessoa, Carro carro)
        {
            if(pessoa == null)
                throw new ArgumentNullException("Não é possível cadastrar sem Pessoa");

            if(string.IsNullOrWhiteSpace(pessoa.Nome) || pessoa.Nome.Length < 4)
                throw new ArgumentException("Nome da pessoa inválido");

            var id = _pessoaRepository.Add(pessoa);

            if (carro == null)
                return id;

            carro.PessoaId = id;
            _carroService.Add(carro);

            return id;
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return _pessoaRepository.GetAll();
        }
    }
}
