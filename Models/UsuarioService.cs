using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public List<Usuario> Listar()
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.usuarios.ToList();
            }
        }
        public Usuario Listar(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.usuarios.Find(id);
            }
        }
        public void incluirUsuario(Usuario novoUser)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Add(novoUser);
                bc.SaveChanges();
            }
        }

        public void editarUsuario(Usuario userEditado)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario u = bc.usuarios.Find(userEditado.Id);
                u.Login = userEditado.Login;
                u.Nome = userEditado.Nome;
                u.senha = userEditado.senha;
                u.tipo = userEditado.tipo;
                bc.SaveChanges();
            }
        }

        public void excluirUsuario(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.usuarios.Remove(bc.usuarios.Find(id));
                bc.SaveChanges();
            }
        }

        //INSERIR NOVO USUARIO
        private static string DB = "Database=Biblioteca; Data Source=localhost; User Id=root;";
        public static void InserirUsuario(Login u)
        {
            MySqlConnection conexao = new MySqlConnection(DB);
            conexao.Open();

            string query = "INSERT INTO usuarios (login, senha, tipo) VALUES (@login, @senha, @tipo)";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@login", u.login);
            comando.Parameters.AddWithValue("@senha", u.senha);
            comando.Parameters.AddWithValue("@tipo", u.tipo);

            comando.ExecuteNonQuery();
            MySqlDataReader Dados = comando.ExecuteReader();
            Login userFound = new Login();

            if (Dados.Read())
            {
                if (!Dados.IsDBNull(Dados.GetOrdinal("id")))
                    userFound.id = Dados.GetInt32("id");
                if (!Dados.IsDBNull(Dados.GetOrdinal("login")))
                    userFound.login = Dados.GetString("login");
                if (!Dados.IsDBNull(Dados.GetOrdinal("senha")))
                    userFound.senha = Dados.GetString("senha");
                conexao.Close();
            }


        }
    }
}