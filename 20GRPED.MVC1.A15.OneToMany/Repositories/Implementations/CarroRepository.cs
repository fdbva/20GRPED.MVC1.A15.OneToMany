using _20GRPED.MVC1.A15.OneToMany.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using _20GRPED.MVC1.A15.OneToMany.Services.Implementations;

namespace _20GRPED.MVC1.A15.OneToMany.Repositories.Implementations
{
    public class CarroRepository : ICarroRepository
    {
        private readonly string _connectionString;

        public CarroRepository(
            IConfiguration configuration,
            CallCountScoped callCountScoped,
            CallCountSingleton callCountSingleton,
            CallCountTransient callCountTransient)
        {
            callCountScoped.Count++;
            callCountSingleton.Count++;
            callCountTransient.Count++;
            _connectionString = configuration.GetValue<string>("OneToManyConnectionString");
        }

        public int Add(Carro carro)
        {
            var cmdText = 
                    @"INSERT INTO Carro 
                            (Modelo, PessoaId) 
                      OUTPUT INSERTED.Id 
                      VALUES (@modelo, @pessoaId);";

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

        public Carro GetById(int id)
        {
            const string cmdText = "SELECT * FROM Carro WHERE Id = @id";

            using (var sqlConnection = new SqlConnection(_connectionString))
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters
                    .Add("@id", SqlDbType.Int).Value = id;

                sqlConnection.Open();
                using (var reader = sqlCommand.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    return new Carro
                    {
                        Id = reader.GetFieldValue<int>("Id"),
                        Modelo = reader.GetFieldValue<string>("Modelo"),
                        PessoaId = reader.GetFieldValue<int>("PessoaId")
                    };
                }
            }
        }

        public void Update(int id, Carro carroUpdated)
        {
            const string cmdText = "UPDATE Carro " +
                                   "SET " +
                                   "Modelo = (@modelo) " +
                                   "WHERE Id = @id;";

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection)) //já faz o close
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters
                    .Add("@modelo", SqlDbType.VarChar).Value = carroUpdated.Modelo;
                sqlCommand.Parameters
                    .Add("@id", SqlDbType.Int).Value = id;

                sqlConnection.Open();

                sqlCommand.ExecuteScalar();
            }
        }

        public void Delete(int id)
        {
            const string cmdText = "DELETE FROM Carro " +
                                   "WHERE Id = @id;";

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection)) //já faz o close
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters
                    .Add("@id", SqlDbType.Int).Value = id;

                sqlConnection.Open();

                sqlCommand.ExecuteScalar();
            }
        }

        public void DeleteAllCarsFromPessoa(int idPessoa)
        {
            const string cmdText = "DELETE FROM Carro " +
                                   "WHERE PessoaId = @id;";

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection)) //já faz o close
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters
                    .Add("@id", SqlDbType.Int).Value = idPessoa;

                sqlConnection.Open();

                sqlCommand.ExecuteScalar();
            }
        }
    }
}
