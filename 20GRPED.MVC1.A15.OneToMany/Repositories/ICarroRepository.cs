using _20GRPED.MVC1.A15.OneToMany.Models;
using System.Collections.Generic;

namespace _20GRPED.MVC1.A15.OneToMany.Repositories
{
    public interface ICarroRepository
    {
        int Add(Carro carro);
        IEnumerable<Carro> GetAll();
        Carro GetById(int id);
        void Update(int id, Carro carroUpdated);
        void Delete(int id);
        void DeleteAllCarsFromPessoa(int idPessoa);
    }
}
