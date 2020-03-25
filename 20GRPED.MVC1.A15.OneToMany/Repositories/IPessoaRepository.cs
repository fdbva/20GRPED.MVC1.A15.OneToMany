using _20GRPED.MVC1.A15.OneToMany.Models;
using System.Collections.Generic;

namespace _20GRPED.MVC1.A15.OneToMany.Repositories
{
    public interface IPessoaRepository
    {
        int Add(Pessoa pessoa);
        IEnumerable<Pessoa> GetAll();
        Pessoa GetById(int id);
        void Update(int id, Pessoa pessoaUpdated);
        void Delete(int id);
        Pessoa GetByIdWithCarros(int id);
    }
}
