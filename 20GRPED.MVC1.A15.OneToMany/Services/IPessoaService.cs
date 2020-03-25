using _20GRPED.MVC1.A15.OneToMany.Models;
using System.Collections.Generic;

namespace _20GRPED.MVC1.A15.OneToMany.Services
{
    public interface IPessoaService
    {
        int Add(Pessoa pessoa, Carro carro);
        IEnumerable<Pessoa> GetAll();
        Pessoa GetById(int id);
        void Update(int id, Pessoa pessoa);
        void Delete(int id);
        Pessoa GetByIdWithCarros(int id);
    }
}
