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
                cmd = new MySqlCommand($"Select * from Responsaveis where id = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Responsavel responsavel = new Responsavel();
                foreach (DataRow item in dt.Rows)
                {
                    responsavel.Id = Convert.ToInt64(item["Id"]);
                    responsavel.Nome = item["Nome"].ToString();
                    responsavel.Cargo = item["Cargo"].ToString();
                    responsavel.EscolaId = Convert.ToInt64(item["EscolaId"]);
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

        public IEnumerable<Responsavel> GetAll()
        {
            try
            {
                List<Responsavel> responsavels = new List<Responsavel>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Responsaveis", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Responsavel responsavel = new Responsavel();
                    responsavel.Id = Convert.ToInt64(item["Id"]);
                    responsavel.Nome = item["Nome"].ToString();
                    responsavel.Cargo = item["Cargo"].ToString();
                    responsavel.EscolaId = Convert.ToInt64(item["EscolaId"]);
                    responsavels.Add(responsavel);
                }
                return responsavels;
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
