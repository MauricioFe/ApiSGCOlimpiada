using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.ProdutoSolicitacoesDAO
{
    public class ProdutoSolicitacoesDAO : IProdutoSolicitacoesDAO
    {
        private readonly string _conn;

        public ProdutoSolicitacoesDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        DataTable dt;

        public bool Add(ProdutoSolicitacao produtoSolicitacao)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into ProdutoSolicitacoes values (null, {produtoSolicitacao.SolicitacaoComprasId}, {produtoSolicitacao.ProdutosId})", conn);
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

        public ProdutoSolicitacao Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"select ps.id as psId, ps.SolicitacaoComprasId, ps.ProdutosId,p.Id as IdProduto, p.descricao, " +
                    $"p.codigoProtheus, p.gruposId, g.id as idGrupo, g.CodigoProtheus as grupoProtheus, g.Descricao as descGrupo, " +
                    $"sc.Id as idSolicitacao, sc.ResponsavelEntrega, sc.Data, sc.Justificativa, sc.Anexo, sc.TipoComprasId, " +
                    $"sc.EscolasId, e.Id as idEscolas, e.Nome, e.Cep, e.Logradouro, e.bairro, e.cidade, e.estado, e.numero, " +
                    $"tc.id as idTipoCompra, tc.descricao as descTipoCompra From produtosolicitacoes as ps Inner Join produtos as p on ps.ProdutosId = p.Id " +
                    $"inner join grupos as g on p.GruposId = g.id inner join solicitacaocompras as sc on ps.SolicitacaoComprasId = sc.id " +
                    $"inner join escolas as e on sc.EscolasId = e.id inner join tipocompras as tc on sc.TipoComprasId = tc.id where psid = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                ProdutoSolicitacao produtoSolicitacao = null;
                foreach (DataRow item in dt.Rows)
                {
                    produtoSolicitacao = new ProdutoSolicitacao();
                    produtoSolicitacao.Id = Convert.ToInt64(item["psId"]);
                    produtoSolicitacao.ProdutosId = Convert.ToInt64(item["produtosId"]);
                    produtoSolicitacao.SolicitacaoComprasId = Convert.ToInt64(item["SolicitacaoComprasId"]);
                    produtoSolicitacao.Produto = new Produto();
                    produtoSolicitacao.Produto.Id = Convert.ToInt64(item["IdPRoduto"]);
                    produtoSolicitacao.Produto.CodigoProtheus = Convert.ToInt64(item["codigoProtheus"]);
                    produtoSolicitacao.Produto.Descricao = item["Descricao"].ToString();
                    produtoSolicitacao.Produto.GrupoId = Convert.ToInt64(item["gruposId"]);
                    produtoSolicitacao.Produto.Grupo = new Grupo();
                    produtoSolicitacao.Produto.Grupo.Id = Convert.ToInt64(item["IdPRoduto"]);
                    produtoSolicitacao.Produto.Grupo.CodigoProtheus = Convert.ToInt64(item["grupoProtheus"]);
                    produtoSolicitacao.Produto.Grupo.Descricao = item["descGrupo"].ToString();
                    produtoSolicitacao.SolicitacaoCompra = new SolicitacaoCompra();
                    produtoSolicitacao.SolicitacaoCompra.Id = Convert.ToInt64(item["idSolicitacao"]);
                    produtoSolicitacao.SolicitacaoCompra.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    produtoSolicitacao.SolicitacaoCompra.TipoCompraId = Convert.ToInt64(item["tipoComprasId"]);
                    produtoSolicitacao.SolicitacaoCompra.Justificativa = item["justificativa"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.ResponsavelEntrega = item["ResponsavelEntrega"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.TipoCompra = new TipoCompra();
                    produtoSolicitacao.SolicitacaoCompra.TipoCompra.Id = Convert.ToInt64(item["IdtipoCompra"]);
                    produtoSolicitacao.SolicitacaoCompra.TipoCompra.Descricao = item["descTipoCompra"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola = new Escola();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Id = Convert.ToInt64(item["IdEscola"]);
                    produtoSolicitacao.SolicitacaoCompra.Escola.Nome = item["Nome"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Cep = item["cep"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Logradouro = item["Logradouro"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Bairro = item["Bairro"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Numero = item["numero"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Estado = item["estado"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Cidade = item["cidade"].ToString();
                }
                return produtoSolicitacao;
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

        public IEnumerable<ProdutoSolicitacao> FindBySolicitacao(long solicitacaoId)
        {
            try
            {
                List<ProdutoSolicitacao> produtoSolicitacoes = new List<ProdutoSolicitacao>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"select ps.id as psId, ps.SolicitacaoComprasId, ps.ProdutosId,p.Id as IdProduto, p.descricao, " +
                    $"p.codigoProtheus, p.gruposId, g.id as idGrupo, g.CodigoProtheus as grupoProtheus, g.Descricao as descGrupo, " +
                    $"sc.Id as idSolicitacao, sc.ResponsavelEntrega, sc.Data, sc.Justificativa, sc.Anexo, sc.TipoComprasId, " +
                    $"sc.EscolasId, e.Id as idEscola, e.Nome, e.Cep, e.Logradouro, e.bairro, e.cidade, e.estado, e.numero, " +
                    $"tc.id as idTipoCompra, tc.descricao as descTipoCompra From produtosolicitacoes as ps Inner Join produtos as p on ps.ProdutosId = p.Id " +
                    $"inner join grupos as g on p.GruposId = g.id inner join solicitacaocompras as sc on ps.SolicitacaoComprasId = sc.id " +
                    $"inner join escolas as e on sc.EscolasId = e.id inner join tipocompras as tc on sc.TipoComprasId = tc.id where ps.SolicitacaoComprasId = {solicitacaoId}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                ProdutoSolicitacao produtoSolicitacao = null;
                foreach (DataRow item in dt.Rows)
                {
                    produtoSolicitacao = new ProdutoSolicitacao();
                    produtoSolicitacao.Id = Convert.ToInt64(item["psId"]);
                    produtoSolicitacao.ProdutosId = Convert.ToInt64(item["produtosId"]);
                    produtoSolicitacao.SolicitacaoComprasId = Convert.ToInt64(item["SolicitacaoComprasId"]);
                    produtoSolicitacao.Produto = new Produto();
                    produtoSolicitacao.Produto.Id = Convert.ToInt64(item["IdPRoduto"]);
                    produtoSolicitacao.Produto.CodigoProtheus = Convert.ToInt64(item["codigoProtheus"]);
                    produtoSolicitacao.Produto.Descricao = item["Descricao"].ToString();
                    produtoSolicitacao.Produto.GrupoId = Convert.ToInt64(item["gruposId"]);
                    produtoSolicitacao.Produto.Grupo = new Grupo();
                    produtoSolicitacao.Produto.Grupo.Id = Convert.ToInt64(item["IdPRoduto"]);
                    produtoSolicitacao.Produto.Grupo.CodigoProtheus = Convert.ToInt64(item["grupoProtheus"]);
                    produtoSolicitacao.Produto.Grupo.Descricao = item["descGrupo"].ToString();
                    produtoSolicitacao.SolicitacaoCompra = new SolicitacaoCompra();
                    produtoSolicitacao.SolicitacaoCompra.Id = Convert.ToInt64(item["idSolicitacao"]);
                    produtoSolicitacao.SolicitacaoCompra.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    produtoSolicitacao.SolicitacaoCompra.TipoCompraId = Convert.ToInt64(item["tipoComprasId"]);
                    produtoSolicitacao.SolicitacaoCompra.Data = Convert.ToDateTime(item["data"]);
                    produtoSolicitacao.SolicitacaoCompra.Justificativa = item["justificativa"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.ResponsavelEntrega = item["ResponsavelEntrega"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.TipoCompra = new TipoCompra();
                    produtoSolicitacao.SolicitacaoCompra.TipoCompra.Id = Convert.ToInt64(item["IdtipoCompra"]);
                    produtoSolicitacao.SolicitacaoCompra.TipoCompra.Descricao = item["descTipoCompra"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola = new Escola();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Id = Convert.ToInt64(item["IdEscola"]);
                    produtoSolicitacao.SolicitacaoCompra.Escola.Nome = item["Nome"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Cep = item["cep"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Logradouro = item["Logradouro"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Bairro = item["Bairro"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Numero = item["numero"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Estado = item["estado"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Cidade = item["cidade"].ToString();
                    produtoSolicitacoes.Add(produtoSolicitacao);
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

        public IEnumerable<ProdutoSolicitacao> GetAll()
        {
            try
            {
                List<ProdutoSolicitacao> produtoSolicitacoes = new List<ProdutoSolicitacao>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"select ps.id as psId, ps.SolicitacaoComprasId, ps.ProdutosId,p.Id as IdProduto, p.descricao, " +
                    $"p.codigoProtheus, p.gruposId, g.id as idGrupo, g.CodigoProtheus as grupoProtheus, g.Descricao as descGrupo, " +
                    $"sc.Id as idSolicitacao, sc.ResponsavelEntrega, sc.Data, sc.Justificativa, sc.Anexo, sc.TipoComprasId, " +
                    $"sc.EscolasId, e.Id as idEscola, e.Nome, e.Cep, e.Logradouro, e.bairro, e.cidade, e.estado, e.numero, " +
                    $"tc.id as idTipoCompra, tc.descricao as descTipoCompra From produtosolicitacoes as ps Inner Join produtos as p on ps.ProdutosId = p.Id " +
                    $"inner join grupos as g on p.GruposId = g.id inner join solicitacaocompras as sc on ps.SolicitacaoComprasId = sc.id " +
                    $"inner join escolas as e on sc.EscolasId = e.id inner join tipocompras as tc on sc.TipoComprasId = tc.id", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                ProdutoSolicitacao produtoSolicitacao = null;
                foreach (DataRow item in dt.Rows)
                {
                    produtoSolicitacao = new ProdutoSolicitacao();
                    produtoSolicitacao.Id = Convert.ToInt64(item["psId"]);
                    produtoSolicitacao.ProdutosId = Convert.ToInt64(item["produtosId"]);
                    produtoSolicitacao.SolicitacaoComprasId = Convert.ToInt64(item["SolicitacaoComprasId"]);
                    produtoSolicitacao.Produto = new Produto();
                    produtoSolicitacao.Produto.Id = Convert.ToInt64(item["IdPRoduto"]);
                    produtoSolicitacao.Produto.CodigoProtheus = Convert.ToInt64(item["codigoProtheus"]);
                    produtoSolicitacao.Produto.Descricao = item["Descricao"].ToString();
                    produtoSolicitacao.Produto.GrupoId = Convert.ToInt64(item["gruposId"]);
                    produtoSolicitacao.Produto.Grupo = new Grupo();
                    produtoSolicitacao.Produto.Grupo.Id = Convert.ToInt64(item["IdPRoduto"]);
                    produtoSolicitacao.Produto.Grupo.CodigoProtheus = Convert.ToInt64(item["grupoProtheus"]);
                    produtoSolicitacao.Produto.Grupo.Descricao = item["descGrupo"].ToString();
                    produtoSolicitacao.SolicitacaoCompra = new SolicitacaoCompra();
                    produtoSolicitacao.SolicitacaoCompra.Id = Convert.ToInt64(item["idSolicitacao"]);
                    produtoSolicitacao.SolicitacaoCompra.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    produtoSolicitacao.SolicitacaoCompra.TipoCompraId = Convert.ToInt64(item["tipoComprasId"]);
                    produtoSolicitacao.SolicitacaoCompra.Justificativa = item["justificativa"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.ResponsavelEntrega = item["ResponsavelEntrega"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.TipoCompra = new TipoCompra();
                    produtoSolicitacao.SolicitacaoCompra.TipoCompra.Id = Convert.ToInt64(item["IdtipoCompra"]);
                    produtoSolicitacao.SolicitacaoCompra.TipoCompra.Descricao = item["descTipoCompra"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola = new Escola();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Id = Convert.ToInt64(item["IdEscola"]);
                    produtoSolicitacao.SolicitacaoCompra.Escola.Nome = item["Nome"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Cep = item["cep"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Logradouro = item["Logradouro"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Bairro = item["Bairro"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Numero = item["numero"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Estado = item["estado"].ToString();
                    produtoSolicitacao.SolicitacaoCompra.Escola.Cidade = item["cidade"].ToString();
                    produtoSolicitacoes.Add(produtoSolicitacao);
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

        public bool Remove(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Delete form ProdutoSolicitacoes where id = {id}", conn);
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

        public bool Update(ProdutoSolicitacao produtoSolicitacao, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update ProdutoSolicitacoes set solicitacaoComprasId = {produtoSolicitacao.SolicitacaoComprasId}, produtosId = {produtoSolicitacao.SolicitacaoComprasId} where id = {id}", conn);
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
