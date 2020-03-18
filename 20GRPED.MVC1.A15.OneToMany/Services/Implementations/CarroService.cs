using _20GRPED.MVC1.A15.OneToMany.Models;
using _20GRPED.MVC1.A15.OneToMany.Repositories;
using System.Collections.Generic;

namespace _20GRPED.MVC1.A15.OneToMany.Services.Implementations
{
    public class CarroService : ICarroService
    {
        private readonly ICarroRepository _carroRepository;

        public CarroService(
            ICarroRepository carroRepository)
        {
            _carroRepository = carroRepository;
        }

        public int Add(Carro carro)
        {
            return _carroRepository.Add(carro);
        }

        public IEnumerable<Carro> GetAll()
        {
            return _carroRepository.GetAll();
        }
    }
}
