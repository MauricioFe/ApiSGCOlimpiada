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

        public Acompanhamento FindBySolicitacaoId(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"SELECT ac.id as acId, ac.data as dataAcompanhamento, ac.observacao, ac.statusId, ac.UsuariosId, ac.solicitacaoComprasId," +
                    $" sc.id as scId, sc.Data, sc.ResponsavelEntrega, sc.Justificativa, sc.Anexo, sc.TipoComprasId, sc.EscolasID, " +
                    $"u.nome as usuario, u.id as uId, u.email, u.funcaoId, st.descricao as status, st.id as statusID FROM " +
                    $"sgc_olimpiada.solicitacaocompras as sc inner join acompanhamento as ac on ac.SolicitacaoComprasId = sc.id " +
                    $"inner join status as st on st.id = ac.StatusId inner join usuarios as u on u.id = ac.UsuariosId where sc.id = {id}", conn);
                conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Acompanhamento acompanhamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    acompanhamento = new Acompanhamento();
                    acompanhamento.Id = Convert.ToInt64(item["acId"]);
                    acompanhamento.Date = Convert.ToDateTime(item["dataAcompanhamento"]);
                    acompanhamento.Observacao = item["observacao"].ToString();
                    acompanhamento.UsuarioId = Convert.ToInt64(item["usuariosId"]);
                    acompanhamento.SolicitacaoCompraId = Convert.ToInt64(item["solicitacaoComprasId"]);
                    acompanhamento.StatusId = Convert.ToInt64(item["statusId"]);
                    acompanhamento.SolicitacaoCompra = new SolicitacaoCompra();
                    acompanhamento.SolicitacaoCompra.Id = Convert.ToInt64(item["scId"]);
                    acompanhamento.SolicitacaoCompra.Data = Convert.ToDateTime(item["data"]);
                    acompanhamento.SolicitacaoCompra.ResponsavelEntrega = item["ResponsavelEntrega"].ToString();
                    acompanhamento.SolicitacaoCompra.Justificativa = item["Justificativa"].ToString();
                    acompanhamento.SolicitacaoCompra.TipoCompraId = Convert.ToInt64(item["TipoComprasId"]);
                    acompanhamento.SolicitacaoCompra.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    acompanhamento.Usuario = new Usuario();
                    acompanhamento.Usuario.Id = Convert.ToInt64(item["uId"]);
                    acompanhamento.Usuario.Nome = item["usuario"].ToString();
                    acompanhamento.Usuario.Email = item["email"].ToString();
                    acompanhamento.Usuario.FuncaoId = Convert.ToInt32(item["funcaoId"]);
                    acompanhamento.Status = new Status();
                    acompanhamento.Status.Id = Convert.ToInt64(item["statusID"]);
                    acompanhamento.Status.Descricao = item["status"].ToString();
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
                    $" data = '{acompanhamento.Date.ToString("yyyy-MM-dd HH:mm")}',statusId = {acompanhamento.StatusId}, usuariosId = {acompanhamento.UsuarioId}," +
                    $" solicitacaoComprasId = {acompanhamento.SolicitacaoCompraId}  where id = {id}", conn);
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
        public IEnumerable<Acompanhamento> GetSolicitacaoAcompanhamento()
        {
            try
            {
                List<Acompanhamento> acompanhamentos = new List<Acompanhamento>();
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"SELECT ac.id as acId, ac.data as dataAcompanhamento, ac.observacao, ac.statusId, ac.UsuariosId, ac.solicitacaoComprasId," +
                    $" sc.id as scId, sc.Data, sc.ResponsavelEntrega, sc.Justificativa, sc.Anexo, sc.TipoComprasId, sc.EscolasID, " +
                    $"u.nome as usuario, u.id as uId, u.email, u.funcaoId, st.descricao as status, st.id as statusID FROM " +
                    $"sgc_olimpiada.solicitacaocompras as sc inner join acompanhamento as ac on ac.SolicitacaoComprasId = sc.id " +
                    $"inner join status as st on st.id = ac.StatusId inner join usuarios as u on u.id = ac.UsuariosId; ", conn); conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Acompanhamento acompanhamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    acompanhamento = new Acompanhamento();
                    acompanhamento.Id = Convert.ToInt64(item["acId"]);
                    acompanhamento.Date = Convert.ToDateTime(item["dataAcompanhamento"]);
                    acompanhamento.Observacao = item["observacao"].ToString();
                    acompanhamento.UsuarioId = Convert.ToInt64(item["usuariosId"]);
                    acompanhamento.SolicitacaoCompraId = Convert.ToInt64(item["solicitacaoComprasId"]);
                    acompanhamento.StatusId = Convert.ToInt64(item["statusId"]);
                    acompanhamento.SolicitacaoCompra = new SolicitacaoCompra();
                    acompanhamento.SolicitacaoCompra.Id = Convert.ToInt64(item["scId"]);
                    acompanhamento.SolicitacaoCompra.Data = Convert.ToDateTime(item["data"]);
                    acompanhamento.SolicitacaoCompra.ResponsavelEntrega = item["ResponsavelEntrega"].ToString();
                    acompanhamento.SolicitacaoCompra.Justificativa = item["Justificativa"].ToString();
                    acompanhamento.SolicitacaoCompra.TipoCompraId = Convert.ToInt64(item["TipoComprasId"]);
                    acompanhamento.SolicitacaoCompra.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    acompanhamento.Usuario = new Usuario();
                    acompanhamento.Usuario.Id = Convert.ToInt64(item["uId"]);
                    acompanhamento.Usuario.Nome = item["usuario"].ToString();
                    acompanhamento.Usuario.Email = item["email"].ToString();
                    acompanhamento.Usuario.FuncaoId = Convert.ToInt32(item["funcaoId"]);
                    acompanhamento.Status = new Status();
                    acompanhamento.Status.Id = Convert.ToInt64(item["statusID"]);
                    acompanhamento.Status.Descricao = item["status"].ToString();
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
        public IEnumerable<Acompanhamento> GetSolicitacaoAcompanhamentoPendente()
        {
            try
            {
                List<Acompanhamento> acompanhamentos = new List<Acompanhamento>();
                conn = new MySqlConnection(_conn);
                cmd = new MySqlCommand($"SELECT ac.id as acId, ac.data as dataAcompanhamento, ac.observacao, ac.statusId, ac.UsuariosId, ac.solicitacaoComprasId," +
                    $" sc.id as scId, sc.Data, sc.ResponsavelEntrega, sc.Justificativa, sc.Anexo, sc.TipoComprasId, sc.EscolasID, " +
                    $"u.nome as usuario, u.id as uId, u.email, u.funcaoId, st.descricao as status, st.id as statusID FROM " +
                    $"sgc_olimpiada.solicitacaocompras as sc inner join acompanhamento as ac on ac.SolicitacaoComprasId = sc.id " +
                    $"inner join status as st on st.id = ac.StatusId inner join usuarios as u on u.id = ac.UsuariosId Where st.id = 1 or" +
                    $" st.id = 4 or st.id = 5 or st.id = 7 or st.id = 6; ", conn); conn.Open();
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Acompanhamento acompanhamento = null;
                foreach (DataRow item in dt.Rows)
                {
                    acompanhamento = new Acompanhamento();
                    acompanhamento.Id = Convert.ToInt64(item["acId"]);
                    acompanhamento.Date = Convert.ToDateTime(item["dataAcompanhamento"]);
                    acompanhamento.Observacao = item["observacao"].ToString();
                    acompanhamento.UsuarioId = Convert.ToInt64(item["usuariosId"]);
                    acompanhamento.SolicitacaoCompraId = Convert.ToInt64(item["solicitacaoComprasId"]);
                    acompanhamento.StatusId = Convert.ToInt64(item["statusId"]);
                    acompanhamento.SolicitacaoCompra = new SolicitacaoCompra();
                    acompanhamento.SolicitacaoCompra.Id = Convert.ToInt64(item["scId"]);
                    acompanhamento.SolicitacaoCompra.Data = Convert.ToDateTime(item["data"]);
                    acompanhamento.SolicitacaoCompra.ResponsavelEntrega = item["ResponsavelEntrega"].ToString();
                    acompanhamento.SolicitacaoCompra.Justificativa = item["Justificativa"].ToString();
                    acompanhamento.SolicitacaoCompra.TipoCompraId = Convert.ToInt64(item["TipoComprasId"]);
                    acompanhamento.SolicitacaoCompra.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    acompanhamento.Usuario = new Usuario();
                    acompanhamento.Usuario.Id = Convert.ToInt64(item["uId"]);
                    acompanhamento.Usuario.Nome = item["usuario"].ToString();
                    acompanhamento.Usuario.Email = item["email"].ToString();
                    acompanhamento.Usuario.FuncaoId = Convert.ToInt32(item["funcaoId"]);
                    acompanhamento.Status = new Status();
                    acompanhamento.Status.Id = Convert.ToInt64(item["statusID"]);
                    acompanhamento.Status.Descricao = item["status"].ToString();
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

    }
}
