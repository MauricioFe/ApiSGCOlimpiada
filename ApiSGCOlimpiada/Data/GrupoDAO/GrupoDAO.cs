using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.GrupoDAO
{
    public class GrupoDAO : IGrupoDAO
    {
        private readonly string _conn;
        public GrupoDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        DataTable dt;

        public void Add(Grupo grupo)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into Grupos values(null, {grupo.CodigoProtheus}, '{grupo.Descricao}')", conn);
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

        public Grupo FindBySearch(string search)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Grupos where codigoProtheus Like '%{search}%' or Descricao Like '%{search}%'", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Grupo grupo = null;
                foreach (DataRow item in dt.Rows)
                {
                    grupo = new Grupo();
                    grupo.Id = Convert.ToInt64(item["Id"]);
                    grupo.CodigoProtheus = int.Parse(item["CodigoProtheus"].ToString());
                    grupo.Descricao = item["Descricao"].ToString();
                }
                return grupo;
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

        public IEnumerable<Grupo> GetAll()
        {
            try
            {
                List<Grupo> grupos = new List<Grupo>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Grupos", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Grupo grupo = new Grupo();
                    grupo.Id = Convert.ToInt64(item["Id"]);
                    grupo.CodigoProtheus = int.Parse(item["CodigoProtheus"].ToString());
                    grupo.Descricao = item["Descricao"].ToString();
                    grupos.Add(grupo);
                }
                return grupos;
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
                cmd = new MySqlCommand($"Delete from Grupos where id = {id}", conn);
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

        public void Update(Grupo grupo, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update Grupos set CodigoProtheus = {grupo.CodigoProtheus}, descricao = '{grupo.Descricao}' where id = {id}", conn);
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

        public Grupo Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Grupos where id = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Grupo grupo = null;
                foreach (DataRow item in dt.Rows)
                {
                    grupo = new Grupo();
                    grupo.Id = Convert.ToInt64(item["Id"]);
                    grupo.CodigoProtheus = int.Parse(item["CodigoProtheus"].ToString());
                    grupo.Descricao = item["Descricao"].ToString();
                }
                return grupo;
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
    }
}
