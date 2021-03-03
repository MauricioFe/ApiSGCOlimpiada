using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.SolicitacaoCompraDAO
{
    public class SolicitacaoCompraDAO : ISolicitacaoCompraDAO
    {
        private readonly string _conn;
        public SolicitacaoCompraDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        public bool Add(SolicitacaoCompra solicitacaoCompra)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"insert into SolicitacaoCompras values (null,'{solicitacaoCompra.ResponsavelEntrega}', " +
                    $"'{solicitacaoCompra.Data.ToString("yyyy-MM-dd HH:mm")}', '{solicitacaoCompra.Justificativa}', '{solicitacaoCompra.Anexo}', {solicitacaoCompra.TipoCompraId}, {solicitacaoCompra.EscolaId})", conn);
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

        public SolicitacaoCompra Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Select * from SolicitacaoCompras where id = {id}", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                SolicitacaoCompra solicitacaoCompra = null;
                foreach (DataRow item in dt.Rows)
                {
                    solicitacaoCompra = new SolicitacaoCompra();
                    solicitacaoCompra.Id = Convert.ToInt32(item["id"]);
                    solicitacaoCompra.ResponsavelEntrega = item["responsavelEntrega"].ToString();
                    solicitacaoCompra.Data = Convert.ToDateTime(item["Data"]);
                    solicitacaoCompra.Justificativa = item["Justificativa"].ToString();
                    solicitacaoCompra.TipoCompraId = Convert.ToInt32(item["tipoComprasId"]);
                    solicitacaoCompra.EscolaId = Convert.ToInt32(item["EscolasId"]);
                    solicitacaoCompra.Anexo = item["Anexo"].ToString();
                }
                return solicitacaoCompra;
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

        public IEnumerable<SolicitacaoCompra> GetAll()
        {
            try
            {
                List<SolicitacaoCompra> solicitacaoCompras = new List<SolicitacaoCompra>();
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Select * from solicitacaocompras", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                SolicitacaoCompra solicitacaoCompra = null;
                foreach (DataRow item in dt.Rows)
                {
                    solicitacaoCompra = new SolicitacaoCompra();
                    solicitacaoCompra.Id = Convert.ToInt64(item["id"]);
                    solicitacaoCompra.ResponsavelEntrega = item["responsavelEntrega"].ToString();
                    solicitacaoCompra.Data = Convert.ToDateTime(item["Data"]);
                    solicitacaoCompra.Justificativa = item["Justificativa"].ToString();
                    solicitacaoCompra.TipoCompraId = Convert.ToInt64(item["tipoComprasId"]);
                    solicitacaoCompra.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    solicitacaoCompra.Anexo = item["Anexo"].ToString();
                    solicitacaoCompras.Add(solicitacaoCompra);
                }
                return solicitacaoCompras;
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

        public bool Update(SolicitacaoCompra solicitacaoCompra, long id)
        {

            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Update SolicitacaoCompras set responsavelEntrega = '{solicitacaoCompra.ResponsavelEntrega}', " +
                    $"data = '{solicitacaoCompra.Data.ToString("yyyy-MM-dd HH:mm")}', justificativa = '{solicitacaoCompra.Justificativa}', tipoComprasId = {solicitacaoCompra.TipoCompraId}, escolasId = {solicitacaoCompra.EscolaId}, anexo = '{solicitacaoCompra.Anexo}' where id = {id}", conn);
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

        public bool AnexarNotaFiscal(string fileName, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Update SolicitacaoCompras set anexo = '{fileName}' where id = {id}", conn);
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
