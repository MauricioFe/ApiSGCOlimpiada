﻿using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.SolicitacaoCompraDAO
{
    public class SolicitacaoCompraDAO : ISolicitacaoCompraDAO
    {
        private readonly string _conn;

        public SolicitacaoCompraDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        public bool Add(SolicitacaoCompra solicitacaoCompra)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"insert into SolicitacaoCompras values ('{solicitacaoCompra.ResponsavelEntrega}', " +
                    $"'{solicitacaoCompra.Data}', '{solicitacaoCompra.Justificativa}', {solicitacaoCompra.TipoCompraId}, {solicitacaoCompra.EscolaId})", conn);
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

        public SolicitacaoCompra Find(int id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Select * from where id = {id}", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                SolicitacaoCompra solicitacaoCompra = null;
                foreach (DataRow item in dt.Rows)
                {
                    solicitacaoCompra.Id = Convert.ToInt32(item["id"]);
                    solicitacaoCompra.ResponsavelEntrega = item["responsavelEntrega"].ToString();
                    solicitacaoCompra.Data = Convert.ToDateTime(item["Data"]);
                    solicitacaoCompra.Justificativa = item["Justificativa"].ToString();
                    solicitacaoCompra.TipoCompraId = Convert.ToInt32(item["tipoCompraId"]);
                    solicitacaoCompra.EscolaId = Convert.ToInt32(item["EscolasId"]);
                }
                return solicitacaoCompra;
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

        public IEnumerable<SolicitacaoCompra> GetAll()
        {
            try
            {
                List<SolicitacaoCompra> solicitacaoCompras = new List<SolicitacaoCompra>();
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Select * from", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                SolicitacaoCompra solicitacaoCompra = null;
                foreach (DataRow item in dt.Rows)
                {
                    solicitacaoCompra.Id = Convert.ToInt32(item["id"]);
                    solicitacaoCompra.ResponsavelEntrega = item["responsavelEntrega"].ToString();
                    solicitacaoCompra.Data = Convert.ToDateTime(item["Data"]);
                    solicitacaoCompra.Justificativa = item["Justificativa"].ToString();
                    solicitacaoCompra.TipoCompraId = Convert.ToInt32(item["tipoCompraId"]);
                    solicitacaoCompra.EscolaId = Convert.ToInt32(item["EscolasId"]);
                    solicitacaoCompras.Add(solicitacaoCompra);
                }
                return solicitacaoCompras;
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

        public bool Update(SolicitacaoCompra solicitacaoCompra)
        {

            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Update SolicitacaoCompras set responsavelEntrega = '{solicitacaoCompra.ResponsavelEntrega}', " +
                    $"data = '{solicitacaoCompra.Data}', justificativa = '{solicitacaoCompra.Justificativa}', tipoCompraId = {solicitacaoCompra.TipoCompraId}, escolasId = {solicitacaoCompra.EscolaId})", conn);
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
