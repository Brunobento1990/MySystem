using Dapper;
using MyTatto.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyTatto.Repositories
{
    public class ClienteRepository
    {
        public string _connection = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MyTatto;Data Source=" + Environment.MachineName;
        public SqlConnection ConexaoBanco
        {
            get
            {
                return new SqlConnection(_connection);
            }
        }

        public Cliente CreateCliente(Cliente cliente)
        {
            var result = new Cliente();

            try
            {
                using (ConexaoBanco)
                {
                    var query = "insert into cliente (Ativo,Nome,Cpf,DataNascimento,Rg,Obs,DataCadastro) Values (@ativo,@nome,@cpf,@dataNascimento,@rg,@obs,@dataCadastro)";
                    var parameters = new
                    {
                        cliente.Ativo,
                        cliente.Nome,
                        cliente.Cpf,
                        cliente.DataNascimento,
                        cliente.Rg,
                        cliente.Obs,
                        cliente.DataCadastro,
                    };
                    ConexaoBanco.Query(query, parameters);
                    var query1 = "select * from cliente ORDER BY Id_Cliente DESC";
                    result = ConexaoBanco.QueryFirstOrDefault<Cliente>(query1);
                }
            }
            catch (SqlException e)
            {
                result = null;
            }

            return result;
        }
        public bool UpdateCliente(Cliente cliente)
        {
            var result = false;

            try
            {
                using (ConexaoBanco)
                {
                    var query = "update Cliente set Ativo = @ativo,Nome = @nome,Cpf = @cpf,DataNascimento = @dataNascimento,Rg = @rg,Obs = @obs where Id_Cliente = @id_cliente";
                    var parameters = new 
                    { 
                        cliente.Ativo,
                        cliente.Nome,
                        cliente.Cpf,
                        cliente.DataNascimento,
                        cliente.Rg,
                        cliente.Obs,
                        cliente.Id_Cliente
                    };
                    ConexaoBanco.Query(query,parameters);
                    result = true;
                }
            }
            catch (SqlException e)
            {
                result = false;
            }

            return result;
        }
        public bool AtualizaUltimaCompra(DateTime ultimaCompra, int id_cliente)
        {
            var result = false;

            try
            {
                using (ConexaoBanco)
                {
                    var query = "update cliente set UltimaCompra = @ultimaCompra where Id_Cliente = @id_Cliente";
                    var parameters = new {ultimaCompra,id_cliente};
                    ConexaoBanco.Query(query,parameters);
                    result = true;
                }
            }
            catch (SqlException e)
            {
                result = false;
            }

            return result;
        }
        public List<Cliente> GetAll()
        {
            var clientes = new List<Cliente>();

            try
            {
                using (ConexaoBanco)
                {
                    var query = "select * from Cliente where Ativo = 1 ORDER BY Id_Cliente DESC";
                    clientes = ConexaoBanco.Query<Cliente>(query).ToList();
                }
            }
            catch (SqlException e)
            {
                clientes = null;
            }

            return clientes;
        }
        public bool DeleteCliente(Cliente cliente)
        {
            var result = false;

            try
            {
                using (ConexaoBanco)
                {
                    var query = "update Cliente set Ativo = @ativo where Id_Cliente = @id_Cliente";
                    var parameters = new
                    {
                        cliente.Ativo,
                        cliente.Id_Cliente
                    };
                    ConexaoBanco.Query(query,parameters);
                    result = true;
                }
            }
            catch (SqlException e)
            {
                result = false;
            }

            return result;
        }
       
    }
}
