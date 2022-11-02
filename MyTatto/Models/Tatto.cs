using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTatto.Models
{
    public class Tatto
    {
        public int Id_Tatto { get; set; }
        public bool Ativo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Foto { get; set; }
    }
}
