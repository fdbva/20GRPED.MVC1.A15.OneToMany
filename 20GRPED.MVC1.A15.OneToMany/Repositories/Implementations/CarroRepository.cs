using _20GRPED.MVC1.A15.OneToMany.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace _20GRPED.MVC1.A15.OneToMany.Repositories.Implementations
{
    public class CarroRepository : ICarroRepository
    {
        private readonly string _connectionString;

        public CarroRepository(
            IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("OneToManyConnectionString");
        }

        public int Add(Carro carro)
        {
            var cmdText = "INSERT INTO Carro" +
                          "		(Modelo, PessoaId)" +
                          "OUTPUT INSERTED.Id" +
                          "VALUES	(@modelo, @pessoaId);";

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection)) //já faz o close
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters
                    .Add("@modelo", SqlDbType.VarChar).Value = carro.Modelo;
                sqlCommand.Parameters
                    .Add("@pessoaId", SqlDbType.Int).Value = carro.PessoaId;

                sqlConnection.Open();

                var resultScalar = sqlCommand.ExecuteScalar();

                var id = (int)resultScalar;

                return id;
            }
        }

        public IEnumerable<Carro> GetAll()
        {
            const string cmdText = "SELECT * FROM Carro;";

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection)) //já faz o close
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlConnection.Open();

                var carros = new List<Carro>();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        carros.Add(new Carro
                        {
                            Id = reader.GetFieldValue<int>("Id"),
                            Modelo = reader.GetFieldValue<string>("Modelo"),
                            PessoaId = reader.GetFieldValue<int>("PessoaId"),
                        });
                    }
                }

                return carros;
            }
        }
    }
}
