﻿using _20GRPED.MVC1.A15.OneToMany.Models;
using System.Collections.Generic;

namespace _20GRPED.MVC1.A15.OneToMany.Services
{
    public interface IPessoaService
    {
        int Add(Pessoa pessoa, Carro carro);
        IEnumerable<Pessoa> GetAll();
    }
}
