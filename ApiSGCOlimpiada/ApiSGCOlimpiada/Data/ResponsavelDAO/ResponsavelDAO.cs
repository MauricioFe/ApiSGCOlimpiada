﻿using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.ResponsavelDAO
{
    public class ResponsavelDAO : IResponsavelDAO
    {
        private readonly string _conn;
        public ResponsavelDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        DataTable dt;

        public bool Add(Responsavel responsavel)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Insert into Responsaveis values(null, '{responsavel.Nome}', '{responsavel.Email}', '{responsavel.Cargo}', {responsavel.EscolaId})", conn);
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

        public Responsavel Find(long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"select e.*, r.Id as ResponsavelId, r.Nome as responsavelNome, r.email ,r.Cargo, r.EscolasId From Escolas e Inner join Responsaveis r on r.EscolasId = e.id where r.Id = {id}", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Responsavel responsavel = null;
                foreach (DataRow item in dt.Rows)
                {
                    responsavel = new Responsavel();
                    responsavel.Id = Convert.ToInt64(item["ResponsavelId"]);
                    responsavel.Nome = item["responsavelNome"].ToString();
                    responsavel.Email = item["email"].ToString();
                    responsavel.Cargo = item["Cargo"].ToString();
                    responsavel.Escola = new Escola();
                    responsavel.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    responsavel.Escola.Id = Convert.ToInt64(item["Id"]);
                    responsavel.Escola.Nome = item["Nome"].ToString();
                    responsavel.Escola.Cep = item["Cep"].ToString();
                    responsavel.Escola.Logradouro = item["Logradouro"].ToString();
                    responsavel.Escola.Bairro = item["Bairro"].ToString();
                    responsavel.Escola.Numero = item["Numero"].ToString();
                    responsavel.Escola.Cidade = item["Cidade"].ToString();
                    responsavel.Escola.Estado = item["Estado"].ToString();
                }
                return responsavel;
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
        public List<Responsavel> FindBySearch(string search)
        {
            try
            {
                List<Responsavel> responsaveis = new List<Responsavel>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"select e.*, r.Id as ResponsavelId, r.Nome as responsavelNome, r.email ,r.Cargo, r.EscolasId From Escolas e Inner join Responsaveis r on r.EscolasId = e.id where r.Nome LIKE '%{search}%' or e.Nome LIKE '%{search}%'", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                Responsavel responsavel = null;
                foreach (DataRow item in dt.Rows)
                {
                    responsavel = new Responsavel();
                    responsavel.Id = Convert.ToInt64(item["ResponsavelId"]);
                    responsavel.Nome = item["responsavelNome"].ToString();
                    responsavel.Cargo = item["Cargo"].ToString();
                    responsavel.Email = item["email"].ToString();
                    responsavel.Escola = new Escola();
                    responsavel.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    responsavel.Escola.Id = Convert.ToInt64(item["Id"]);
                    responsavel.Escola.Nome = item["Nome"].ToString();
                    responsavel.Escola.Cep = item["Cep"].ToString();
                    responsavel.Escola.Logradouro = item["Logradouro"].ToString();
                    responsavel.Escola.Bairro = item["Bairro"].ToString();
                    responsavel.Escola.Numero = item["Numero"].ToString();
                    responsavel.Escola.Cidade = item["Cidade"].ToString();
                    responsavel.Escola.Estado = item["Estado"].ToString();
                    responsaveis.Add(responsavel);
                }
                return responsaveis;
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

        public IEnumerable<Responsavel> GetAll()
        {
            try
            {
                List<Responsavel> responsaveis = new List<Responsavel>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"select e.*, r.Id as ResponsavelId, r.Nome as responsavelNome, r.email , r.Cargo, r.EscolasId From Escolas e Inner join Responsaveis r on r.EscolasId = e.id", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Responsavel responsavel = new Responsavel();
                    responsavel.Id = Convert.ToInt64(item["ResponsavelId"]);
                    responsavel.Nome = item["responsavelNome"].ToString();
                    responsavel.Email = item["Email"].ToString();
                    responsavel.Cargo = item["Cargo"].ToString();
                    responsavel.Escola = new Escola();
                    responsavel.EscolaId = Convert.ToInt64(item["EscolasId"]);
                    responsavel.Escola.Id = Convert.ToInt64(item["Id"]);
                    responsavel.Escola.Nome = item["Nome"].ToString();
                    responsavel.Escola.Cep = item["Cep"].ToString();
                    responsavel.Escola.Logradouro = item["Logradouro"].ToString();
                    responsavel.Escola.Bairro = item["Bairro"].ToString();
                    responsavel.Escola.Numero = item["Numero"].ToString();
                    responsavel.Escola.Cidade = item["Cidade"].ToString();
                    responsavel.Escola.Estado = item["Estado"].ToString();
                    responsaveis.Add(responsavel);
                }
                return responsaveis;
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
        public IEnumerable<Responsavel> GetBySolicitacao(long idSoliciatacao)
        {
            try
            {
                List<Responsavel> responsaveis = new List<Responsavel>();
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"select r.Id, r.nome, r.email, r.cargo, r.escolasId from responsaveis as r inner join  escolas as e on r.EscolasId = e.id inner join solicitacaoCompras as sc on sc.escolasid = e.id where sc.id = {idSoliciatacao}; ", conn);
                adapter = new MySqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Responsavel responsavel = new Responsavel();
                    responsavel.Id = Convert.ToInt64(item["id"]);
                    responsavel.Nome = item["nome"].ToString();
                    responsavel.Email = item["Email"].ToString();
                    responsavel.Cargo = item["Cargo"].ToString();
                    responsavel.EscolaId = Convert.ToInt64(item["escolasId"]);
                    responsaveis.Add(responsavel);
                }
                return responsaveis;
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
                cmd = new MySqlCommand($"Delete from Responsaveis where id = {id}", conn);
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

        public bool Update(Responsavel responsavel, long id)
        {
            try
            {
                conn = new MySqlConnection(_conn);
                conn.Open();
                cmd = new MySqlCommand($"Update Responsaveis set Nome = '{responsavel.Nome}', Email = '{responsavel.Email}', Cargo = '{responsavel.Cargo}', " +
                    $"EscolasId = {responsavel.EscolaId} where id = {id}", conn);
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
