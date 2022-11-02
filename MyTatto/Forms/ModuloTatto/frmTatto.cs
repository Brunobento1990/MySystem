using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MyTatto.Models;
using MyTatto.Repositories;

namespace MyTatto.Forms.ModuloTatto
{
    public partial class frmTatto : Form
    {
        public string caminhoFoto = System.Environment.CurrentDirectory + @"\foto\";
        public Tatto tatto = new Tatto();
        public TattoRepository tattoRepository = new TattoRepository();
        public List<Tatto> tattos = new List<Tatto>();
        public bool Incluir;
        Msg msg = new Msg();
        public frmTatto()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string origem = string.Empty;
            string foto = string.Empty;
            string pastaDestino = string.Empty;
            string destino = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                origem = openFileDialog1.FileName;
                foto = openFileDialog1.SafeFileName;
                destino = caminhoFoto + foto;
            }

            if (File.Exists(destino))
            {
                tatto.Foto = destino;
                pictureBox1.ImageLocation = origem;
            }
            else
            {
                MessageBox.Show("Arquivo não encontrado!");
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            Incluir = true;
            tabControl1.SelectedTab = tabPage2;
            tabControl2.SelectedTab = tabPage3;
            LimparCampos();
            LiberarCampo();
            tatto = new Tatto();
        }
        private void LiberarCampo()
        {
            txtDescricao.Enabled = true;
            txtPreco.Enabled = true;

            btnSair.Enabled = false;
            btnExcluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnIncluir.Enabled = false;
            button1.Enabled = true;
            btnGravar.Enabled = true;
            btnCancelar.Enabled = true;
        }
        private void BloquearCampo()
        {
            txtDescricao.Enabled = false;
            txtPreco.Enabled = false;

            btnSair.Enabled = true;
            btnExcluir.Enabled = true;
            btnAlterar.Enabled = true;
            btnIncluir.Enabled = true;
            button1.Enabled = false;
            btnGravar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            BloquearCampo();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Gravar();
        }
        private void Gravar()
        {
            tatto = new Tatto();
            tatto.Descricao = txtDescricao.Text;
            tatto.Ativo = true;
            tatto.Preco = txtPreco.Value;
            var result = false;
            if (Incluir)
            {
                tatto.DataCadastro = DateTime.Now;
                result = tattoRepository.CreateTatto(tatto);
            }
            else
            {
                tatto.Id_Tatto = Convert.ToInt32(txtCodigo.Text);
                result = tattoRepository.UpdateTatto(tatto);
            }
             
            if (result)
            {
                if (Incluir)
                {
                    tattos.Clear();
                    tattos = tattoRepository.GetAll();
                    dataGridView1.DataSource = tattos;
                    msg.Alerta("Produto cadastrado com sucesso!");
                }
                else
                {
                    tattos.Clear();
                    tattos = tattoRepository.GetAll();
                    dataGridView1.DataSource = tattos;
                    msg.Alerta("produto alterado com sucesso!");
                } 
            }
            else
            {
                msg.Alerta("Ocorreu algum erro a cadastrar o produto!");
            }

            BloquearCampo();
        }

        private void frmTatto_Load(object sender, EventArgs e)
        {
            tattos = tattoRepository.GetAll();
            if (tattos != null)
            {
                dataGridView1.DataSource = tattos;
                var id = tattos.Count();
                CarregarInicio(id);
            }
        }
        private void CarregarInicio(int id)
        {
            var tatto = tattos.LastOrDefault(x => x.Id_Tatto == id);
            if (tatto != null)
            {
                txtCodigo.Text = tatto.Id_Tatto.ToString();
                txtDescricao.Text = tatto.Descricao;
                txtPreco.Value = tatto.Preco;
                txtDataCadastro.Text = tatto.DataCadastro.ToString();

                if (File.Exists(tatto.Foto))
                {
                    pictureBox1.ImageLocation = tatto.Foto;
                }
            }
            
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Incluir = false;
            LiberarCampo();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var resposta = msg.Excluir("Confirma a exclusão da tatto?");
            if (resposta && tatto.Id_Tatto > 0)
            {
                var result = tattoRepository.Excluir(tatto.Id_Tatto);
                if (result)
                {
                    tattos.Remove(tatto);
                    msg.Alerta("Exclusão efetuada com sucesso!");
                }
            }
            else
            {
                msg.Alerta("Não é possível continuar com a exclusão!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int I = dataGridView1.Rows[e.RowIndex].Index;
            var Id = dataGridView1.Rows[I].Cells[0].Value.ToString();
            CarregarInicio(Convert.ToInt32(Id));
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }
        private void LimparCampos()
        {
            txtCodigo.Text = "";
            txtDataCadastro.Text = "";
            txtDescricao.Text = "";
            txtPreco.Value = Convert.ToDecimal(0.00);
        }
    }
}
