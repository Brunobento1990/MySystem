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
    public class EnderecoRepository
    {
        public string _connection = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MyTatto;Data Source=" + Environment.MachineName;
        public SqlConnection ConexaoBanco
        {
            get
            {
                return new SqlConnection(_connection);
            }
        }

        public bool CreateEndereco(Endereco endereco,int id_Cliente)
        {
            var result = false;

            try
            {
                using (ConexaoBanco)
                {
                    var query = "insert into Endereco (Ativo,Cep,Rua,Numero,Complemento,Bairro,Cidade,Id_Cliente,UF) Values(@ativo,@cep,@rua,@numero,@complemento,@bairro,@cidade,@id_Cliente,@uf)";
                    var parameters = new 
                    {
                        endereco.Ativo,
                        endereco.Cep,
                        endereco.Rua,
                        endereco.Numero,
                        endereco.Complemento,
                        endereco.Bairro,
                        endereco.Cidade,
                        endereco.UF,
                        id_Cliente
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
        public bool UpdateEndereco(Endereco endereco)
        {
            var result = false;

            try
            {
                using (ConexaoBanco)
                {
                    var query = "update Endereco Set Ativo = @ativo,Cep = @cep,Rua = @rua,Numero = @numero,Complemento = @complemento,Bairro = @bairro,Cidade = @cidade,UF = @uf where Id_Endereco = @id_Endereco";
                    var parameters = new
                    {
                        endereco.Ativo,
                        endereco.Cep,
                        endereco.Rua,
                        endereco.Numero,
                        endereco.Complemento,
                        endereco.Bairro,
                        endereco.Cidade,
                        endereco.UF,
                        endereco.Id_Endereco
                        
                    };
                    ConexaoBanco.Query(query,parameters);
                    result = true;
                }
            }
            catch (SqlException e)
            {
                
            }

            return result;
        }

        public Endereco GetEndereco(int id_Cliente)
        {
            var endereco = new Endereco();

            try
            {
                using (ConexaoBanco)
                {
                    var query = "Select * from Endereco where Id_Cliente = @id_Cliente";
                    var parameters = new { id_Cliente };
                    endereco = ConexaoBanco.QueryFirstOrDefault<Endereco>(query,parameters);
                }
            }
            catch (SqlException e)
            {
                endereco = null;
            }

            return endereco;
        }
        public bool DeleteEndereco(Endereco endereco)
        {
            var result = false;

            try
            {
                using (ConexaoBanco)
                {
                    var query = "update Endereco set Ativo = @ativo where Id_Endereco = @id_Endereco";
                    var parameters = new
                    {
                        endereco.Ativo,
                        endereco.Id_Endereco
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
