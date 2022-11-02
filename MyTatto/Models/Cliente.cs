using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTatto.Models
{
	public class Cliente
	{
		public int Id_Cliente { get; set; }
		public bool Ativo { get; set; }
		public string Nome { get; set; }
		public string Cpf { get; set; }
		public string DataNascimento { get; set; }
		public string Rg { get; set; }
		public string Obs { get; set; }
		public DateTime DataCadastro { get; set; }
		public DateTime UltimaCompra { get; set; }
		public DateTime UltimaTatto { get; set; }
	}
}
