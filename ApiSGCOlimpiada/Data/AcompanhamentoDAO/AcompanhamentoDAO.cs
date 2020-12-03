using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.AcompanhamentoDAO
{
    public class AcompanhamentoDAO : IAcompanhamentoDAO
    {
        private readonly string _conn;

        public AcompanhamentoDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        public bool Add(Acompanhamento acompanhamento)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"insert into acompanhamento values ({acompanhamento.Id}, '{acompanhamento.Observacao}', " +
                    $"'{acompanhamento.Date.ToString("yyyy-MM-dd HH:mm")}', {acompanhamento.StatusId}, {acompanhamento.UsuarioId}," +
                    $" {acompanhamento.SolicitacaoCompraId})", conn);
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

        public Acompanhamento Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Select * from acompanhamento where id = {id}", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Acompanhamento acompanhamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    acompanhamento = new Acompanhamento();
                    acompanhamento = new Acompanhamento();
                    acompanhamento.Id = Convert.ToInt64(item["Id"]);
                }
                return acompanhamento;
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

        public IEnumerable<Acompanhamento> GetAll()
        {
            try
            {
                List<Acompanhamento> acompanhamentos = new List<Acompanhamento>();
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Select * from acompanhamento", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Acompanhamento acompanhamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    acompanhamento = new Acompanhamento();
                    acompanhamento.Id = Convert.ToInt64(item["Id"]);
                    acompanhamentos.Add(acompanhamento);
                }
                return acompanhamentos;
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

        public bool Update(Acompanhamento acompanhamento, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"Update acompanhamento set observacao = '{acompanhamento.Observacao}', " +
                    $" data = '{acompanhamento.Date.ToString("yyyy-MM-dd HH:mm")}',statusId = {acompanhamento.StatusId}, usuarioId = {acompanhamento.UsuarioId}," +
                    $" solicitacaoCompraId = {acompanhamento.SolicitacaoCompraId}  where id = {id}", conn);
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
