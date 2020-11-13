using ApiSGCOlimpiada.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGCOlimpiada.Data.UsuarioDAO
{
    public class FuncaoDAO : IFuncaoDAO
    {
        private readonly string _conn;
        public FuncaoDAO(IConfiguration config)
        {
            _conn = config.GetConnectionString("conn");
        }
        MySqlConnection conn;
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        DataTable dt;

        public IEnumerable<Funcao> GetAll()
        {
            throw new NotImplementedException();
        }

        public Funcao Find(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Funcao funcao)
        {
            throw new NotImplementedException();
        }

        public void Update(Funcao funcao, int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
