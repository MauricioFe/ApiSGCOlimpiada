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
                cmd = new MySqlCommand($"insert into orcamentos values (null, '{orcamento.Fornecedor}', '{orcamento.Cnpj}', {orcamento.ValorTotal}," +
                    $"{orcamento.TotalIpi}, {orcamento.TotalProdutos}, '{orcamento.Anexo}', '{orcamento.Data.ToString("yyyy-MM-dd HH:mm")}')", conn);
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
                    orcamento.ValorTotal = Convert.ToDouble(item["valorTotal"]);
                    orcamento.TotalIpi = Convert.ToDouble(item["TotalIpi"]);
                    orcamento.TotalProdutos = Convert.ToDouble(item["TotalProdutos"]);
                    orcamento.Anexo = item["anexo"].ToString();
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
                    orcamento.ValorTotal = Convert.ToDouble(item["valorTotal"]);
                    orcamento.TotalIpi = Convert.ToDouble(item["TotalIpi"]);
                    orcamento.TotalProdutos = Convert.ToDouble(item["TotalProdutos"]);
                    orcamento.Anexo = item["anexo"].ToString();
                    orcamento.Data = Convert.ToDateTime(item["data"]);
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
                cmd = new MySqlCommand($"Update orcamentos set fornecedor = '{orcamento.Fornecedor}', cnpj = '{orcamento.Cnpj}', valorTotal = {orcamento.ValorTotal}," +
                    $"totalIpi = {orcamento.TotalIpi}, totalProdutos = {orcamento.TotalProdutos}, anexo = '{orcamento.Anexo}', data = {orcamento.Data.ToString("yyyy-MM-dd HH:mm")}" +
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
    }
}
