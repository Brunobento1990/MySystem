using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTatto.Repositories
{
    public class dbConnection
    {
        public string _connection = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MyPromo21;Data Source="+ Environment.MachineName;
        public SqlConnection ConexaoBanco
        {
            get
            {
                return new SqlConnection(_connection);
            }
        }
    }
}
