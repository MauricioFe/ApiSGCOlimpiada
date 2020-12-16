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

        public bool Add(Produto produto)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into Produtos values(null, {produto.CodigoProtheus}, '{produto.Descricao}', {produto.GrupoId})", conn);
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

        public List<Produto> FindBySearch(string search)
        {
            try
            {
                List<Produto> produtos = new List<Produto>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select produtos.id, produtos.CodigoProtheus, produtos.Descricao, produtos.gruposId," +
                    $" grupos.id as idGrupos, grupos.CodigoProtheus as protheusGrupo, grupos.Descricao as descricaoGrupo" +
                    $" from Produtos inner join grupos on produtos.gruposId = grupos.id " +
                    $" where codigoProtheus LIKE '{search}%' or descricao like '%{search}%'", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Produto produto = null;
                foreach (DataRow item in dt.Rows)
                {
                    produto = new Produto();
                    produto.Id = Convert.ToInt64(item["Id"]);
                    produto.CodigoProtheus = int.Parse(item["CodigoProtheus"].ToString());
                    produto.Descricao = item["Descricao"].ToString();
                    produto.GrupoId = Convert.ToInt64(item["gruposID"]);
                    produto.Grupo = new Grupo();
                    produto.Grupo.Id = Convert.ToInt64(item["idGrupos"]);
                    produto.Grupo.CodigoProtheus = Convert.ToInt64(item["protheusGrupo"]);
                    produto.Grupo.Descricao = item["descricaoGrupo"].ToString();
                    produtos.Add(produto);
                }
                return produtos;
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
        public Produto Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select produtos.id, produtos.CodigoProtheus, produtos.Descricao, produtos.gruposId," +
                    $" grupos.id as idGrupos, grupos.CodigoProtheus as protheusGrupo, grupos.Descricao as descricaoGrupo" +
                    $" from Produtos inner join grupos on produtos.gruposId = grupos.id where id = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Produto produto = null;
                foreach (DataRow item in dt.Rows)
                {
                    produto = new Produto();
                    produto.Id = Convert.ToInt64(item["Id"]);
                    produto.CodigoProtheus = int.Parse(item["CodigoProtheus"].ToString());
                    produto.Descricao = item["Descricao"].ToString();
                    produto.GrupoId = Convert.ToInt64(item["gruposID"]);
                    produto.Grupo = new Grupo();
                    produto.Grupo.Id = Convert.ToInt64(item["idGrupos"]);
                    produto.Grupo.CodigoProtheus = Convert.ToInt64(item["protheusGrupo"]);
                    produto.Grupo.Descricao = item["descricaoGrupo"].ToString();
                }
                return produto;
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
        public Produto FindByProtheus(long codigoProtheus)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select produtos.id, produtos.CodigoProtheus, produtos.Descricao, produtos.gruposId," +
                    $" grupos.id as idGrupos, grupos.CodigoProtheus as protheusGrupo, grupos.Descricao as descricaoGrupo" +
                    $" from Produtos inner join grupos on produtos.gruposId = grupos.id where produtos.CodigoProtheus = {codigoProtheus}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Produto produto = null;
                foreach (DataRow item in dt.Rows)
                {
                    produto = new Produto();
                    produto.Id = Convert.ToInt64(item["Id"]);
                    produto.CodigoProtheus = int.Parse(item["CodigoProtheus"].ToString());
                    produto.Descricao = item["Descricao"].ToString();
                    produto.GrupoId = Convert.ToInt64(item["gruposID"]);
                    produto.Grupo = new Grupo();
                    produto.Grupo.Id = Convert.ToInt64(item["idGrupos"]);
                    produto.Grupo.CodigoProtheus = Convert.ToInt64(item["protheusGrupo"]);
                    produto.Grupo.Descricao = item["descricaoGrupo"].ToString();
                }
                return produto;
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

        public IEnumerable<Produto> GetAll()
        {
            try
            {
                List<Produto> produtos = new List<Produto>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Select produtos.id, produtos.CodigoProtheus, produtos.Descricao, produtos.gruposId," +
                    $" grupos.id as idGrupos, grupos.CodigoProtheus as protheusGrupo, grupos.Descricao as descricaoGrupo" +
                    $" from Produtos inner join grupos on produtos.gruposId = grupos.id", conn);
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
                    produto.Grupo = new Grupo();
                    produto.Grupo.Id = Convert.ToInt64(item["idGrupos"]);
                    produto.Grupo.CodigoProtheus = Convert.ToInt64(item["protheusGrupo"]);
                    produto.Grupo.Descricao = item["descricaoGrupo"].ToString();
                    produtos.Add(produto);
                }
                return produtos;
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
                cmd = new MySqlCommand($"Delete from Produtos where id = {id}", conn);
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

        public bool Update(Produto produto, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update Produtos set CodigoProtheus = {produto.CodigoProtheus}, descricao = '{produto.Descricao}', GruposId = {produto.GrupoId} where id = {id}", conn);
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
