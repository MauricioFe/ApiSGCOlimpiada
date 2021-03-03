using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.LogDAO
{
    public class LogDAO : ILogDAO
    {
        private readonly string _conn;
        public LogDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        DataTable dt;

        public bool Add(Log log)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into Logs values(null, '{log.Data}', '{log.Descricao}', {log.UsuarioId})", conn);
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

        public Log Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Logs where id = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Log log = new Log();
                foreach (DataRow item in dt.Rows)
                {
                    log.Id = Convert.ToInt64(item["Id"]);
                    log.Data = DateTime.Parse(item["data"].ToString());
                    log.Descricao = item["Descricao"].ToString();
                    log.UsuarioId = Convert.ToInt64(item["UsuarioId"]);
                }
                return log;
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

        public IEnumerable<Log> GetAll()
        {
            try
            {
                List<Log> logs = new List<Log>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Logs", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Log log = new Log();
                    log.Id = Convert.ToInt64(item["Id"]);
                    log.Data = DateTime.Parse(item["data"].ToString());
                    log.Descricao = item["Descricao"].ToString();
                    log.UsuarioId = Convert.ToInt64(item["UsuarioId"]);
                    logs.Add(log);
                }
                return logs;
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
                cmd = new MySqlCommand($"Delete from Log where id = {id}", conn);
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
                return false ;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool Update(Log log, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update Log set data = '{log.Data}', descricao = '{log.Descricao}', usuarioId = {log.UsuarioId} where id = {id}", conn);
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
