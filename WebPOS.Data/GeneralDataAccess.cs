using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace WebPOS.Data
{
    public class GeneralDataAccess : IGeneralDataAccess
    {
        public async Task<List<Model>> LoadData<Model, O>(string sqlSyntax, O parameters, string connectionString)
        {
            try
            {
                using (IDbConnection conn = new MySqlConnection(connectionString))
                {
                    var datas = await conn.QueryAsync<Model>(sqlSyntax, parameters);
                    return datas.ToList();
                }
            }
            catch (Exception e)
            {
                var debug = e.Message;
                return null;
            }
        }

        public Task<int> ModifyData<O>(string sqlSyntax, O parameters, string connectionString)
        {
            try
            {
                using (IDbConnection conn = new MySqlConnection(connectionString))
                {
                    return conn.ExecuteAsync(sqlSyntax, parameters);
                }
            }
            catch (Exception e)
            {
                var debug = e.Message;
                return null;
            }
        }
    }
}
