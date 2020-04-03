using _20GRPED.MVC1.A15.OneToMany.Models;
using _20GRPED.MVC1.A15.OneToMany.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _20GRPED.MVC1.A15.OneToMany.Services.Implementations
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ICarroService _carroService;

        public PessoaService(
            IPessoaRepository pessoaRepository,
            ICarroService carroService,
            CallCountScoped callCountScoped,
            CallCountSingleton callCountSingleton,
            CallCountTransient callCountTransient)
        {
            callCountScoped.Count++;
            callCountSingleton.Count++;
            callCountTransient.Count++;
            _pessoaRepository = pessoaRepository;
            _carroService = carroService;
        }

        public async Task<int> AddAsync(Pessoa pessoa, Carro carro)
        {
            if(pessoa == null)
                throw new ArgumentNullException("Não é possível cadastrar sem Pessoa");

            if(string.IsNullOrWhiteSpace(pessoa.Nome) || pessoa.Nome.Length < 4)
                throw new ArgumentException("Nome da pessoa inválido");

            var pessoaAddTask = _pessoaRepository.AddAsync(pessoa);

            if (carro == null)
            {
                var id = await pessoaAddTask;
                return id;
            }

            carro.PessoaId = await pessoaAddTask;
            var carroAddTask = await _carroService.AddAsync(carro);

            return carro.PessoaId;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteAllCarsFromPessoaTask = _carroService.DeleteAllCarsFromPessoaAsync(id);
            var deletePessoaTask = _pessoaRepository.DeleteAsync(id);

            await Task.WhenAll(deleteAllCarsFromPessoaTask, deletePessoaTask);
        }

        public Pessoa GetByIdWithCarros(int id)
        {
            var pessoa = _pessoaRepository.GetByIdWithCarros(id);

            return pessoa;
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            return await _pessoaRepository.GetAllAsync();
        }

        public Pessoa GetById(int id)
        {
            return _pessoaRepository.GetById(id);
        }

        public void Update(int id, Pessoa pessoaUpdated)
        {
            _pessoaRepository.Update(id, pessoaUpdated);
        }
    }
}
