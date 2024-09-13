using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Grupo_Sistema_de_Biblioteca_POO
{
	internal class Punicoes
	{
		#region Atributos

		private Usuarios UsuarioRequerinte { get; set; }
		private DateTime DataLimite { get; set; }

		#endregion

		#region Builder

		public Punicoes(Usuarios UR, TimeSpan Dias)
		{
			UsuarioRequerinte = UR;
			DataLimite = DateTime.Now + Dias + Dias;
            Console.WriteLine("");
			Console.WriteLine($"Devido ao atraso, o usuário {UR.Ft_GetNome()} ficará suspenso até {(Ft_GetDataLimite().Day > 9 ? Ft_GetDataLimite().Day : "0" + Ft_GetDataLimite().Day)}/{(Ft_GetDataLimite().Month > 9 ? Ft_GetDataLimite().Month : "0" + Ft_GetDataLimite().Month)}/{Ft_GetDataLimite().Year,-23}");
        }

		public Punicoes(Usuarios UR, DateTime DT)
		{
			UsuarioRequerinte = UR;
			DataLimite = DT;
		}

		#endregion

		#region Getters

		public Usuarios Ft_GetUsuario()
		{
			return UsuarioRequerinte;
		}

		public DateTime Ft_GetDataLimite()
		{
			return DataLimite;
		}

		#endregion

		#region ExibirInfo

		public void Ft_ExibirInformacoes()
		{
            Console.WriteLine($"O usuário {UsuarioRequerinte.Ft_GetNome()} se encontra suspenso até {(Ft_GetDataLimite().Day > 9 ? Ft_GetDataLimite().Day : "0" + Ft_GetDataLimite().Day)}/{(Ft_GetDataLimite().Month > 9 ? Ft_GetDataLimite().Month : "0" + Ft_GetDataLimite().Month)}/{Ft_GetDataLimite().Year,-23}");
            Console.WriteLine("");
		}
		
		#endregion
	}
}
