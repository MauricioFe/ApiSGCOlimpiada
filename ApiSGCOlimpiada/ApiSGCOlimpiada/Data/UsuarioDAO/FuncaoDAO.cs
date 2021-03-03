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
    public class FuncaoDAO : IFuncaoDAO
    {
        private readonly string _conn;
        public FuncaoDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        DataTable dt;
        public bool Add(Funcao funcao)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into Funcao values(null, '{funcao.funcao}')", conn);
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

        public Funcao Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Funcao where id = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Funcao funcao = new Funcao();
                foreach (DataRow item in dt.Rows)
                {
                    funcao.Id = Convert.ToInt64(item["Id"]);
                    funcao.funcao = item["funcao"].ToString();
                }
                return funcao;
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

        public IEnumerable<Funcao> GetAll()
        {
            try
            {
                List<Funcao> funcaos = new List<Funcao>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Funcao", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Funcao funcao = new Funcao();
                    funcao.Id = Convert.ToInt64(item["Id"]);
                    funcao.funcao = item["funcao"].ToString();
                    funcaos.Add(funcao);
                }
                return funcaos;
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

        public bool Remove(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Delete from Funcao where id = {id}", conn);
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

        public bool Update(Funcao funcao, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update Funcao set Funcao = {funcao.funcao}  where id = {id}", conn);
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
    }
}

