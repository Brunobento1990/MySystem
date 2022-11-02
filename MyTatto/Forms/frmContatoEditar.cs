using MyTatto.Models;
using MyTatto.Repositories;
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
    public partial class frmContatoEditar : Form
    {
        public Contato contatoAlterar = new Contato();
        ContatoRepository contatoRepository = new ContatoRepository();
        Msg msg = new Msg();
        public bool Excluiu = false;
        public frmContatoEditar(Contato contato)
        {
            contatoAlterar = contato;
            InitializeComponent();
            txtCodigo.Text = contatoAlterar.Id_Contato.ToString();
            txtEmail.Text = contatoAlterar.Email;
            txtTelefone.Text = contatoAlterar.Telefone;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmContatoEditar_Load(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var pergunta = "Confirma a exclusão do contato?";
            var confirma = msg.Excluir(pergunta);
            if (confirma)
            {
                contatoAlterar.Ativo = false;
                var retorno = contatoRepository.DeleteContato(contatoAlterar);
                if (retorno)
                {
                    Excluiu = true;
                    Close();
                }
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Excluiu = false;
            contatoAlterar.Email = txtEmail.Text;
            contatoAlterar.Telefone = txtTelefone.Text;
            var result = contatoRepository.UpdateContatoUnico(contatoAlterar);
            if (result)
            {
                Close();
            }
        }
    }
}
