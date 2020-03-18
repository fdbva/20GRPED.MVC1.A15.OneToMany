using System.ComponentModel;

namespace _20GRPED.MVC1.A15.OneToMany.Models
{
    public class PessoaCarroAggregateViewModel
    {
        public string NomePessoa { get; set; }
        public string ModeloCarro { get; set; }

        [Description("Id da Pessoa")]
        public int PessoaIdCarro { get; set; }
    }
}
