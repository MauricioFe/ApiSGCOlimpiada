using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public bool Add(Usuario usuario)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into Usuarios values(null, '{usuario.Nome}', '{usuario.Email}', '{usuario.Senha}', {usuario.FuncaoId})", conn);
                int rows = cmd.ExecuteNonQuery();
                if (rows != -1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }

        }

        public Usuario Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select usuarios.id,nome, email, senha, funcaoId, f.funcao, f.id as idFuncao from Usuarios" +
                    $" inner join funcao as f on f.id = usuarios.funcaoId where id = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Usuario usuario = null;
                foreach (DataRow item in dt.Rows)
                {
                    usuario = new Usuario();
                    usuario.Id = Convert.ToInt64(item["Id"]);
                    usuario.Nome = item["Nome"].ToString();
                    usuario.Email = item["Email"].ToString();
                    usuario.Senha = item["Senha"].ToString();
                    usuario.Funcao = new Funcao();
                    usuario.Funcao.Id = Convert.ToInt64(item["IdFuncao"]);
                    usuario.Funcao.funcao = item["funcao"].ToString();
                }
                return usuario;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                cmd = new MySqlCommand($"Select usuarios.id,nome, email, senha, funcaoId, f.funcao, f.id as idFuncao from Usuarios" +
                    $" inner join funcao as f on f.id = usuarios.funcaoId ", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = Convert.ToInt64(item["Id"]);
                    usuario.Nome = item["Nome"].ToString();
                    usuario.Email = item["Email"].ToString();
                    usuario.Senha = item["Senha"].ToString();
                    usuario.FuncaoId = Convert.ToInt32(item["funcaoId"]);
                    usuario.Funcao = new Funcao();
                    usuario.Funcao.Id = Convert.ToInt64(item["IdFuncao"]);
                    usuario.Funcao.funcao = item["funcao"].ToString();
                    usuarios.Add(usuario);
                }
                return usuarios;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                cmd = new MySqlCommand($"Select id, Nome, Email, Senha, funcaoId from Usuarios where Email = '{usuario.Email}' and Senha = '{usuario.Senha}'", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Usuario usuarioLogado = null;
                if (dt.Rows.Count > 0)
                {
                    usuarioLogado = new Usuario();
                    foreach (DataRow item in dt.Rows)
                    {
                        usuarioLogado.Id = Convert.ToInt64(item["Id"]);
                        usuarioLogado.Nome = item["Nome"].ToString();
                        usuarioLogado.Email = item["Email"].ToString();
                        usuarioLogado.Senha = item["Senha"].ToString();
                        usuarioLogado.FuncaoId = Convert.ToInt32(item["funcaoId"]);
                    }
                }
                return usuarioLogado;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool Remove(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Delete from Usuarios where id = {id}", conn);
                int rows = cmd.ExecuteNonQuery();
                if (rows != -1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool Update(Usuario usuario, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update Usuarios set Nome ='{usuario.Nome}', email = '{usuario.Email}', senha = '{usuario.Senha}' where id = {id}", conn);
                int rows = cmd.ExecuteNonQuery();
                if (rows != -1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Usuario> FindByName(string Nome)
        {
            try
            {
                List<Usuario> usuarios = new List<Usuario>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Usuarios where nome Like '%{Nome}%'", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Usuario usuario = null;
                foreach (DataRow item in dt.Rows)
                {
                    usuario = new Usuario();
                    usuario.Id = Convert.ToInt64(item["Id"]);
                    usuario.Nome = item["Nome"].ToString();
                    usuario.Email = item["Email"].ToString();
                    usuario.Senha = item["Senha"].ToString();
                    usuario.FuncaoId = Convert.ToInt32(item["funcaoId"]);
                    usuarios.Add(usuario);
                }
                return usuarios;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
