
using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.EscolaDAO
{
    public class EscolaDAO : IEscolaDAO
    {
        private readonly string _conn;
        public EscolaDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        DataTable dt;

        public void Add(Escola escola)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into Escolas values(null, '{escola.Nome}', '{escola.Cep}', '{escola.Logradouro}', " +
                    $"'{escola.Bairro}', '{escola.Numero}', {escola.Cidade}, {escola.Estado})", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public Escola Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Escolas where id = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Escola escola = new Escola();
                foreach (DataRow item in dt.Rows)
                {
                    escola.Id = Convert.ToInt64(item["Id"]);
                    escola.Nome = item["Nome"].ToString();
                    escola.Cep = item["Cep"].ToString();
                    escola.Logradouro = item["Logradouro"].ToString();
                    escola.Bairro = item["Bairro"].ToString();
                    escola.Numero = item["Numero"].ToString();
                    escola.Cidade = item["Cidade"].ToString();
                    escola.Estado = item["Estado"].ToString();
                }
                return escola;
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

        public IEnumerable<Escola> GetAll()
        {
            try
            {
                List<Escola> escolas = new List<Escola>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Escolas", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Escola escola = new Escola();
                    escola.Id = Convert.ToInt64(item["Id"]);
                    escola.Nome = item["Nome"].ToString();
                    escola.Cep = item["Cep"].ToString();
                    escola.Logradouro = item["Logradouro"].ToString();
                    escola.Bairro = item["Bairro"].ToString();
                    escola.Numero = item["Numero"].ToString();
                    escola.Cidade = item["Cidade"].ToString();
                    escola.Estado = item["Estado"].ToString();
                    escolas.Add(escola);
                }
                return escolas;
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

        public void Remove(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Delete from Escolas where id = {id}", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Escola escola, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update Escolas set Nome = '{escola.Nome}',  Cep = '{escola.Cep}', Logradouro = '{escola.Logradouro}', " +
                    $"Bairro = '{escola.Bairro}', Numero = '{escola.Numero}', Cidade = {escola.Cidade}, Estado = {escola.Estado} where id = {id}", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
