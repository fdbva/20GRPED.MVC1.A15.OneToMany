using _20GRPED.MVC1.A15.OneToMany.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _20GRPED.MVC1.A15.OneToMany.Services
{
    public interface IPessoaService
    {
        Task<int> AddAsync(Pessoa pessoa, Carro carro);
        Task<IEnumerable<Pessoa>> GetAllAsync();
        Pessoa GetById(int id);
        void Update(int id, Pessoa pessoa);
        Task DeleteAsync(int id);
        Pessoa GetByIdWithCarros(int id);
    }
}
