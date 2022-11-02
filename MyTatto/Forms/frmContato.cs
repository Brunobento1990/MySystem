using MyTatto.Models;
using MyTatto.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTatto.Forms
{
    public partial class frmContato : Form
    {
        public static List<Contato> contatosAdd = new List<Contato>();
        
        public frmContato()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Contato_Load(object sender, EventArgs e)
        {
            
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            Contato contato = new Contato();
            contato.Ativo = true;
            contato.Email = txtEmail.Text;
            contato.Telefone = txtTelefone.Text;
            Add(contato);
            txtEmail.Text = "";
            txtTelefone.Text = "";
        }
        private void Add(Contato contato)
        {
            contatosAdd.Add(contato);
        }
    }
}
