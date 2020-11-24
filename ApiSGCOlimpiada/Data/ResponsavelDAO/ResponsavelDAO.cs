using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.ResponsavelDAO
{
    public class ResponsavelDAO : IResponsavelDAO
    {
        private readonly string _conn;
        public ResponsavelDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        DataTable dt;

        public void Add(Responsavel responsavel)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into Responsaveis values(null, '{responsavel.Nome}', '{responsavel.Cargo}', {responsavel.EscolaId})", conn);
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

        public Responsavel Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"select e.*, r.Id as ResponsavelId, r.Nome as responsavelNome, r.Cargo, r.EscolasId From Escolas e Inner join Responsaveis r on r.EscolasId = e.id where r.Id = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Responsavel responsavel = null;
                foreach (DataRow item in dt.Rows)
                {
                    responsavel = new Responsavel();
                    responsavel.Id = Convert.ToInt64(item["ResponsavelId"]);
                    responsavel.Nome = item["responsavelNome"].ToString();
                    responsavel.Cargo = item["Cargo"].ToString();
                    responsavel.Escola = new Escola();
                    responsavel.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    responsavel.Escola.Id = Convert.ToInt64(item["Id"]);
                    responsavel.Escola.Nome = item["Nome"].ToString();
                    responsavel.Escola.Cep = item["Cep"].ToString();
                    responsavel.Escola.Logradouro = item["Logradouro"].ToString();
                    responsavel.Escola.Bairro = item["Bairro"].ToString();
                    responsavel.Escola.Numero = item["Numero"].ToString();
                    responsavel.Escola.Cidade = item["Cidade"].ToString();
                    responsavel.Escola.Estado = item["Estado"].ToString();
                }
                return responsavel;
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
        public List<Responsavel> FindBySearch(string search)
        {
            try
            {
                List<Responsavel> responsaveis = new List<Responsavel>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"select e.*, r.Id as ResponsavelId, r.Nome as responsavelNome, r.Cargo, r.EscolasId From Escolas e Inner join Responsaveis r on r.EscolasId = e.id where r.Nome LIKE '%{search}%' or e.Nome LIKE '%{search}%'", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Responsavel responsavel = null;
                foreach (DataRow item in dt.Rows)
                {
                    responsavel = new Responsavel();
                    responsavel.Id = Convert.ToInt64(item["ResponsavelId"]);
                    responsavel.Nome = item["responsavelNome"].ToString();
                    responsavel.Cargo = item["Cargo"].ToString();
                    responsavel.Escola = new Escola();
                    responsavel.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    responsavel.Escola.Id = Convert.ToInt64(item["Id"]);
                    responsavel.Escola.Nome = item["Nome"].ToString();
                    responsavel.Escola.Cep = item["Cep"].ToString();
                    responsavel.Escola.Logradouro = item["Logradouro"].ToString();
                    responsavel.Escola.Bairro = item["Bairro"].ToString();
                    responsavel.Escola.Numero = item["Numero"].ToString();
                    responsavel.Escola.Cidade = item["Cidade"].ToString();
                    responsavel.Escola.Estado = item["Estado"].ToString();
                    responsaveis.Add(responsavel);
                }
                return responsaveis;
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

        public IEnumerable<Responsavel> GetAll()
        {
            try
            {
                List<Responsavel> responsaveis = new List<Responsavel>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"select e.*, r.Id as ResponsavelId, r.Nome as responsavelNome, r.Cargo, r.EscolasId From Escolas e Inner join Responsaveis r on r.EscolasId = e.id", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Responsavel responsavel = new Responsavel();
                    responsavel.Id = Convert.ToInt64(item["ResponsavelId"]);
                    responsavel.Nome = item["responsavelNome"].ToString();
                    responsavel.Cargo = item["Cargo"].ToString();
                    responsavel.Escola = new Escola();
                    responsavel.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    responsavel.Escola.Id = Convert.ToInt64(item["Id"]);
                    responsavel.Escola.Nome = item["Nome"].ToString();
                    responsavel.Escola.Cep = item["Cep"].ToString();
                    responsavel.Escola.Logradouro = item["Logradouro"].ToString();
                    responsavel.Escola.Bairro = item["Bairro"].ToString();
                    responsavel.Escola.Numero = item["Numero"].ToString();
                    responsavel.Escola.Cidade = item["Cidade"].ToString();
                    responsavel.Escola.Estado = item["Estado"].ToString();
                    responsaveis.Add(responsavel);
                }
                return responsaveis;
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
                cmd = new MySqlCommand($"Delete from Responsaveis where id = {id}", conn);
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

        public void Update(Responsavel responsavel, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update Responsaveis set Nome = {responsavel.Nome}, Cargo = '{responsavel.Cargo}', " +
                    $"EscolaId = {responsavel.EscolaId} where id = {id}", conn);
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
