using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.ProdutoDAO
{
    public class ProdutoDAO : IProdutoDAO
    {
        private readonly string _conn;
        public ProdutoDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        DataTable dt;

        public void Add(Produto produto)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into Produtos values(null, {produto.CodigoProtheus}, '{produto.Descricao}', {produto.GrupoId})", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public Produto FindByProtheus(long codigoProtheus)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Produtos where codigoProtheus = {codigoProtheus}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Produto produto = new Produto();
                foreach (DataRow item in dt.Rows)
                {
                    produto.Id = Convert.ToInt64(item["Id"]);
                    produto.CodigoProtheus = int.Parse(item["CodigoProtheus"].ToString());
                    produto.Descricao = item["Descricao"].ToString();
                    produto.GrupoId = Convert.ToInt64(item["gruposID"]);
                }
                return produto;
            }
            catch
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public Produto Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Produtos where id = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Produto produto = new Produto();
                foreach (DataRow item in dt.Rows)
                {
                    produto.Id = Convert.ToInt64(item["Id"]);
                    produto.CodigoProtheus = int.Parse(item["CodigoProtheus"].ToString());
                    produto.Descricao = item["Descricao"].ToString();
                    produto.GrupoId = Convert.ToInt64(item["gruposID"]);
                }
                return produto;
            }
            catch
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public IEnumerable<Produto> GetAll()
        {
            try
            {
                List<Produto> produtos = new List<Produto>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select * from Produtos", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Produto produto = new Produto();
                    produto.Id = Convert.ToInt64(item["Id"]);
                    produto.CodigoProtheus = int.Parse(item["CodigoProtheus"].ToString());
                    produto.Descricao = item["Descricao"].ToString();
                    produto.GrupoId = Convert.ToInt64(item["gruposID"]);
                    produtos.Add(produto);
                }
                return produtos;
            }
            catch
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Remove(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Delete from Produtos where id = {id}", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Produto produto, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update Produtos set CodigoProtheus = {produto.CodigoProtheus}, descricao = '{produto.Descricao}', GruposId = {produto.GrupoId}where id = {id}", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
