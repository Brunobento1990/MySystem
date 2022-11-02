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
    public class ContatoRepository
    {
        public string _connection = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MyTatto;Data Source=" + Environment.MachineName;
        public SqlConnection ConexaoBanco
        {
            get
            {
                return new SqlConnection(_connection);
            }
        }
        public bool CreateContato(List<Contato> contatos)
        {
            var result = false;

            try
            {
                using (ConexaoBanco)
                {
                    var query = "insert into contato (Ativo,Email,Telefone,Id_Cliente) values (@ativo,@email,@telefone,@id_Cliente)";
                    foreach (Contato c in contatos)
                    {
                        var parameters = new
                        {
                            c.Ativo,
                            c.Email,
                            c.Telefone,
                            c.Id_Cliente
                        };
                        ConexaoBanco.Query(query,parameters);
                        result = true;
                    }
                }
            }
            catch (SqlException e)
            {
                result = false;
            }

            return result;
        }
        public bool UpdateContato(List<Contato> contatos)
        {
            var result = false;

            try
            {
                using (ConexaoBanco)
                {
                    var query = "update Contato Set Ativo = @ativo,Email = @email,Telefone = @telefone where Id_Contato = @id_Contato";
                    foreach (Contato c in contatos)
                    {
                        var parameters = new
                        {
                            c.Ativo,
                            c.Email,
                            c.Telefone,
                            c.Id_Contato
                        };
                        ConexaoBanco.Query(query,parameters);
                        result = true;
                    }
                }
            }
            catch (SqlException e)
            {
                result = false;
            }

            return result;
        }
        public bool UpdateContatoUnico(Contato contato)
        {
            var result = false;

            try
            {
                using (ConexaoBanco)
                {
                    var query = "update Contato set Email = @email,Telefone = @telefone where Id_Contato = @id_Contato ";
                    var parameters = new
                    {
                        contato.Email,
                        contato.Telefone,
                        contato.Id_Contato
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
        public List<Contato> GetAll(int id_Cliente)
        {
            var contatos = new List<Contato>();

            try
            {
                using (ConexaoBanco)
                {
                    var query = "select * from Contato where Id_Cliente = @id_Cliente and Ativo = 1";
                    var parameters = new { id_Cliente };
                    contatos = ConexaoBanco.Query<Contato>(query,parameters).ToList();
                }
            }
            catch (SqlException e)
            {
                contatos = null;
            }

            return contatos;
        }
        public bool DeleteContato(Contato contato)
        {
            var result = false;

            try
            {
                using (ConexaoBanco)
                {
                    var query = "update Contato set Ativo = @ativo where Id_Contato = @id_Contato";
                    var parameters = new
                    {
                        contato.Ativo,
                        contato.Id_Contato
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
