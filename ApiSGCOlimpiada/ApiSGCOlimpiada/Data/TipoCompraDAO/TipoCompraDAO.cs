using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.TipoCompraDAO
{
    public class TipoCompraDAO : ITipoCompraDAO
    {
        private readonly string _conn;
        public TipoCompraDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        DataTable dt;

        public bool Add(TipoCompra tipoCompra)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into TipoCompras values(null, '{tipoCompra.Descricao}')", conn);
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

        public TipoCompra Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from TipoCompras where id = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                TipoCompra tipoCompra = null;
                foreach (DataRow item in dt.Rows)
                {
                    tipoCompra = new TipoCompra();
                    tipoCompra.Id = Convert.ToInt64(item["Id"]);
                    tipoCompra.Descricao = item["Descricao"].ToString();
                }
                return tipoCompra;
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
        public List<TipoCompra> FindBySearch(string search)
        {
            try
            {
                List<TipoCompra> tipoCompras = new List<TipoCompra>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from TipoCompras where descricao LIKE '%{search}%'", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    TipoCompra tipoCompra = new TipoCompra();
                    tipoCompra.Id = Convert.ToInt64(item["Id"]);
                    tipoCompra.Descricao = item["Descricao"].ToString();
                    tipoCompras.Add(tipoCompra);
                }
                return tipoCompras;
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
        public IEnumerable<TipoCompra> GetAll()
        {
            try
            {
                List<TipoCompra> tipoCompras = new List<TipoCompra>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from TipoCompras", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    TipoCompra tipoCompra = new TipoCompra();
                    tipoCompra.Id = Convert.ToInt64(item["Id"]);
                    tipoCompra.Descricao = item["Descricao"].ToString();
                    tipoCompras.Add(tipoCompra);
                }
                return tipoCompras;
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
                cmd = new MySqlCommand($"Delete from TipoCompras where id = {id}", conn);
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

        public bool Update(TipoCompra tipoCompra, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update TipoCompras set  descricao = '{tipoCompra.Descricao}' where id = {id}", conn);
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
