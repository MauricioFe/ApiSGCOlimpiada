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

        public IEnumerable<OcupacaoSolicitacaoCompra> GetSolicitacaoOcupacao(long solicitacaoID)
        {
            try
            {
                List<OcupacaoSolicitacaoCompra> ocupacaoSolicitacaoCompras = new List<OcupacaoSolicitacaoCompra>();
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"select sc.id, sc.ResponsavelEntrega, sc.Data, sc.Justificativa, sc.TipoComprasId, sc.EscolasId, sc.justificativa,o.id as ocupacaoId, o.Nome as ocupacao, o.Numero as hashtag, osc.OcupacoesId, osc.SolicitacaoComprasId, e.Id as idEscola, e.Bairro, e.Cep, e.Cidade, e.Estado, e.Logradouro, e.Nome as escola, e.Numero, tc.id as IdTipoCompras, tc.Descricao from solicitacaoCompras as sc inner join ocupacoessolicitacaocompras as osc on osc.SolicitacaoComprasId = sc.id inner join ocupacoes as o on osc.OcupacoesId = o.id inner join escolas as e on sc.escolasId = e.id inner join tipocompras tc on tc.Id = sc.tipocomprasId where sc.id = {solicitacaoID}", conn);
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
                    ocupacaoSolicitacaoCompra.Ocupacao = new Ocupacao();
                    ocupacaoSolicitacaoCompra.Ocupacao.Id = Convert.ToInt64(item["OcupacaoId"]);
                    ocupacaoSolicitacaoCompra.Ocupacao.Nome = item["ocupacao"].ToString();
                    ocupacaoSolicitacaoCompra.Ocupacao.Numero = item["hashtag"].ToString();
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra = new SolicitacaoCompra();
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.Id = Convert.ToInt64(item["Id"]);
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.ResponsavelEntrega = item["ResponsavelEntrega"].ToString();
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.Justificativa = item["Justificativa"].ToString();
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.Data = Convert.ToDateTime(item["data"]);
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.TipoCompraId = Convert.ToInt64(item["tipoComprasId"]);
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.Escola = new Escola();
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.Escola.Id = Convert.ToInt64(item["IdEscola"]);
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.Escola.Bairro = item["Bairro"].ToString();
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.Escola.Cep = item["Cep"].ToString();
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.Escola.Cidade = item["Cidade"].ToString();
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.Escola.Estado = item["Estado"].ToString();
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.Escola.Logradouro = item["Logradouro"].ToString();
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.Escola.Nome = item["escola"].ToString();
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.Escola.Numero = item["Numero"].ToString();
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.TipoCompra = new TipoCompra();
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.TipoCompra.Id = Convert.ToInt64(item["IdTipoCompras"]);
                    ocupacaoSolicitacaoCompra.SolicitacaoCompra.TipoCompra.Descricao = item["Descricao"].ToString();
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

        public bool Remove(long ocupacoesId, long solicitacaoCompraId)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Delete from OcupacoesSolicitacaoCompras where ocupacoesId = {ocupacoesId} and solicitacaoComprasId = {solicitacaoCompraId}", conn);
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
