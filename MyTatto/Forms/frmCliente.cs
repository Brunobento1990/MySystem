using MyTatto.Models;
using MyTatto.Repositories;
using MyTatto.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTatto.Forms
{
    public partial class frmCliente : Form
    {
        //Objeto completo
        public ClienteViewModel ClienteView = new ClienteViewModel();
        //Lista de todos os clientes
        List<Cliente> clientes = new List<Cliente>();
        // Repositórios
        ClienteRepository clienteRepository = new ClienteRepository();
        EnderecoRepository enderecoRepository = new EnderecoRepository();
        ContatoRepository contatoRepository = new ContatoRepository();
        //Verifica se o usuário está incluindo ou alterando
        bool Incluir = false;

        Msg msg = new Msg();
        //Lista de contatos caso o usuário esteja alterando
        List<Contato> contatosAlterar = new List<Contato>();

        public frmCliente()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            clientes = clienteRepository.GetAll();
            if (clientes != null)
            {
                dataGridView1.DataSource = clientes;
                var Id = clientes.Count();
                CarregarCliente(clientes,Id);
            }
        }
        private void LiberarCampos()
        {
            txtNome.Enabled = true;
            txtCpf.Enabled = true;
            txtDataNascimento.Enabled = true;
            txtRg.Enabled = true;
            txtObservacoes.Enabled = true;
            txtCep.Enabled = true;
            btnCancelar.Enabled = true;
            btnGravar.Enabled = true;
            btnIncluirContato.Enabled = true;
            btnpesquisar.Enabled = true;

            btnSair.Enabled = false;
            btnIncluir.Enabled = false;
            btnExcluir.Enabled = false;
            btnAlterar.Enabled = false;

        }
        private void BloquearCampos()
        {
            txtNome.Enabled = false;
            txtCpf.Enabled = false;
            txtDataNascimento.Enabled = false;
            txtRg.Enabled = false;
            txtObservacoes.Enabled = false;
            txtCep.Enabled = false;
            btnCancelar.Enabled = false;
            btnGravar.Enabled = false;
            btnIncluirContato.Enabled = false;
            btnpesquisar.Enabled = false;
            txtCidade.Enabled = false;
            txtBairro.Enabled = false;
            txtUf.Enabled = false;
            txtRua.Enabled = false;
            txtnumero.Enabled = false;
            txtComplemento.Enabled = false;

            btnSair.Enabled = true;
            btnIncluir.Enabled = true;
            btnExcluir.Enabled = true;
            btnAlterar.Enabled = true;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            LiberarCampos();
            LimparCampos();
            tabControl1.SelectedTab = tabPage2;
            Incluir = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            BloquearCampos();
            tabControl1.SelectedTab = tabPage1;
        }
        private void Gravar()
        {
            if (Incluir)
            {
                ClienteView.Cliente = new Cliente();
                ClienteView.Cliente.Nome = txtNome.Text;
                ClienteView.Cliente.Cpf = txtCpf.Text;
                ClienteView.Cliente.DataNascimento = txtDataNascimento.Text;
                ClienteView.Cliente.Rg = txtRg.Text;
                ClienteView.Cliente.Obs = txtObservacoes.Text;
                ClienteView.Cliente.Ativo = true;
                ClienteView.Cliente.UltimaCompra = (DateTime)SqlDateTime.MinValue;
                ClienteView.Cliente.UltimaTatto = (DateTime)SqlDateTime.MinValue;
                ClienteView.Cliente.DataCadastro = DateTime.Now;

                var result = clienteRepository.CreateCliente(ClienteView.Cliente);

                if (result != null)
                {
                    ClienteView.Endereco = new Endereco();
                    ClienteView.Endereco.Cep = txtCep.Text;
                    ClienteView.Endereco.Ativo = true;
                    ClienteView.Endereco.Bairro = txtBairro.Text;
                    ClienteView.Endereco.Cidade = txtCidade.Text;
                    ClienteView.Endereco.Complemento = txtComplemento.Text;
                    ClienteView.Endereco.Rua = txtRua.Text;
                    ClienteView.Endereco.Numero = txtnumero.Text;
                    ClienteView.Endereco.UF = txtUf.Text;

                    var resultEndereco = enderecoRepository.CreateEndereco(ClienteView.Endereco, result.Id_Cliente);

                    if (resultEndereco)
                    {
                        if(ClienteView.Contatos.Count() > 0)
                        {
                            foreach (Contato c in ClienteView.Contatos)
                            {
                                c.Id_Cliente = result.Id_Cliente;
                            }
                            contatoRepository.CreateContato(ClienteView.Contatos);
                            dataGridView2.DataSource = ClienteView.Contatos;
                        }
                        clientes.Clear();
                        clientes = clienteRepository.GetAll();
                        if (clientes != null)
                        {
                            dataGridView1.DataSource = clientes;
                            var Id = clientes.Count();
                            CarregarCliente(clientes, Id);
                            dataGridView2.DataSource = ClienteView.Contatos;
                        }
                        var message = "Cliente cadastrado com sucesso!";
                        msg.Alerta(message);
                    }
                    else
                    {
                        var error = "Erro ao cadastrar o endereço!";
                        msg.Alerta(error);
                    }
                    
                }
                else
                {
                    var error = "Cadastro não finalizado!";
                    msg.Alerta(error);
                }
            }
            else
            {
                dataGridView2.Enabled = false;

                ClienteView.Cliente.Nome = txtNome.Text;
                ClienteView.Cliente.Cpf = txtCpf.Text;
                ClienteView.Cliente.DataNascimento = txtDataNascimento.Text;
                ClienteView.Cliente.Rg = txtRg.Text;
                ClienteView.Cliente.Obs = txtObservacoes.Text;
                ClienteView.Cliente.Ativo = true;

                var rAlterar = clienteRepository.UpdateCliente(ClienteView.Cliente);
                if (rAlterar)
                {
                    ClienteView.Endereco.Cep = txtCep.Text;
                    ClienteView.Endereco.Ativo = true;
                    ClienteView.Endereco.Bairro = txtBairro.Text;
                    ClienteView.Endereco.Cidade = txtCidade.Text;
                    ClienteView.Endereco.Complemento = txtComplemento.Text;
                    ClienteView.Endereco.Rua = txtRua.Text;
                    ClienteView.Endereco.Numero = txtnumero.Text;
                    ClienteView.Endereco.UF = txtUf.Text;

                    rAlterar = enderecoRepository.UpdateEndereco(ClienteView.Endereco);

                    if (rAlterar)
                    {
                        if (contatosAlterar.Count() > 0)
                        {
                            foreach (Contato c in contatosAlterar)
                            {
                                c.Id_Cliente = ClienteView.Cliente.Id_Cliente;
                            }
                            contatoRepository.CreateContato(contatosAlterar);
                        }
                        msg.Alerta("Cadastro do cliente alterado com sucesso!");
                        clientes.Clear();
                        clientes = clienteRepository.GetAll();
                        dataGridView1.DataSource = clientes;
                    }
                    else
                    {
                        msg.Alerta("Ocorreu um erro ao atualizar o endereço do cliente!");
                    }
                }
                else
                {
                    msg.Alerta("Ocorreu um erro ao atualizar o cliente!");
                }
            }
            
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Gravar();
            BloquearCampos();
        }

        private void clienteBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void btnpesquisar_Click(object sender, EventArgs e)
        {
            var strCep = string.Format("https://viacep.com.br/ws/{0}/json/", txtCep.Text);
            if (string.IsNullOrWhiteSpace(txtCep.Text))
            {
                string texto = "Senhor usuário, informe um CEP Válido.";
                msg.Alerta(texto);
                return;
            }
            else
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var respost = client.GetAsync(strCep).Result;
                        if (respost.IsSuccessStatusCode)
                        {
                            var resultado = respost.Content.ReadAsStringAsync().Result;
                            EnderecoViaCep endereco = JsonConvert.DeserializeObject<EnderecoViaCep>(resultado);
                            txtCidade.Text = endereco.Localidade;
                            txtBairro.Text = endereco.Bairro;
                            txtUf.Text = endereco.Uf;
                            txtRua.Text = endereco.Logradouro;
                            txtComplemento.Text = endereco.Complemento;
                            txtCidade.Enabled = true;
                            txtBairro.Enabled = true;
                            txtUf.Enabled = true;
                            txtRua.Enabled = true;
                            txtnumero.Enabled = true;
                            txtComplemento.Enabled = true;
                        }
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Sr usuário, não foi encontrado o Cep informado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void btnIncluirContato_Click(object sender, EventArgs e)
        {
            if (Incluir)
            {
                var frmContato = new frmContato();
                frmContato.ShowDialog();
                ClienteView.Contatos = frmContato.contatosAdd;
                if (ClienteView.Contatos.Count() > 0)
                {
                    dataGridView2.DataSource = ClienteView.Contatos;
                }
                
            }
            else
            {
                var frmContato = new frmContato();
                frmContato.ShowDialog();
                contatosAlterar.Clear();
                contatosAlterar = frmContato.contatosAdd;
                
            }
            
        }
        private void CarregarCliente(List<Cliente> cliente,int id)
        {
            ClienteView.Cliente = cliente.LastOrDefault(x => x.Id_Cliente == id);
            if (ClienteView.Cliente != null)
            {
                ClienteView.Endereco = enderecoRepository.GetEndereco(ClienteView.Cliente.Id_Cliente);
                ClienteView.Contatos = contatoRepository.GetAll(ClienteView.Cliente.Id_Cliente);
                dataGridView2.DataSource = ClienteView.Contatos;

                txtCodigo.Text = ClienteView.Cliente.Id_Cliente.ToString();
                txtNome.Text = ClienteView.Cliente.Nome;
                txtCpf.Text = ClienteView.Cliente.Cpf;
                txtRg.Text = ClienteView.Cliente.Rg;
                txtObservacoes.Text = ClienteView.Cliente.Obs;
                txtDataNascimento.Text = ClienteView.Cliente.DataNascimento;
                if (ClienteView.Cliente.UltimaCompra > DateTime.MinValue)
                {
                    txtUltimaCompra.Text = ClienteView.Cliente.UltimaCompra.ToString();
                }
                if (ClienteView.Cliente.UltimaTatto > DateTime.MinValue)
                {
                    txtUltimatatto.Text = ClienteView.Cliente.UltimaTatto.ToString();
                }
                txtDataCadastro.Text = ClienteView.Cliente.DataCadastro.ToString();

                //Endereço
                if (ClienteView.Endereco != null)
                {
                    txtCep.Text = ClienteView.Endereco.Cep;
                    txtRua.Text = ClienteView.Endereco.Rua;
                    txtnumero.Text = ClienteView.Endereco.Numero;
                    txtCidade.Text = ClienteView.Endereco.Cidade;
                    txtBairro.Text = ClienteView.Endereco.Bairro;
                    txtUf.Text = ClienteView.Endereco.UF;
                    txtComplemento.Text = ClienteView.Endereco.Complemento;
                }
                if (ClienteView.Contatos.Count() > 0)
                {
                    dataGridView2.DataSource = ClienteView.Contatos;
                }
            }

            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int I = dataGridView1.Rows[e.RowIndex].Index;
            var Id = dataGridView1.Rows[I].Cells[0].Value.ToString();
            CarregarCliente(clientes, Convert.ToInt32(Id));
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
           
            LiberarCampos();
            tabControl1.SelectedTab = tabPage2;
            Incluir = false;
            dataGridView2.Enabled = true;
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var contato = new Contato();
            contato.Id_Contato = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
            contato.Ativo = Convert.ToBoolean(dataGridView2.Rows[e.RowIndex].Cells[1].Value);
            contato.Email = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            contato.Telefone = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            contato.Id_Cliente = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[4].Value);

            var frmContatoAlterar = new frmContatoEditar(contato);
            frmContatoAlterar.ShowDialog();
            if (frmContatoAlterar.Excluiu)
            {
                ClienteView.Contatos.Remove(contato);
                dataGridView2.DataSource = ClienteView.Contatos;
            }
        }
        private void LimparCampos()
        {
            txtCodigo.Text = "";
            txtNome.Text = "";
            txtCpf.Text = "";
            txtRg.Text = "";
            txtDataNascimento.Text = "";
            txtDataCadastro.Text = "";
            txtUltimaCompra.Text = "";
            txtUltimatatto.Text = "";
            txtObservacoes.Text = "";

            txtCep.Text = "";
            txtRua.Text = "";
            txtnumero.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtUf.Text = "";
            txtComplemento.Text = "";

            dataGridView2.DataSource = null;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            var pergunta = "Confirma a exclusão do contato?";
            var resposta = msg.Excluir(pergunta);
            if (resposta)
            {
                ClienteView.Cliente.Ativo = false;
                var result = clienteRepository.DeleteCliente(ClienteView.Cliente);
                if (result)
                {
                    ClienteView.Endereco.Ativo = false;
                    result = enderecoRepository.DeleteEndereco(ClienteView.Endereco);
                    if (result)
                    {
                        foreach (Contato c in ClienteView.Contatos)
                        {
                            c.Ativo = false;
                            result = contatoRepository.DeleteContato(c);
                        }
                        if (result)
                        {
                            msg.Alerta("Exclusão concluída com sucesso!");
                            clientes.Remove(ClienteView.Cliente);
                            LimparCampos();
                            tabControl1.SelectedTab = tabPage1;
                        }
                        else
                        {
                            msg.Alerta("Ocorreu um erro ao excluir os contatos do cliente!");
                        }
                    }
                    else
                    {
                        msg.Alerta("Ocorreu um erro ao excluir o endereço do cliente!");
                    }
                }
                else
                {
                    msg.Alerta("Ocorreu um erro ao excluir o contato!");
                }
            }
        }

        private void btnPesquisarDataGrid_Click(object sender, EventArgs e)
        {
            if (txtPesquisa.Text == "") dataGridView1.DataSource = clientes;
            if (cboPesquisa.SelectedItem.ToString() == "Nome")
            {
                var clientesPesquisa = clientes.Where(x => x.Nome.Contains(txtPesquisa.Text)).ToList();
                dataGridView1.DataSource = clientesPesquisa;
            }
            if (cboPesquisa.SelectedItem.ToString() == "CPF")
            {
                var clientesPesquisa = clientes.Where(x => x.Cpf.Contains(txtPesquisa.Text)).ToList();
                dataGridView1.DataSource = clientesPesquisa;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
