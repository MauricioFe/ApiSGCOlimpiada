using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.ProdutoPedidoOrcamentoDAO
{
    public class ProdutoPedidoOrcamentoDAO : IProdutoPedidoOrcamentoDAO
    {
        private readonly string _conn;
        public ProdutoPedidoOrcamentoDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        public bool Add(ProdutoPedidoOrcamento produtoPedidoOrcamento)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"insert into ProdutoPedidoOrcamento values ({produtoPedidoOrcamento.ProdutoId}, " +
                    $"{produtoPedidoOrcamento.SolicitacaoComprasId}, {produtoPedidoOrcamento.valor}, {produtoPedidoOrcamento.Quantidade}, {produtoPedidoOrcamento.Ipi}," +
                    $" {produtoPedidoOrcamento.Icms}, {produtoPedidoOrcamento.OrcamentoId})", conn);
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

        public ProdutoPedidoOrcamento Find(long solicitacaoId, long produtoId)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Select * from produtoPedidoOrcamento where solicitacaoComprasId = {solicitacaoId} and produtosId = {produtoId}", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                ProdutoPedidoOrcamento produtoPedidoOrcamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    produtoPedidoOrcamento = new ProdutoPedidoOrcamento();
                    produtoPedidoOrcamento.ProdutoId = Convert.ToInt64(item["ProdutosId"]);
                    produtoPedidoOrcamento.SolicitacaoComprasId = Convert.ToInt64(item["SolicitacaoComprasId"]);
                    produtoPedidoOrcamento.valor = Convert.ToDouble(item["valor"]);
                    produtoPedidoOrcamento.Quantidade = Convert.ToInt32(item["Quantidade"]);
                    produtoPedidoOrcamento.Ipi = Convert.ToDouble(item["Ipi"]);
                    produtoPedidoOrcamento.Icms = Convert.ToDouble(item["Icms"]);
                    produtoPedidoOrcamento.OrcamentoId = Convert.ToInt64(item["OrcamentosId"]);
                }
                return produtoPedidoOrcamento;
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

        public IEnumerable<ProdutoPedidoOrcamento> GetAll()
        {
            try
            {
                List<ProdutoPedidoOrcamento> produtoPedidoOrcamentos = new List<ProdutoPedidoOrcamento>();
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Select * from produtoPedidoOrcamento", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                ProdutoPedidoOrcamento produtoPedidoOrcamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    produtoPedidoOrcamento = new ProdutoPedidoOrcamento();
                    produtoPedidoOrcamento.ProdutoId = Convert.ToInt64(item["ProdutosId"]);
                    produtoPedidoOrcamento.SolicitacaoComprasId = Convert.ToInt64(item["SolicitacaoComprasId"]);
                    produtoPedidoOrcamento.valor = Convert.ToDouble(item["valor"]);
                    produtoPedidoOrcamento.Quantidade = Convert.ToInt32(item["Quantidade"]);
                    produtoPedidoOrcamento.Ipi = Convert.ToDouble(item["Ipi"]);
                    produtoPedidoOrcamento.Icms = Convert.ToDouble(item["Icms"]);
                    produtoPedidoOrcamento.OrcamentoId = Convert.ToInt64(item["OrcamentosId"]);
                    produtoPedidoOrcamentos.Add(produtoPedidoOrcamento);
                }
                return produtoPedidoOrcamentos;
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

        public bool Update(ProdutoPedidoOrcamento produtoPedidoOrcamento, long solicitacaoId, long produtoId)
        {

            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Update produtoPedidoOrcamento set produtosId = {produtoPedidoOrcamento.ProdutoId}, " +
                    $"solictacaoComprasId = '{produtoPedidoOrcamento.SolicitacaoComprasId}', valor = {produtoPedidoOrcamento.valor}, quantidade = {produtoPedidoOrcamento.Quantidade}," +
                    $"ipi = {produtoPedidoOrcamento.Ipi}, icms = {produtoPedidoOrcamento.Icms}, orcamentosId={produtoPedidoOrcamento.OrcamentoId}" +
                    $" where solicitacaoComprasId = {solicitacaoId} and produtosId = {produtoId}", conn);
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
