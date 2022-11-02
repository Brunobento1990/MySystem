using MyTatto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTatto.ViewModel
{
    public class ClienteViewModel
    {
        public Cliente Cliente { get; set; } = new Cliente();
        public Endereco Endereco { get; set; } = new Endereco();
        public List<Contato> Contatos { get; set; } = new List<Contato>();
    }
}
