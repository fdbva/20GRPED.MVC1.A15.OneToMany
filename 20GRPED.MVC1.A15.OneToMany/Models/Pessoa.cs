﻿using System.Collections.Generic;

namespace _20GRPED.MVC1.A15.OneToMany.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Carro> Carros { get; set; }
    }
}
