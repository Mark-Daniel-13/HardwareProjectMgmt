using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace WebPOS.Data
{
    public interface IGeneralDataAccess
    {
        Task<List<Model>> LoadData<Model, O>(string sqlSyntax, O parameters, string connectionString);
        Task<int> ModifyData<O>(string sqlSyntax, O parameters, string connectionString);
    }
}