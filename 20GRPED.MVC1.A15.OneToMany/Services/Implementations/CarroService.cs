using _20GRPED.MVC1.A15.OneToMany.Models;
using _20GRPED.MVC1.A15.OneToMany.Repositories;
using System.Collections.Generic;

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

        public int Add(Carro carro)
        {
            return _carroRepository.Add(carro);
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

        public void DeleteAllCarsFromPessoa(int idPessoa)
        {
            _carroRepository.DeleteAllCarsFromPessoa(idPessoa);
        }
    }
}
