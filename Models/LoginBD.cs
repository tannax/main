using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;

namespace Biblioteca.Models
{
    public class LoginBD
    {
           private static string DB = "Database=Biblioteca; Data Source=localhost; User Id=root;";
         
       
          public static Login inserirLogin (Login u) {
             MySqlConnection conexao = new MySqlConnection(DB);
             
            conexao.Open();
            string query = "SELECT * FROM usuarios WHERE login=@login AND senha=@senha";
            MySqlCommand comando = new MySqlCommand(query, conexao);

         comando.Parameters.AddWithValue("@login", u.login);
         comando.Parameters.AddWithValue("@senha", u.senha);
            
            MySqlDataReader Dados = comando.ExecuteReader();


            Login userFound = new Login();

if(Dados.Read()) {
if (!Dados.IsDBNull(Dados.GetOrdinal("id")))
userFound.id = Dados.GetInt32("id");
if (!Dados.IsDBNull(Dados.GetOrdinal("login")))
userFound.login = Dados.GetString("login");
if(!Dados.IsDBNull(Dados.GetOrdinal("senha")))
userFound.senha = Dados.GetString("senha");
}

          conexao.Close();
          return userFound;
    }
}
}