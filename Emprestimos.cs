using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Grupo_Sistema_de_Biblioteca_POO
{
    internal class Emprestimos
    {
        public Livros LivroEmprestado { get; set; }
        public Usuarios UsuarioRequerinte { get; set; }
        public DateTime DataEmprestimo { get; set; }

        public Emprestimos(Livros LE, Usuarios UR)
        {
            LivroEmprestado = LE;
            UsuarioRequerinte = UR;
            DataEmprestimo = DateTime.Now;
        }
        public Emprestimos(Livros LE, Usuarios UR, DateTime DT)
        {
            LivroEmprestado = LE;
            UsuarioRequerinte = UR;
            DataEmprestimo = DT;
        }
    }
}
