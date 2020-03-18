using _20GRPED.MVC1.A15.OneToMany.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _20GRPED.MVC1.A15.OneToMany.Repositories.Implementations
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly string _connectionString;

        public PessoaRepository(
            IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("OneToManyConnectionString");
        }

        public int Add(Pessoa pessoa)
        {
            var cmdText = "INSERT INTO Pessoa " +
                          "		(Nome) " +
                          " OUTPUT INSERTED.Id " +
                          " VALUES	(@nome) ";

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection)) //já faz o close
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters
                    .Add("@nome", SqlDbType.VarChar).Value = pessoa.Nome;

                sqlConnection.Open();

                var resultScalar = sqlCommand.ExecuteScalar();

                var id = (int) resultScalar;

                return id;
            }
        }

        public IEnumerable<Pessoa> GetAll()
        {
            const string cmdText = "SELECT * FROM Pessoa;";

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection)) //já faz o close
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlConnection.Open();

                var pessoas = new List<Pessoa>();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pessoas.Add(new Pessoa
                        {
                            Id = reader.GetFieldValue<int>("Id"),
                            Nome = reader.GetFieldValue<string>("Nome")
                        });
                    }
                }

                return pessoas;
            }
        }
    }
}
