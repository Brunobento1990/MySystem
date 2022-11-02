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
    public class TattoRepository
    {
        public string _connection = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MyTatto;Data Source=" + Environment.MachineName;
        public SqlConnection ConexaoBanco
        {
            get
            {
                return new SqlConnection(_connection);
            }
        }
        public bool CreateTatto(Tatto tatto)
        {
            var result = false;

            try
            {
                using (ConexaoBanco)
                {
                    var query = "insert into Tatto (Ativo,Descricao,Preco,DataCadastro,Foto) values (@ativo,@descricao,@preco,@dataCadastro,@foto)";
                    var parameters = new
                    {
                        tatto.Ativo,
                        tatto.Descricao,
                        tatto.Preco,
                        tatto.DataCadastro,
                        tatto.Foto
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
        public List<Tatto> GetAll()
        {
            var tattos = new List<Tatto>();

            try
            {
                using (ConexaoBanco)
                {
                    var query = "select * from Tatto where Ativo = 1 ORDER BY Id_Tatto DESC";

                    tattos = ConexaoBanco.Query<Tatto>(query).ToList();
                }
            }
            catch (SqlException e)
            {
                tattos = null;
            }

            return tattos;
        }
        public Tatto GetById(int id_Tatto)
        {
            var tatto = new Tatto();

            try
            {
                using (ConexaoBanco)
                {
                    var query = "Select * from tatto where Ativo = 1";
                    tatto = ConexaoBanco.QueryFirstOrDefault<Tatto>(query);
                }
            }
            catch (SqlException e)
            {
                tatto = null;
            }

            return tatto;
        }
        public bool UpdateTatto(Tatto tatto)
        {
            var result = false;
            try
            {
                using (ConexaoBanco)
                {
                    var query = "update Tatto set Ativo = @ativo,Descricao = @descricao,Preco = @preco,Foto = @foto where Id_Tatto = @id_Tatto";
                    var parameters = new
                    {
                        tatto.Ativo,
                        tatto.Descricao,
                        tatto.Preco,
                        tatto.Foto,
                        tatto.Id_Tatto
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
        public bool Excluir(int id_Tatto)
        {
            var result = false;

            try
            {
                using (ConexaoBanco)
                {
                    var query = "update Tatto set Ativo = 0 where Id_Tatto = @id_Tatto";
                    var parameters = new { id_Tatto };
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
