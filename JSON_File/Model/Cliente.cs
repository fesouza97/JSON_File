using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_File.Model
{
    internal class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }

        public override string ToString()
        {
            return "ID: " + ID + " | Nome: " + Nome + " | Email: " + Email + " | Idade: " + Idade;
        }
    }
}
