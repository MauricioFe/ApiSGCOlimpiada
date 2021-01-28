using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.EmailDAO
{
    public class EmailDAO : IEmailDAO
    {
        private string _conn;

        public EmailDAO(IConfiguration config)
        {
            this._conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        public EmailModel GetDadosSolicitacao(long idSolicitacao)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"SELECT ac.id AS acId, ac.data AS dataAcompanhamento, ac.observacao, ac.statusId, ac.UsuariosId,ac.solicitacaoComprasId, sc.id AS scId, sc.Data, sc.ResponsavelEntrega, sc.Justificativa, sc.Anexo, sc.TipoComprasId, sc.EscolasID, e.id AS idEscolas, e.Nome AS escola, e.Cep, e.logradouro, e.bairro, e.numero, e.estado, e.cidade, u.nome AS usuario, u.id AS uId, u.email,u.funcaoId,  st.descricao AS status, st.id AS statusID FROM sgc_olimpiada.solicitacaocompras AS sc INNER JOIN Acompanhamento AS ac ON ac.SolicitacaoComprasId = sc.id INNER JOIN escolas AS e ON sc.escolasId = e.id INNER JOIN status AS st ON st.id = ac.StatusId INNER JOIN usuarios AS u ON u.id = ac.UsuariosId where sc.id = {idSolicitacao}; ", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                EmailModel data = null;
                foreach (DataRow item in dt.Rows)
                {
                    data = new EmailModel();
                    data.Acompanhamento = new Acompanhamento();
                    data.Acompanhamento.Id = Convert.ToInt64(item["acId"]);
                    data.Acompanhamento.Date = Convert.ToDateTime(item["dataAcompanhamento"]);
                    data.Acompanhamento.Observacao = item["observacao"].ToString();
                    data.Acompanhamento.UsuarioId = Convert.ToInt64(item["usuariosId"]);
                    data.Acompanhamento.SolicitacaoCompraId = Convert.ToInt64(item["solicitacaoComprasId"]);
                    data.Acompanhamento.StatusId = Convert.ToInt64(item["statusId"]);
                    data.Acompanhamento.SolicitacaoCompra = new SolicitacaoCompra();
                    data.Acompanhamento.SolicitacaoCompra.Id = Convert.ToInt64(item["scId"]);
                    data.Acompanhamento.SolicitacaoCompra.Data = Convert.ToDateTime(item["data"]);
                    data.Acompanhamento.SolicitacaoCompra.ResponsavelEntrega = item["ResponsavelEntrega"].ToString();
                    data.Acompanhamento.SolicitacaoCompra.Justificativa = item["Justificativa"].ToString();
                    data.Acompanhamento.SolicitacaoCompra.TipoCompraId = Convert.ToInt64(item["TipoComprasId"]);
                    data.Acompanhamento.SolicitacaoCompra.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    data.Acompanhamento.SolicitacaoCompra.Escola = new Escola();
                    data.Acompanhamento.SolicitacaoCompra.Escola.Id = Convert.ToInt64(item["idEscolas"]);
                    data.Acompanhamento.SolicitacaoCompra.Escola.Nome = item["escola"].ToString();
                    data.Acompanhamento.SolicitacaoCompra.Escola.Cep = item["cep"].ToString();
                    data.Acompanhamento.SolicitacaoCompra.Escola.Logradouro = item["logradouro"].ToString();
                    data.Acompanhamento.SolicitacaoCompra.Escola.Bairro = item["Bairro"].ToString();
                    data.Acompanhamento.SolicitacaoCompra.Escola.Numero = item["Numero"].ToString();
                    data.Acompanhamento.SolicitacaoCompra.Escola.Estado = item["Estado"].ToString();
                    data.Acompanhamento.SolicitacaoCompra.Escola.Cidade = item["Cidade"].ToString();
                    data.Acompanhamento.Usuario = new Usuario();
                    data.Acompanhamento.Usuario.Id = Convert.ToInt64(item["uId"]);
                    data.Acompanhamento.Usuario.Nome = item["usuario"].ToString();
                    data.Acompanhamento.Usuario.Email = item["email"].ToString();
                    data.Acompanhamento.Usuario.FuncaoId = Convert.ToInt32(item["funcaoId"]);
                    data.Acompanhamento.Status = new Status();
                    data.Acompanhamento.Status.Id = Convert.ToInt64(item["statusID"]);
                    data.Acompanhamento.Status.Descricao = item["status"].ToString();
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
