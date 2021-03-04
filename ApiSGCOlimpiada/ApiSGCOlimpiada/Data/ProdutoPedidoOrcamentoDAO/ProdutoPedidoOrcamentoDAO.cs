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
                cmd = new MySqlCommand($"INSERT INTO produtopedidoorcamento (valor, Quantidade, IPI, ICMS, Desconto, ProdutoSolicitacoesId, OrcamentosId) " +
                    $"select * from (select {produtoPedidoOrcamento.valor.ToString().Replace(",", ".")} as valor, " +
                    $"{produtoPedidoOrcamento.Quantidade.ToString().Replace(",", ".")} as quantidade, " +
                    $"{produtoPedidoOrcamento.Ipi.ToString().Replace(",", ".")} as ipi, {produtoPedidoOrcamento.Icms.ToString().Replace(",", ".")} as icms, " +
                    $"{produtoPedidoOrcamento.Desconto.ToString().Replace(",", ".")} as desconto, {produtoPedidoOrcamento.ProdutoSolicitacoesId} as ProdutoSolicitacoesId, " +
                    $"{produtoPedidoOrcamento.OrcamentoId} as orcamentosId) as bacon " +
                    $"where not exists(select produtosolicitacoesId from produtopedidoorcamento " +
                    $"where produtosolicitacoesId = {produtoPedidoOrcamento.ProdutoSolicitacoesId} and orcamentosId = {produtoPedidoOrcamento.OrcamentoId}) limit 1", conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return true;
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


        public IEnumerable<ProdutoPedidoOrcamento> GetSolicitacao(long idSolicitacao)
        {
            try
            {
                List<ProdutoPedidoOrcamento> produtoSolicitacoes = new List<ProdutoPedidoOrcamento>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"select ps.id as psId, ps.SolicitacaoComprasId, ps.ProdutosId, p.Id as IdProduto, " +
                    $"p.descricao, p.codigoProtheus, p.gruposId, g.id as idGrupo, g.CodigoProtheus as grupoProtheus, " +
                    $"g.Descricao as descGrupo, sc.Id as idSolicitacao, sc.ResponsavelEntrega, sc.Data, sc.Justificativa,  " +
                    $"sc.Anexo, sc.TipoComprasId, sc.EscolasId, e.Id as idEscola, e.Nome, e.Cep, e.Logradouro, e.bairro, " +
                    $" e.cidade, e.estado, e.numero, tc.id as idTipoCompra, tc.descricao as descTipoCompra, ppo.Desconto, ppo.ICMS," +
                    $" ppo.id as ppoId, ppo.IPI, ppo.OrcamentosId, ppo.ProdutoSolicitacoesId, ppo.Quantidade, ppo.valor, o.id as idOrcamento, " +
                    $" o.Anexo as anexoOrcamento, o.CNPJ, o.Data as dataOrcamento, o.FormaPagamento, o.Fornecedor, o.TotalIPI, o.TotalProdutos, o.ValorFrete, " +
                    $" o.ValorTotal  From produtosolicitacoes as ps  Inner Join produtos as p on ps.ProdutosId = p.Id  " +
                    $"inner join grupos as g on p.GruposId = g.id inner join solicitacaocompras as sc on ps.SolicitacaoComprasId = sc.id " +
                    $" inner join escolas as e on sc.EscolasId = e.id inner join tipocompras as tc on sc.TipoComprasId = tc.id " +
                    $" inner join produtopedidoorcamento as ppo on ppo.ProdutoSolicitacoesId = ps.id " +
                    $" inner join orcamentos as o on ppo.OrcamentosId = o.id where solicitacaoComprasId = { idSolicitacao }", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                ProdutoPedidoOrcamento produtoPedidoOrcamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    produtoPedidoOrcamento = new ProdutoPedidoOrcamento();
                    produtoPedidoOrcamento.Id = Convert.ToInt64(item["ppoId"]);
                    produtoPedidoOrcamento.Desconto = Convert.ToDouble(item["Desconto"]);
                    produtoPedidoOrcamento.Icms = Convert.ToDouble(item["icms"]);
                    produtoPedidoOrcamento.Ipi = Convert.ToDouble(item["ipi"]);
                    produtoPedidoOrcamento.Quantidade = Convert.ToInt32(item["quantidade"]);
                    produtoPedidoOrcamento.valor = Convert.ToDouble(item["valor"]);
                    produtoPedidoOrcamento.OrcamentoId = Convert.ToInt64(item["orcamentosId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacoesId = Convert.ToInt64(item["produtoSolicitacoesId"]);
                    produtoPedidoOrcamento.Orcamento = new Orcamento();
                    produtoPedidoOrcamento.Orcamento.Id = Convert.ToInt64(item["idOrcamento"]);
                    produtoPedidoOrcamento.Orcamento.TotalIpi = Convert.ToDouble(item["totalIpi"]);
                    produtoPedidoOrcamento.Orcamento.ValorFrete = Convert.ToDouble(item["ValorFrete"]);
                    produtoPedidoOrcamento.Orcamento.TotalProdutos = Convert.ToDouble(item["totalProdutos"]);
                    produtoPedidoOrcamento.Orcamento.ValorTotal = Convert.ToDouble(item["totalProdutos"]);
                    produtoPedidoOrcamento.Orcamento.Data = Convert.ToDateTime(item["dataOrcamento"]);
                    produtoPedidoOrcamento.Orcamento.Anexo = item["anexoOrcamento"].ToString();
                    produtoPedidoOrcamento.Orcamento.Cnpj = item["cnpj"].ToString();
                    produtoPedidoOrcamento.Orcamento.FormaPagamento = item["FormaPagamento"].ToString();
                    produtoPedidoOrcamento.Orcamento.Fornecedor = item["Fornecedor"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao = new ProdutoSolicitacao();
                    produtoPedidoOrcamento.ProdutoSolicitacao.Id = Convert.ToInt64(item["psId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.ProdutosId = Convert.ToInt64(item["produtosId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoComprasId = Convert.ToInt64(item["SolicitacaoComprasId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto = new Produto();
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Id = Convert.ToInt64(item["IdPRoduto"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.CodigoProtheus = Convert.ToInt64(item["codigoProtheus"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Descricao = item["Descricao"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.GrupoId = Convert.ToInt64(item["gruposId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Grupo = new Grupo();
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Grupo.Id = Convert.ToInt64(item["IdPRoduto"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Grupo.CodigoProtheus = Convert.ToInt64(item["grupoProtheus"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Grupo.Descricao = item["descGrupo"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra = new SolicitacaoCompra();
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.Id = Convert.ToInt64(item["idSolicitacao"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.TipoCompraId = Convert.ToInt64(item["tipoComprasId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.Data = Convert.ToDateTime(item["data"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.Justificativa = item["justificativa"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.ResponsavelEntrega = item["ResponsavelEntrega"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.TipoCompra = new TipoCompra();
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.TipoCompra.Id = Convert.ToInt64(item["IdtipoCompra"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.TipoCompra.Descricao = item["descTipoCompra"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.Escola = new Escola();
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.Escola.Id = Convert.ToInt64(item["IdEscola"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.Escola.Nome = item["Nome"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.Escola.Cep = item["cep"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.Escola.Logradouro = item["Logradouro"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.Escola.Bairro = item["Bairro"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.Escola.Numero = item["numero"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.Escola.Estado = item["estado"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoCompra.Escola.Cidade = item["cidade"].ToString();
                    produtoSolicitacoes.Add(produtoPedidoOrcamento);
                }
                return produtoSolicitacoes;
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

        public IEnumerable<ProdutoPedidoOrcamento> GetDadosProddutoBySolicitacao(long idSolicitacao)
        {
            try
            {
                List<ProdutoPedidoOrcamento> produtoSolicitacoes = new List<ProdutoPedidoOrcamento>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"select * from produtosolicitacoes ps inner join produtos p on ps.ProdutosId = p.id left join orcamento1 o1 on o1.ProdutoSolicitacoesId = ps.id left join orcamento2 o2 on o2.ProdutoSolicitacoesId = ps.id left join orcamento3 o3 on o3.ProdutoSolicitacoesId = ps.id where ps.SolicitacaoComprasId = { idSolicitacao }", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                ProdutoPedidoOrcamento produtoPedidoOrcamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    produtoPedidoOrcamento = new ProdutoPedidoOrcamento();
                    produtoPedidoOrcamento.Id = Convert.ToInt64(item["ppoId"]);
                    produtoPedidoOrcamento.Desconto = Convert.ToDouble(item["Desconto"]);
                    produtoPedidoOrcamento.Icms = Convert.ToDouble(item["icms"]);
                    produtoPedidoOrcamento.Ipi = Convert.ToDouble(item["ipi"]);
                    produtoPedidoOrcamento.Quantidade = Convert.ToInt32(item["quantidade"]);
                    produtoPedidoOrcamento.valor = Convert.ToDouble(item["valor"]);
                    produtoPedidoOrcamento.OrcamentoId = Convert.ToInt64(item["orcamentosId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacoesId = Convert.ToInt64(item["produtoSolicitacoesId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao = new ProdutoSolicitacao();
                    produtoPedidoOrcamento.ProdutoSolicitacao.Id = Convert.ToInt64(item["psId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.ProdutosId = Convert.ToInt64(item["produtosId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.SolicitacaoComprasId = Convert.ToInt64(item["SolicitacaoComprasId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto = new Produto();
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Id = Convert.ToInt64(item["IdPRoduto"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.CodigoProtheus = Convert.ToInt64(item["codigoProtheus"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Descricao = item["Descricao"].ToString();
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.GrupoId = Convert.ToInt64(item["gruposId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Grupo = new Grupo();
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Grupo.Id = Convert.ToInt64(item["IdPRoduto"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Grupo.CodigoProtheus = Convert.ToInt64(item["grupoProtheus"]);
                    produtoPedidoOrcamento.ProdutoSolicitacao.Produto.Grupo.Descricao = item["descGrupo"].ToString();
                    
                    produtoSolicitacoes.Add(produtoPedidoOrcamento);
                }
                return produtoSolicitacoes;
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
