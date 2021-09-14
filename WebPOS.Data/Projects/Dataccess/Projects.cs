using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Data.Projects.Dataccess
{
    public class Projects:GeneralDataAccess
    {
        string connString = ConfigurationManager.ConnectionStrings["App_DB"]?.ConnectionString;
        public async Task<List<Models.Projects>> GetAll(bool forceGet = false)
        {
            string sqlSyntax = (forceGet) ? "Select * from projects" : "Select * from projects Where DeletedAt IS NULL Order By ProjectId desc";
            return await LoadData<Models.Projects, dynamic>(sqlSyntax, new { }, connString);
        }
        public async Task<Models.Projects> GetById(int projectId, bool forceGet = false)
        {
            var p = new { ProjectId = projectId };
            string sqlSyntax = (forceGet) ? @"Select * from projects Where ProjectId=@ProjectId" : @"Select * from projects Where ProjectId=@ProjectId And DeletedAt IS NULL";
            var result = await LoadData<Models.Projects, dynamic>(sqlSyntax, p, connString);
            return result?.FirstOrDefault();
        }
        public async Task<bool> Update(Models.Projects project)
        {
            var p = project;
            string sqlSyntax = @"Update projects Set Name=@Name,Address=@Address,updatedAt=@UpdatedAt Where ProjectId=@ProjectId";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
        public async Task<bool> Add(Models.Projects project)
        {
            var p = project;
            string sqlSyntax = @"Insert Into projects(Name,Address,createdAt,updatedAt)
                                    VALUES(@Name,@Address,@CreatedAt,@UpdatedAt)";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
        public async Task<bool> Remove(Models.Projects project)
        {
            var p = project;
            string sqlSyntax = @"Update projects Set deletedAt=@DeletedAt Where ProjectId=@ProjectId";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
    }
}
