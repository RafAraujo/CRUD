using Dapper;
using Data.Repositories;
using Data.Utils;
using Domain.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class ClienteRepository : BaseRepository<Cliente>
    {
        protected override ISqlGenerator<Cliente> SqlGenerator { get; set; }

        public ClienteRepository(string connectionString) : base(connectionString)
        {
            SqlGenerator = new SqlGenerator<Cliente>("Cliente");
        }

        public override async Task<IEnumerable<Cliente>> GetAll()
        {
            IEnumerable<Cliente> result = new List<Cliente>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = @"
                        SELECT
                            *
                        FROM
                            Cliente c
                            INNER JOIN Endereco e ON c.Id = e.ClienteId
                            INNER JOIN Uf uf ON e.UfId = uf.Id";

                result = await connection.QueryAsync<Cliente, Endereco, Uf, Cliente>(sql, (cliente, endereco, uf) => {
                    cliente.Endereco = endereco;
                    cliente.Endereco.Uf = uf;
                    return cliente;
                }, splitOn: "Id, Id");
            }

            return result;
        }

        public override async Task<Cliente> GetById(int id)
        {
            Cliente result = null;

            using (var connection = new SqlConnection(ConnectionString))
            {
                var sql = @"
                        SELECT
                            *
                        FROM
                            Cliente c
                            LEFT JOIN Uf ufExp ON c.UfExpedicaoId = ufExp.Id
                            INNER JOIN Endereco e ON c.Id = e.ClienteId
                            INNER JOIN Uf uf ON e.UfId = uf.Id
                        WHERE
                            c.Id = @Id";

                var list = await connection.QueryAsync<Cliente, Uf, Endereco, Uf, Cliente>(sql, (cliente, ufExpedicao, endereco, uf) => {
                    cliente.UfExpedicao = ufExpedicao;
                    cliente.Endereco = endereco;
                    cliente.Endereco.Uf = uf;
                    return cliente;
                }, splitOn: "Id, Id, Id", param: new { Id = id });

                result = list.SingleOrDefault();
            }

            return result;
        }

        public override async Task<Cliente> Insert(Cliente cliente)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {

                    var sql = @"
                            INSERT INTO Cliente (Cpf, Nome, Rg, DataExpedicao, OrgaoExpedicao, UfExpedicaoId, DataNascimento, Sexo, EstadoCivil)
                            VALUES (@Cpf, @Nome, @Rg, @DataExpedicao, @OrgaoExpedicao, @UfExpedicaoId, @DataNascimento, @Sexo, @EstadoCivil)

                            SELECT CAST(SCOPE_IDENTITY() AS INT)";

                    cliente.Id = await connection.QuerySingleAsync<int>(sql, cliente, transaction);

                    sql = @"
                            INSERT INTO Endereco (ClienteId, Cep, Logradouro, Numero, Complemento, Bairro, Cidade, UfId)
                            VALUES (@ClienteId, @Cep, @Logradouro, @Numero, @Complemento, @Bairro, @Cidade, @UfId)

                            SELECT CAST(SCOPE_IDENTITY() AS INT)";

                    var endereco = cliente.Endereco;
                    endereco.Id = await connection.QuerySingleAsync<int>(sql, new { ClienteId = cliente.Id, endereco.Cep, endereco.Logradouro, endereco.Numero, endereco.Complemento, endereco.Bairro, endereco.Cidade, endereco.UfId }, transaction);

                    transaction.Commit();
                }
            }

            return cliente;
        }

        public override async Task<Cliente> Update(Cliente cliente)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {

                    var sql = @"
                            UPDATE Cliente SET
                                Cpf = @Cpf,
                                Nome = @Nome,
                                Rg = @Rg,
                                DataExpedicao = @DataExpedicao,
                                OrgaoExpedicao = @OrgaoExpedicao,
                                UfExpedicaoId = @UfExpedicaoId,
                                DataNascimento = @DataNascimento,
                                Sexo = @Sexo,
                                EstadoCivil = @EstadoCivil
                            WHERE
                                Id = @Id";

                    await connection.ExecuteAsync(sql, cliente, transaction);

                    sql = @"
                            UPDATE Endereco SET
                                Cep = @Cep,
                                Logradouro = @Logradouro,
                                Numero = @Numero,
                                Complemento = @Complemento,
                                Bairro = @Bairro,
                                Cidade = @Cidade,
                                UfId = @UfId
                            WHERE
                                ClienteId = @ClienteId";

                    await connection.ExecuteAsync(sql, cliente.Endereco, transaction);

                    transaction.Commit();
                }
            }

            return cliente;
        }

        public override async Task Remove(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {

                    var sql = @"DELETE FROM Endereco WHERE ClienteId = @Id";
                    await connection.ExecuteAsync(sql, new { Id = id }, transaction);

                    sql = @"DELETE FROM Cliente WHERE Id = @Id";
                    connection.Execute(sql, new { Id = id }, transaction);

                    transaction.Commit();
                }
            }
        }
    }
}
