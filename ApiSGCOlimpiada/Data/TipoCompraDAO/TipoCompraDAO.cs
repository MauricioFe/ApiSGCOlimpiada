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

        public void Add(TipoCompra tipoCompra)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into TipoCompras values(null, '{tipoCompra.Descricao}')", conn);
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
                TipoCompra tipoCompra = new TipoCompra();
                foreach (DataRow item in dt.Rows)
                {
                    tipoCompra.Id = Convert.ToInt64(item["Id"]);
                    tipoCompra.Descricao = item["Descricao"].ToString();
                }
                return tipoCompra;
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
                cmd = new MySqlCommand($"Delete from TipoCompras where id = {id}", conn);
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

        public void Update(TipoCompra tipoCompra, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update TipoCompras set  descricao = '{tipoCompra.Descricao}' where id = {id}", conn);
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
