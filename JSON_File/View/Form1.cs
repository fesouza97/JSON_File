using JSON_File.Controller;
using JSON_File.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace JSON_File
{
    public partial class Form1 : Form
    {
        private ControllerCliente cc = new ControllerCliente();
        public Form1()
        {
            InitializeComponent();
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            List<Cliente> clientes = cc.CarregarDados();
            list_cliente.Items.Clear();
            foreach (Cliente cliente in clientes)
            {
                list_cliente.Items.Add(cliente);
            }
        }

        private void LimparCampos()
        {
            txt_nome.Clear();
            txt_email.Clear();
            txt_idade.Clear();
            list_cliente.ClearSelected();
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_nome.Text) 
                || string.IsNullOrWhiteSpace(txt_email.Text) 
                || string.IsNullOrWhiteSpace(txt_idade.Text))
            {
                MessageBox.Show("Todos os campos devem ser preenchidos!");
            }   

            string nome = txt_nome.Text;
            string email = txt_email.Text;
            int idade = int.Parse(txt_idade.Text);
            cc.CadastrarCliente(nome, email, idade);
            AtualizarLista();
            LimparCampos();
            MessageBox.Show("Cliente adicionado com sucesso!");

        }

        private Cliente ObterClienteSelecionado()
        {
            return list_cliente.SelectedItem as Cliente;
        }

        private void list_cliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cliente clienteSelecionado = ObterClienteSelecionado();
            if (clienteSelecionado != null)
            {
                txt_nome.Text = clienteSelecionado.Nome;
                txt_email.Text = clienteSelecionado.Email;
                txt_idade.Text = clienteSelecionado.Idade.ToString();
            }
        }

        private void btn_atualizar_Click(object sender, EventArgs e)
        {
            Cliente clienteSelecionado = ObterClienteSelecionado();
            if (clienteSelecionado != null)
            {
                string nome = string.IsNullOrWhiteSpace(txt_nome.Text) ? clienteSelecionado.Nome : txt_nome.Text;
                string email = string.IsNullOrWhiteSpace(txt_email.Text) ? clienteSelecionado.Email : txt_email.Text;

                if (!int.TryParse(txt_idade.Text, out int idade) || idade <= 0)
                {
                    MessageBox.Show("Idade inválida! Insira um número positivo.");
                    return;
                }

                cc.AtualizarCliente(clienteSelecionado.ID, nome, email, idade);
                AtualizarLista();
                LimparCampos();
                MessageBox.Show("Cliente atualizado com sucesso!");
            }
            else
            {
                MessageBox.Show("Selecione um cliente para atualizar.");
            }
        }

        private void btn_deletar_Click(object sender, EventArgs e)
        {
            Cliente clienteSelecionado = ObterClienteSelecionado();
            if (clienteSelecionado != null)
            {
                cc.ExcluirCliente(clienteSelecionado.ID);
                AtualizarLista();
            }
            else
            {
                MessageBox.Show("Selecione um cliente para deletar.");
            }
        }

        private async void Buscar()
        {
            string url = "https://fakestoreapi.com/users";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    List<User> usuarios = JsonSerializer.Deserialize<List<User>>(json);
                    dgv_usuarios.DataSource = usuarios.Select(u => new
                    {
                        u.id,
                        Nome = u.name.firstname + " " + u.name.lastname,
                        u.email,
                        u.username,
                        u.password,
                        u.phone,
                        Cidade = u.address.city
                    }).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar dados: " + ex.Message);
                }
            }
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
