using _20GRPED.MVC1.A15.OneToMany.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _20GRPED.MVC1.A15.OneToMany.Services
{
    public interface ICarroService
    {
        Task<int> AddAsync(Carro carro);
        IEnumerable<Carro> GetAll(string filtro = null);
        Carro GetById(int id);
        void Update(int id, Carro carroUpdated);
        void Delete(int id);
        Task DeleteAllCarsFromPessoaAsync(int idPessoa);
    }
}
