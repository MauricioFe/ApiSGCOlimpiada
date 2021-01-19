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
                cmd = new MySqlCommand($"insert into ProdutoPedidoOrcamento values (null, {produtoPedidoOrcamento.valor.ToString("F2").Replace(",", ".")}, " +
                    $"{produtoPedidoOrcamento.Quantidade.ToString().Replace(",", ".")}, {produtoPedidoOrcamento.Ipi.ToString("F2").Replace(",", ".")}, " +
                    $"{produtoPedidoOrcamento.Icms.ToString("F2").Replace(",", ".")}, {produtoPedidoOrcamento.Desconto.ToString("F2").Replace(",", ".")}, " +
                    $"{produtoPedidoOrcamento.ProdutoSolicitacoesId},{produtoPedidoOrcamento.OrcamentoId})", conn);
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

        public ProdutoPedidoOrcamento Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Select * from produtoPedidoOrcamento where Id = {id}", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                ProdutoPedidoOrcamento produtoPedidoOrcamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    produtoPedidoOrcamento = new ProdutoPedidoOrcamento();
                    produtoPedidoOrcamento.Id = Convert.ToInt64(item["Id"]);
                    produtoPedidoOrcamento.valor = Convert.ToDouble(item["valor"]);
                    produtoPedidoOrcamento.Quantidade = Convert.ToInt32(item["Quantidade"]);
                    produtoPedidoOrcamento.Ipi = Convert.ToDouble(item["Ipi"]);
                    produtoPedidoOrcamento.Icms = Convert.ToDouble(item["Icms"]);
                    produtoPedidoOrcamento.OrcamentoId = Convert.ToInt64(item["OrcamentosId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacoesId = Convert.ToInt64(item["ProdutoSolicitacoesId"]);
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
                    produtoPedidoOrcamento.Id = Convert.ToInt64(item["Id"]);
                    produtoPedidoOrcamento.valor = Convert.ToDouble(item["valor"]);
                    produtoPedidoOrcamento.Quantidade = Convert.ToInt32(item["Quantidade"]);
                    produtoPedidoOrcamento.Ipi = Convert.ToDouble(item["Ipi"]);
                    produtoPedidoOrcamento.Icms = Convert.ToDouble(item["Icms"]);
                    produtoPedidoOrcamento.OrcamentoId = Convert.ToInt64(item["OrcamentosId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacoesId = Convert.ToInt64(item["ProdutoSolicitacoesId"]);
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

        public bool Update(ProdutoPedidoOrcamento produtoPedidoOrcamento, long id)
        {

            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Update produtoPedidoOrcamento set valor = {produtoPedidoOrcamento.valor.ToString().Replace(",", ".")}, quantidade = {produtoPedidoOrcamento.Quantidade.ToString().Replace(",", ".")}," +
                    $"ipi = {produtoPedidoOrcamento.Ipi.ToString().Replace(",", ".")}, icms = {produtoPedidoOrcamento.Icms.ToString().Replace(",", ".")}, orcamentosId={produtoPedidoOrcamento.OrcamentoId}" +
                    $" where Id = {id} ", conn);
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

        public IEnumerable<ProdutoPedidoOrcamento> GetProdutoOrcamentoSolicitacao(long idSolicitacao)
        {
            try
            {
                List<ProdutoPedidoOrcamento> produtoPedidoOrcamentos = new List<ProdutoPedidoOrcamento>();
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"select p.Id as idProduto, p.CodigoProtheus, p.Descricao, p.GruposId, g.descricao as descGrupo, " +
                    $"g.codigoProtheus as codGrupo, g.id as idGrupo, ppo.OrcamentosId, ppo.ProdutoSolicitacoesId, ppo.Desconto, ppo.ICMS, ppo.IPI, " +
                    $"ppo.Quantidade, ppo.valor, o.Id as idOrcamento, o.Anexo, o.CNPJ, o.Data, o.FormaPagamento, o.Fornecedor, o.TotalIPI, o.TotalProdutos, " +
                    $"o.ValorFrete, o.ValorTotal from produtos as p inner join produtosolicitacoes as ps on ps.ProdutosId = p.id " +
                    $"inner join produtopedidoorcamento as ppo on ppo.ProdutoSolicitacoesId = ps.id inner join orcamentos as o on ppo.orcamentosID = o.id " +
                    $"inner join solicitacaocompras as sc on ps.SolicitacaoComprasId = sc.id inner join grupos as g on p.gruposId = g.id where SolicitacaoComprasId = {idSolicitacao}", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                ProdutoPedidoOrcamento produtoPedidoOrcamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    produtoPedidoOrcamento = new ProdutoPedidoOrcamento();
                    produtoPedidoOrcamento.Id = Convert.ToInt64(item["Id"]);
                    produtoPedidoOrcamento.ProdutoSolicitacoesId = Convert.ToInt64(item["ProdutoSolicitacoesId"]);
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
                    produtoPedidoOrcamento.ProdutoSolicitacao = new ProdutoSolicitacao();
                    produtoPedidoOrcamento.ProdutoSolicitacao.ProdutosId = Convert.ToInt64(item["produtosId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoComprasId = Convert.ToInt64(item["solicitacaoComprasId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto = new Produto();
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Id = Convert.ToInt64(item["IdProduto"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.CodigoProtheus = Convert.ToInt64(item["CodigoProtheus"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Descricao = item["Descricao"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.GrupoId = Convert.ToInt64(item["GruposId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Grupo = new Grupo();
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Grupo.Id = Convert.ToInt64(item["idGrupo"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Grupo.CodigoProtheus = Convert.ToInt64(item["codGrupo"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Grupo.Descricao = item["descGrupo"].ToString();
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

        public bool Remove(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Delete from ProdutoPedidoOrcamento where Id = {id}", conn);
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
            throw new NotImplementedException();
        }

        public IEnumerable<ProdutoPedidoOrcamento> GetOrcamentoSolicitacao(long idSolicitacao)
        {
            throw new NotImplementedException();
        }
    }
}
