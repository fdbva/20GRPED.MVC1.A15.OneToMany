﻿using _20GRPED.MVC1.A15.OneToMany.Models;
using System.Collections.Generic;

namespace _20GRPED.MVC1.A15.OneToMany.Repositories
{
    public interface ICarroRepository
    {
        int Add(Carro carro);
        IEnumerable<Carro> GetAll();
    }
}
