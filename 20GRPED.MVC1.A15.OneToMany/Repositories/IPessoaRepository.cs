using _20GRPED.MVC1.A15.OneToMany.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _20GRPED.MVC1.A15.OneToMany.Repositories
{
    public interface IPessoaRepository
    {
        Task<int> AddAsync(Pessoa pessoa);
        Task<IEnumerable<Pessoa>> GetAllAsync();
        Pessoa GetById(int id);
        void Update(int id, Pessoa pessoaUpdated);
        Task DeleteAsync(int id);
        Pessoa GetByIdWithCarros(int id);
    }
}
