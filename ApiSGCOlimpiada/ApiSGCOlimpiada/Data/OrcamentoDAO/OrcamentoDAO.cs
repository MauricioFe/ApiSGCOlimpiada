using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.OrcamentoDAO
{
    public class OrcamentoDAO : IOrcamentoDAO
    {
        private readonly string _conn;
        public OrcamentoDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        public bool Add(Orcamento orcamento)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"insert into orcamentos values (null, '{orcamento.Fornecedor}', '{orcamento.Cnpj}', {orcamento.ValorTotal.ToString().Replace(",", ".")}," +
                    $"{orcamento.TotalIpi.ToString().Replace(",", ".")}, {orcamento.TotalProdutos.ToString().Replace(",", ".")}, '{orcamento.Anexo}', '{orcamento.Data.ToString("yyyy-MM-dd HH:mm")}', '{orcamento.FormaPagamento}', {orcamento.ValorFrete.ToString().Replace(",", ".")},'{orcamento.OrderFlag}')", conn);
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

        public Orcamento Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Select * from orcamentos where id = {id}", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Orcamento orcamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    orcamento = new Orcamento();
                    orcamento.Id = Convert.ToInt64(item["id"]);
                    orcamento.Fornecedor = item["fornecedor"].ToString();
                    orcamento.Cnpj = item["cnpj"].ToString();
                    orcamento.ValorTotal = Convert.ToDecimal(item["valorTotal"]);
                    orcamento.TotalIpi = Convert.ToDecimal(item["TotalIpi"]);
                    orcamento.TotalProdutos = Convert.ToDecimal(item["TotalProdutos"]);
                    orcamento.Anexo = item["anexo"].ToString();
                    orcamento.FormaPagamento = item["FormaPagamento"].ToString();
                    orcamento.Data = Convert.ToDateTime(item["data"]);
                }
                return orcamento;
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

        public IEnumerable<Orcamento> GetAll()
        {
            try
            {
                List<Orcamento> orcamentos = new List<Orcamento>();
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Select * from orcamentos", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Orcamento orcamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    orcamento = new Orcamento();
                    orcamento.Id = Convert.ToInt64(item["id"]);
                    orcamento.Fornecedor = item["fornecedor"].ToString();
                    orcamento.Cnpj = item["cnpj"].ToString();
                    orcamento.ValorTotal = Convert.ToDecimal(item["valorTotal"]);
                    orcamento.TotalIpi = Convert.ToDecimal(item["TotalIpi"]);
                    orcamento.TotalProdutos = Convert.ToDecimal(item["TotalProdutos"]);
                    orcamento.Anexo = item["anexo"].ToString();
                    orcamento.Data = Convert.ToDateTime(item["data"]);
                    orcamento.FormaPagamento = item["FormaPagamento"].ToString();
                    orcamentos.Add(orcamento);
                }
                return orcamentos;
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

        public bool Update(Orcamento orcamento, long id)
        {

            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Update orcamentos set fornecedor = '{orcamento.Fornecedor}', cnpj = '{orcamento.Cnpj}', valorTotal = {orcamento.ValorTotal.ToString().Replace(",", ".")}," +
                    $"totalIpi = {orcamento.TotalIpi.ToString().Replace(",", ".")}, totalProdutos = {orcamento.TotalProdutos.ToString().Replace(",", ".")}, anexo = '{orcamento.Anexo}', " +
                    $"data = '{orcamento.Data.ToString("yyyy-MM-dd HH:mm")}', FormaPagamento = '{orcamento.FormaPagamento}', ValorFrete = {orcamento.ValorFrete.ToString().Replace(",", ".")}" +
                    $" where id = {id}", conn);
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
        public IEnumerable<Orcamento> GetOrcamentoBySolicitacao(long idSolicitacao)
        {
            try
            {
                List<Orcamento> orcamentoSolicitacoes = new List<Orcamento>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"SELECT o.id AS idOrcamento, o.Anexo, o.CNPJ, o.Data AS dataOrcamento, o.FormaPagamento, o.Fornecedor, " +
                    $"o.TotalIPI, o.TotalProdutos, o.ValorFrete, o.ValorTotal FROM produtosolicitacoes AS ps " +
                    $"INNER JOIN produtos AS p ON ps.ProdutosId = p.Id INNER JOIN grupos AS g ON p.GruposId = g.id " +
                    $"INNER JOIN solicitacaocompras AS sc ON ps.SolicitacaoComprasId = sc.id INNER JOIN escolas AS e ON sc.EscolasId = e.id " +
                    $"INNER JOIN tipocompras AS tc ON sc.TipoComprasId = tc.id INNER JOIN produtopedidoorcamento AS ppo ON ppo.ProdutoSolicitacoesId = ps.id " +
                    $"INNER JOIN orcamentos AS o ON ppo.OrcamentosId = o.id WHERE solicitacaoComprasId = { idSolicitacao } group by o.id", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Orcamento orcamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    orcamento = new Orcamento();
                    orcamento.Id = Convert.ToInt64(item["idOrcamento"]);
                    orcamento.TotalIpi = Convert.ToDecimal(item["totalIpi"]);
                    orcamento.ValorFrete = Convert.ToDecimal(item["ValorFrete"]);
                    orcamento.TotalProdutos = Convert.ToDecimal(item["totalProdutos"]);
                    orcamento.ValorTotal = Convert.ToDecimal(item["totalProdutos"]);
                    orcamento.Data = Convert.ToDateTime(item["dataOrcamento"]);
                    orcamento.Anexo = item["anexo"].ToString();
                    orcamento.Cnpj = item["cnpj"].ToString();
                    orcamento.FormaPagamento = item["FormaPagamento"].ToString();
                    orcamento.Fornecedor = item["Fornecedor"].ToString();
                    orcamentoSolicitacoes.Add(orcamento);
                }
                return orcamentoSolicitacoes;
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
