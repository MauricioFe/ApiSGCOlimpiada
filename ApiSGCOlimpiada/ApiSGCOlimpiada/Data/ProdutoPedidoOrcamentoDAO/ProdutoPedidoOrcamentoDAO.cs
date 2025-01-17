﻿using ApiSGCOlimpiada.Models;
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
                    produtoPedidoOrcamento.valor = Convert.ToDecimal(item["valor"]);
                    produtoPedidoOrcamento.Quantidade = Convert.ToDecimal(item["Quantidade"]);
                    produtoPedidoOrcamento.Ipi = Convert.ToDecimal(item["Ipi"]);
                    produtoPedidoOrcamento.Icms = Convert.ToDecimal(item["Icms"]);
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
                    produtoPedidoOrcamento.valor = Convert.ToDecimal(item["valor"]);
                    produtoPedidoOrcamento.Quantidade = Convert.ToDecimal(item["Quantidade"]);
                    produtoPedidoOrcamento.Ipi = Convert.ToDecimal(item["Ipi"]);
                    produtoPedidoOrcamento.Icms = Convert.ToDecimal(item["Icms"]);
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
                    produtoPedidoOrcamento.valor = Convert.ToDecimal(item["valor"]);
                    produtoPedidoOrcamento.Quantidade = Convert.ToDecimal(item["Quantidade"]);
                    produtoPedidoOrcamento.Ipi = Convert.ToDecimal(item["Ipi"]);
                    produtoPedidoOrcamento.Icms = Convert.ToDecimal(item["Icms"]);
                    produtoPedidoOrcamento.OrcamentoId = Convert.ToInt64(item["OrcamentosId"]);
                    produtoPedidoOrcamento.Orcamento = new Orcamento();
                    produtoPedidoOrcamento.Orcamento.Id = Convert.ToInt64(item["IdOrcamento"]);
                    produtoPedidoOrcamento.Orcamento.Anexo = item["Anexo"].ToString();
                    produtoPedidoOrcamento.Orcamento.Cnpj = item["CNPJ"].ToString();
                    produtoPedidoOrcamento.Orcamento.Fornecedor = item["Fornecedor"].ToString();
                    produtoPedidoOrcamento.Orcamento.Data = Convert.ToDateTime(item["Data"]);
                    produtoPedidoOrcamento.Orcamento.FormaPagamento = item["FormaPagamento"].ToString();
                    produtoPedidoOrcamento.Orcamento.TotalIpi = Convert.ToDecimal(item["TotalIPI"]);
                    produtoPedidoOrcamento.Orcamento.TotalProdutos = Convert.ToDecimal(item["TotalProdutos"]);
                    produtoPedidoOrcamento.Orcamento.ValorTotal = Convert.ToDecimal(item["ValorTotal"]);
                    produtoPedidoOrcamento.Orcamento.ValorFrete = Convert.ToDecimal(item["ValorFrete"]);
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
                    produtoPedidoOrcamento.Desconto = Convert.ToDecimal(item["Desconto"]);
                    produtoPedidoOrcamento.Icms = Convert.ToDecimal(item["icms"]);
                    produtoPedidoOrcamento.Ipi = Convert.ToDecimal(item["ipi"]);
                    produtoPedidoOrcamento.Quantidade = Convert.ToDecimal(item["quantidade"]);
                    produtoPedidoOrcamento.valor = Convert.ToDecimal(item["valor"]);
                    produtoPedidoOrcamento.OrcamentoId = Convert.ToInt64(item["orcamentosId"]);
                    produtoPedidoOrcamento.ProdutoSolicitacoesId = Convert.ToInt64(item["produtoSolicitacoesId"]);
                    produtoPedidoOrcamento.Orcamento = new Orcamento();
                    produtoPedidoOrcamento.Orcamento.Id = Convert.ToInt64(item["idOrcamento"]);
                    produtoPedidoOrcamento.Orcamento.TotalIpi = Convert.ToDecimal(item["totalIpi"]);
                    produtoPedidoOrcamento.Orcamento.ValorFrete = Convert.ToDecimal(item["ValorFrete"]);
                    produtoPedidoOrcamento.Orcamento.TotalProdutos = Convert.ToDecimal(item["totalProdutos"]);
                    produtoPedidoOrcamento.Orcamento.ValorTotal = Convert.ToDecimal(item["totalProdutos"]);
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

        public IEnumerable<Planilha> GetDadosProdutoBySolicitacao(long idSolicitacao)
        {
            try
            {
                List<ProdutoPedidoOrcamento> produtoSolicitacoes = new List<ProdutoPedidoOrcamento>();
                List<Planilha> planilhaList = new List<Planilha>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"select * from produtosolicitacoes ps inner join produtos p on ps.ProdutosId = p.id inner join grupos g on g.Id = p.GruposId left join orcamento1 o1 on o1.ProdutoSolicitacoesId = ps.id left join orcamento2 o2 on o2.ProdutoSolicitacoesId = ps.id left join orcamento3 o3 on o3.ProdutoSolicitacoesId = ps.id where ps.SolicitacaoComprasId = { idSolicitacao }; ", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                ProdutoPedidoOrcamento produtoPedidoOrcamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    produtoPedidoOrcamento = new ProdutoPedidoOrcamento();
                    ProdutoPedidoOrcamento produtoPedidoOrcamento2 = new ProdutoPedidoOrcamento();
                    ProdutoPedidoOrcamento produtoPedidoOrcamento3 = new ProdutoPedidoOrcamento();
                    Planilha planilha = new Planilha();
                    planilha.Produto = new Produto();
                    planilha.Produto.Id = Convert.ToInt64(item[3]);
                    planilha.Produto.CodigoProtheus = Convert.ToInt64(item[4]);
                    planilha.Produto.Descricao = item[5].ToString();
                    planilha.Produto.GrupoId = Convert.ToInt64(item[6]);
                    planilha.Produto.Grupo = new Grupo();
                    planilha.Produto.Grupo.Id = Convert.ToInt64(item[7]);
                    planilha.Produto.Grupo.CodigoProtheus = Convert.ToInt64(item[8]);
                    planilha.Produto.Grupo.Descricao = item[9].ToString();

                    produtoPedidoOrcamento.Id = Convert.ToInt64(item[10]);
                    produtoPedidoOrcamento.valor = Convert.ToDecimal(item[11]);
                    produtoPedidoOrcamento.Quantidade = Convert.ToDecimal(item[12]);
                    produtoPedidoOrcamento.Ipi = Convert.ToDecimal(item[13]);
                    produtoPedidoOrcamento.Icms = Convert.ToDecimal(item[14]);
                    produtoPedidoOrcamento.Desconto = Convert.ToDecimal(item[15]);
                    produtoPedidoOrcamento.ProdutoSolicitacoesId = Convert.ToInt64(item[16]);
                    produtoPedidoOrcamento.OrcamentoId = Convert.ToInt64(item[17]);
                    produtoPedidoOrcamento.TotalItem = Convert.ToDecimal(item[18]);
                    planilha.ProdutoPedidoOrcamentosList = new List<ProdutoPedidoOrcamento>();

                    if (DBNull.Value != item[19])
                    {

                        produtoPedidoOrcamento2.Id = Convert.ToInt64(item[19]);
                        produtoPedidoOrcamento2.valor = Convert.ToDecimal(item[20]);
                        produtoPedidoOrcamento2.Quantidade = Convert.ToInt32(item[21]);
                        produtoPedidoOrcamento2.Ipi = Convert.ToDecimal(item[22]);
                        produtoPedidoOrcamento2.Icms = Convert.ToDecimal(item[23]);
                        produtoPedidoOrcamento2.Desconto = Convert.ToDecimal(item[24]);
                        produtoPedidoOrcamento2.ProdutoSolicitacoesId = Convert.ToInt64(item[25]);
                        produtoPedidoOrcamento2.OrcamentoId = Convert.ToInt64(item[26]);
                        produtoPedidoOrcamento2.TotalItem = Convert.ToDecimal(item[27]);

                        produtoPedidoOrcamento3.Id = Convert.ToInt64(item[28]);
                        produtoPedidoOrcamento3.valor = Convert.ToDecimal(item[29]);
                        produtoPedidoOrcamento3.Quantidade = Convert.ToInt32(item[30]);
                        produtoPedidoOrcamento3.Ipi = Convert.ToDecimal(item[31]);
                        produtoPedidoOrcamento3.Icms = Convert.ToDecimal(item[32]);
                        produtoPedidoOrcamento3.Desconto = Convert.ToDecimal(item[33]);
                        produtoPedidoOrcamento3.ProdutoSolicitacoesId = Convert.ToInt64(item[34]);
                        produtoPedidoOrcamento3.OrcamentoId = Convert.ToInt64(item[35]);
                        produtoPedidoOrcamento3.TotalItem = Convert.ToDecimal(item[36]);
                        planilha.ProdutoPedidoOrcamentosList.Add(produtoPedidoOrcamento2);
                        planilha.ProdutoPedidoOrcamentosList.Add(produtoPedidoOrcamento3);
                    }

                    planilha.ProdutoPedidoOrcamentosList.Add(produtoPedidoOrcamento);

                    planilhaList.Add(planilha);
                }
                return planilhaList;
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
