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
                    $"{produtoPedidoOrcamento.SolicitacaoComprasId}, {produtoPedidoOrcamento.OrcamentoId}, {produtoPedidoOrcamento.valor.ToString().Replace(",", ".")}, " +
                    $"{produtoPedidoOrcamento.Quantidade.ToString().Replace(",", ".")}, {produtoPedidoOrcamento.Ipi.ToString().Replace(",", ".")}," +
                    $" {produtoPedidoOrcamento.Icms.ToString().Replace(",", ".")}, {produtoPedidoOrcamento.Desconto.ToString().Replace(",", ".")})", conn);
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

        public bool Update(ProdutoPedidoOrcamento produtoPedidoOrcamento, long solicitacaoId, long produtoId, long orcamentoId)
        {

            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Update produtoPedidoOrcamento set produtosId = {produtoPedidoOrcamento.ProdutoId}, " +
                    $"solictacaoComprasId = '{produtoPedidoOrcamento.SolicitacaoComprasId}', valor = {produtoPedidoOrcamento.valor.ToString().Replace(",", ".")}, quantidade = {produtoPedidoOrcamento.Quantidade.ToString().Replace(",", ".")}," +
                    $"ipi = {produtoPedidoOrcamento.Ipi.ToString().Replace(",", ".")}, icms = {produtoPedidoOrcamento.Icms.ToString().Replace(",", ".")}, orcamentosId={produtoPedidoOrcamento.OrcamentoId}" +
                    $" where solicitacaoComprasId = {solicitacaoId} and produtosId = {produtoId}, and orcamentosId = {orcamentoId} ", conn);
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
        public IEnumerable<ProdutoPedidoOrcamento> GetProdutosSolicitacao(long idSolicitacao)
        {
            try
            {
                List<ProdutoPedidoOrcamento> produtoPedidoOrcamentos = new List<ProdutoPedidoOrcamento>();
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"select p.Id as idProduto, p.CodigoProtheus, p.Descricao, p.GruposId, " +
                    $"ppo.OrcamentosId, ppo.SolicitacaoComprasId, ppo.ProdutosId, ppo.Desconto, ppo.ICMS, ppo.IPI, ppo.Quantidade, ppo.valor, " +
                    $"o.Id as idOrcamento, o.Anexo, o.CNPJ, o.Data, o.FormaPagamento, o.Fornecedor, o.TotalIPI, o.TotalProdutos, o.ValorFrete, o.ValorTotal " +
                    $"from produtos as p inner join produtopedidoorcamento as ppo on ppo.ProdutosId = p.id inner join orcamentos as o on ppo.orcamentosID = o.id " +
                    $"inner join solicitacaocompras as sc on ppo.SolicitacaoComprasId = sc.id where SolicitacaoComprasId = {idSolicitacao} order by orcamentosId ", conn);
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
                    produtoPedidoOrcamento.Orcamento = new Orcamento();
                    produtoPedidoOrcamento.Orcamento.Id = Convert.ToInt64(item["IdOrcamento"]);
                    produtoPedidoOrcamento.Orcamento.Anexo = item["Anexo"].ToString();
                    produtoPedidoOrcamento.Orcamento.Cnpj = item["CNPJ"].ToString();
                    produtoPedidoOrcamento.Orcamento.Fornecedor = item["Fornecedor"].ToString();
                    produtoPedidoOrcamento.Orcamento.Data = Convert.ToDateTime(item["Data"]);
                    produtoPedidoOrcamento.Orcamento.FormaPagamento = item["FormaPagamento"].ToString();
                    produtoPedidoOrcamento.Orcamento.TotalIpi = Convert.ToDouble(item["TotalIPI"]);
                    produtoPedidoOrcamento.Orcamento.TotalProdutos = Convert.ToDouble(item["TotalProdutos"]);
                    produtoPedidoOrcamento.Orcamento.ValorTotal = Convert.ToDouble(item["ValorTotal"]);
                    produtoPedidoOrcamento.Orcamento.ValorFrete = Convert.ToDouble(item["ValorFrete"]);
                    produtoPedidoOrcamento.Produto = new Produto();
                    produtoPedidoOrcamento.Produto.Id = Convert.ToInt64(item["IdProduto"]);
                    produtoPedidoOrcamento.Produto.CodigoProtheus = Convert.ToInt64(item["CodigoProtheus"]);
                    produtoPedidoOrcamento.Produto.Descricao = item["Descricao"].ToString();
                    produtoPedidoOrcamento.Produto.GrupoId = Convert.ToInt64(item["GrupoId"]);
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
    }
}
