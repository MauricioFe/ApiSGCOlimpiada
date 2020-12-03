using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.OcupacaoSolicitacaoCompraDAO
{
    public class OcupacaoSolicitacaoCompraDAO : IOcupacaoSolicitacaoCompraDAO
    {
        private readonly string _conn;
        public OcupacaoSolicitacaoCompraDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        public bool Add(OcupacaoSolicitacaoCompra ocupacaoSolicitacaoCompra)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"insert into OcupacoesSolicitacaoCompras values ({ocupacaoSolicitacaoCompra.OcupacaoId}, {ocupacaoSolicitacaoCompra.SolicitacaoId})", conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
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

        public OcupacaoSolicitacaoCompra Find(long ocupacoesId, long solicitacaoCompraId)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Select * from OcupacoesSolicitacaoCompras where OcupacoesId = {ocupacoesId} and SolicitacaoComprasId = {solicitacaoCompraId}", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                OcupacaoSolicitacaoCompra ocupacaoSolicitacaoCompra = null;
                foreach (DataRow item in dt.Rows)
                {
                    ocupacaoSolicitacaoCompra = new OcupacaoSolicitacaoCompra();
                    ocupacaoSolicitacaoCompra.OcupacaoId = Convert.ToInt64(item["OcupacoesId"]);
                    ocupacaoSolicitacaoCompra.SolicitacaoId = Convert.ToInt64(item["SolicitacaoComprasId"]);
                }
                return ocupacaoSolicitacaoCompra;
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

        public IEnumerable<OcupacaoSolicitacaoCompra> GetAll()
        {
            try
            {
                List<OcupacaoSolicitacaoCompra> ocupacaoSolicitacaoCompras = new List<OcupacaoSolicitacaoCompra>();
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Select * from OcupacoesSolicitacaoCompras", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                OcupacaoSolicitacaoCompra ocupacaoSolicitacaoCompra = null;
                foreach (DataRow item in dt.Rows)
                {
                    ocupacaoSolicitacaoCompra = new OcupacaoSolicitacaoCompra();
                    ocupacaoSolicitacaoCompra.OcupacaoId = Convert.ToInt64(item["OcupacoesId"]);
                    ocupacaoSolicitacaoCompra.SolicitacaoId = Convert.ToInt64(item["SolicitacaoComprasId"]);
                    ocupacaoSolicitacaoCompras.Add(ocupacaoSolicitacaoCompra);
                }
                return ocupacaoSolicitacaoCompras;
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

        public bool Update(OcupacaoSolicitacaoCompra ocupacaoSolicitacaoCompra, long ocupacoesId, long solicitacaoCompraId)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Update OcupacoesSolicitacaoCompras set OcupacoesId = {ocupacaoSolicitacaoCompra.OcupacaoId}, " +
                    $"SolicitacaoComprasId = {ocupacaoSolicitacaoCompra.SolicitacaoId}  where OcupacoesId = {ocupacoesId} and SolicitacaoComprasId = {solicitacaoCompraId}", conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
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
