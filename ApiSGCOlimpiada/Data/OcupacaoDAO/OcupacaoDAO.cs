using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.OcupacaoDAO
{
    public class OcupacaoDAO : IOcupacaoDAO
    {
        private readonly string _conn;
        public OcupacaoDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        DataTable dt;

        public bool Add(Ocupacao ocupacao)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into Ocupacoes values(null, '{ocupacao.Nome}', '{ocupacao.Numero}')", conn);
                cmd.ExecuteNonQuery();
                return true;
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

        public Ocupacao Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Ocupacoes where id = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Ocupacao ocupacao = null;
                foreach (DataRow item in dt.Rows)
                {
                    ocupacao = new Ocupacao();
                    ocupacao.Id = Convert.ToInt64(item["Id"]);
                    ocupacao.Nome = item["Nome"].ToString();
                    ocupacao.Numero = item["Numero"].ToString();
                }
                return ocupacao;
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
        public List<Ocupacao> FindBySearch(string search)
        {
            List<Ocupacao> ocupacaos = new List<Ocupacao>();
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Ocupacoes where nome LIKE '%{search}%' or numero LIKE '%{search}%'", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Ocupacao ocupacao = null;
                foreach (DataRow item in dt.Rows)
                {
                    ocupacao = new Ocupacao();
                    ocupacao.Id = Convert.ToInt64(item["Id"]);
                    ocupacao.Nome = item["Nome"].ToString();
                    ocupacao.Numero = item["Numero"].ToString();
                    ocupacaos.Add(ocupacao);
                }
                return ocupacaos;
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


        public IEnumerable<Ocupacao> GetAll()
        {
            try
            {
                List<Ocupacao> ocupacaos = new List<Ocupacao>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Ocupacoes", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Ocupacao ocupacao = new Ocupacao();
                    ocupacao.Id = Convert.ToInt64(item["Id"]);
                    ocupacao.Nome = item["Nome"].ToString();
                    ocupacao.Numero = item["Numero"].ToString();
                    ocupacaos.Add(ocupacao);
                }
                return ocupacaos;
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
                cmd = new MySqlCommand($"Delete from Ocupacoes where id = {id}", conn);
                cmd.ExecuteNonQuery();
                return true;
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

        public bool Update(Ocupacao ocupacao, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update Ocupacoes set Nome = '{ocupacao.Nome}', Numero = '{ocupacao.Numero}' where id = {id}", conn);
                cmd.ExecuteNonQuery();
                return true;
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
