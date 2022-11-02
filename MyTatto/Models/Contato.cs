using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTatto.Models
{
    public class Contato
    {
        public int Id_Contato { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int Id_Cliente { get; set; }
    }
}
