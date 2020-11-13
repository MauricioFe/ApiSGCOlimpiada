using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.UsuarioDAO
{
    public class UsuarioDAO : IUsuarioDAO
    {
        private readonly string _conn;
        public UsuarioDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        DataTable dt;

        public void Add(Usuario usuario)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into Usuarios values({usuario.Nome}, {usuario.Email}, {usuario.Senha}, {usuario.FuncaoId})", conn);
                cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }

        }

        public Usuario Find(int id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Usuarios where id = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Usuario usuario = new Usuario();
                foreach (DataRow item in dt.Rows)
                {
                    usuario.Nome = item["Nome"].ToString();
                    usuario.Email = item["Email"].ToString();
                    usuario.Senha = item["Senha"].ToString();
                    usuario.FuncaoId = Convert.ToInt32(item["funcaoId"]);
                }
                return usuario;
            }
            catch
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public IEnumerable<Usuario> GetAll()
        {
            try
            {
                List<Usuario> usuarios = new List<Usuario>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Usuarios", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Usuario usuario = new Usuario();
                    usuario.Nome = item["Nome"].ToString();
                    usuario.Email = item["Email"].ToString();
                    usuario.Senha = item["Senha"].ToString();
                    usuario.FuncaoId = Convert.ToInt32(item["funcaoId"]);
                    usuarios.Add(usuario);
                }
                return usuarios;
            }
            catch
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public Usuario Login(Usuario usuario)
        {
            try
            {

                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select Nome, Email, Senha, funcaoId from Usuarios where Email = {usuario.Email} and Senha = {usuario.Senha}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Usuario usuarioLogado = new Usuario();
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        usuarioLogado.Id = Convert.ToInt32(item["Id"]);
                        usuarioLogado.Nome = item["Nome"].ToString();
                        usuarioLogado.Email = item["Email"].ToString();
                        usuarioLogado.Senha = item["Senha"].ToString();
                        usuarioLogado.FuncaoId = Convert.ToInt32(item["funcaoId"]);
                    }
                    return usuarioLogado;
                }
                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Remove(int id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Delete from Usuarios where id = {id}", conn);
                cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Usuario usuario, int id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update Usuarios set Nome = {usuario.Nome}, email = {usuario.Email}, senha = {usuario.Senha} where id = {id}", conn);
                cmd.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }
        }
    }
}
