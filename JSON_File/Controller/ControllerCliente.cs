using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JSON_File.Model;

namespace JSON_File.Controller
{
    internal class ControllerCliente
    {
        private string arquivoCaminho = "clientes.json";
        private List<Cliente> clientes = new List<Cliente>();

        public List<Cliente> CarregarDados()
        {
            if(File.Exists(arquivoCaminho))
            {
                string json = File.ReadAllText(arquivoCaminho);
                clientes = JsonSerializer.Deserialize<List<Cliente>>(json) 
                    ?? new List<Cliente>();
            }
            return clientes;
        }

        public void CadastrarCliente(string nome, string email, int idade)
        {
            int novoId = clientes.Count > 0 ? clientes[clientes.Count-1].ID+1 : 0;
            clientes.Add(new Cliente
            {
                ID = novoId,
                Nome = nome,
                Email = email,
                Idade = idade
            });
            SalvarDados();
        }

        private void SalvarDados()
        {
            string json = JsonSerializer.Serialize(clientes);
            File.WriteAllText(arquivoCaminho, json);
        }

        public void AtualizarCliente(int id, string novoNome, string novoEmail, int novaIdade)
        {
            Cliente clienteExistente = clientes.FirstOrDefault(c => c.ID == id);
            if (clienteExistente != null)
            {
                clienteExistente.Nome = novoNome;
                clienteExistente.Email = novoEmail;
                clienteExistente.Idade = novaIdade;
                SalvarDados();
            }
        }

        public void ExcluirCliente(int id)
        {
            Cliente clienteParaRemover = clientes.FirstOrDefault(c => c.ID == id);
            if (clienteParaRemover != null)
            {
                clientes.Remove(clienteParaRemover);
                SalvarDados();
            }
        }
    }
}
