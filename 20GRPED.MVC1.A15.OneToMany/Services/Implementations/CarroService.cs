using _20GRPED.MVC1.A15.OneToMany.Models;
using _20GRPED.MVC1.A15.OneToMany.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _20GRPED.MVC1.A15.OneToMany.Services.Implementations
{
    public class CarroService : ICarroService
    {
        private readonly ICarroRepository _carroRepository;

        public CarroService(
            ICarroRepository carroRepository,
            CallCountScoped callCountScoped,
            CallCountSingleton callCountSingleton,
            CallCountTransient callCountTransient)
        {
            callCountScoped.Count++;
            callCountSingleton.Count++;
            callCountTransient.Count++;
            _carroRepository = carroRepository;
        }

        public async Task<int> AddAsync(Carro carro)
        {
            return await _carroRepository.AddAsync(carro);
        }

        public IEnumerable<Carro> GetAll(string filtro = null)
        {
            return _carroRepository.GetAll(filtro);
        }

        public Carro GetById(int id)
        {
            return _carroRepository.GetById(id);
        }

        public void Update(int id, Carro carroUpdated)
        {
            _carroRepository.Update(id, carroUpdated);
        }

        public void Delete(int id)
        {
            _carroRepository.Delete(id);
        }

        public async Task DeleteAllCarsFromPessoaAsync(int idPessoa)
        {
            await _carroRepository.DeleteAllCarsFromPessoaAsync(idPessoa);
        }
    }
}
