using _20GRPED.MVC1.A15.OneToMany.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using _20GRPED.MVC1.A15.OneToMany.Services.Implementations;

namespace _20GRPED.MVC1.A15.OneToMany.Repositories.Implementations
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly string _connectionString;

        public PessoaRepository(
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

        public int Add(Pessoa pessoa)
        {
            const string cmdText = "INSERT INTO Pessoa " +
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

        public Pessoa GetById(int id)
        {
            const string cmdText = "SELECT * FROM Pessoa WHERE Id = @id";

            using(var sqlConnection = new SqlConnection(_connectionString))
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters
                    .Add("@id", SqlDbType.Int).Value = id;

                sqlConnection.Open();
                return ReadPessoa(sqlCommand);
            }
        }

        /// <summary>
        /// Instancia uma pessoa e popula os dados a partir do sqlCommand com um reader.
        /// Precisa ter a sqlConnection.Open() executado antes
        /// </summary>
        /// <param name="sqlCommand">Não pode ser null!</param>
        /// <returns></returns>
        private static Pessoa ReadPessoa(SqlCommand sqlCommand)
        {
            using (var reader = sqlCommand.ExecuteReader())
            {
                if (!reader.Read())
                    return null;

                return new Pessoa
                {
                    Id = reader.GetFieldValue<int>("Id"),
                    Nome = reader.GetFieldValue<string>("Nome")
                };
            }
        }

        public void Update(int id, Pessoa pessoaUpdated)
        {
            const string cmdText = "UPDATE Pessoa " +
                          "SET " +
                          "Nome = (@nome) " +
                          "WHERE Id = @id;";

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection)) //já faz o close
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters
                    .Add("@nome", SqlDbType.VarChar).Value = pessoaUpdated.Nome;
                sqlCommand.Parameters
                    .Add("@id", SqlDbType.Int).Value = pessoaUpdated.Id;

                sqlConnection.Open();

                sqlCommand.ExecuteScalar();
            }
        }

        public void Delete(int id)
        {
            const string cmdText = "DELETE FROM Pessoa " +
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

        public Pessoa GetByIdWithCarros(int id)
        {
            const string cmdText = 
                "SELECT Pessoa.Id as PessoaId, Pessoa.Nome, Carro.Id as CarroId, Carro.Modelo FROM Pessoa " +
                "LEFT JOIN Carro " +
                "ON Pessoa.Id = Carro.PessoaId " +
                "WHERE Pessoa.Id = @id;";

            using (var sqlConnection = new SqlConnection(_connectionString)) //já faz o close e dispose
            using (var sqlCommand = new SqlCommand(cmdText, sqlConnection)) //já faz o close
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters
                    .Add("@id", SqlDbType.Int).Value = id;

                sqlConnection.Open();

                using (var reader = sqlCommand.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    var pessoaId = reader.GetFieldValue<int>("PessoaId");
                    var pessoa = new Pessoa
                    {
                        Id = pessoaId,
                        Nome = reader.GetFieldValue<string>("Nome"),
                        Carros = new List<Carro>()
                    };

                    if (reader.IsDBNull("CarroId"))
                        return pessoa;

                    pessoa.Carros.Add(
                        new Carro
                        {
                            Id = reader.GetFieldValue<int>("CarroId"),
                            Modelo = reader.GetFieldValue<string>("Modelo"),
                            PessoaId = pessoaId
                        });

                    while (reader.Read())
                    {
                        pessoa.Carros.Add(
                            new Carro
                            {
                                Id = reader.GetFieldValue<int>("CarroId"),
                                Modelo = reader.GetFieldValue<string>("Modelo"),
                                PessoaId = pessoaId
                            });
                    }

                    return pessoa;
                }
            }
        }
    }
}
