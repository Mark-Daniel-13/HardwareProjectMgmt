using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Data.Users.DataAccess
{
    public class User:GeneralDataAccess
    {
        string connString = ConfigurationManager.ConnectionStrings["Resto_POS"]?.ConnectionString;
        //force get is to either retrieve soft deleted data or not
        public async Task<List<Models.User>> GetAll(bool forceGet = false) 
        {
            string sqlSyntax = (forceGet) ? "Select * from users Order By UserId desc" : "Select * from users Where DeletedAt IS NULL Order By UserId desc";
            return await LoadData<Models.User,dynamic>(sqlSyntax,new { }, connString);
        }

        public async Task<Models.User> GetById(int userId, bool forceGet = false)
        {
            var p = new { UserId = userId };
            string sqlSyntax = (forceGet) ? @"Select * from users Where UserId=@UserId" : @"Select * from users Where UserId=@UserId And DeletedAt IS NULL";
            var result = await LoadData<Models.User, dynamic>(sqlSyntax, p, connString);
            return result?.FirstOrDefault();
        }
        public async Task<Models.User> GetByUsername(string userName)
        {
            var p = new { Username = userName };
            string sqlSyntax = @"Select * from users Where Username=@Username And DeletedAt IS NULL";
            var result = await LoadData<Models.User, dynamic>(sqlSyntax, p, connString);
            return result?.FirstOrDefault();
        }
        public async Task<bool> isUsernameExist(string userName,int? userId)
        {
            if (userId != null)
            {
                var p = new { Username = userName, UserId = Convert.ToInt32(userId) };
                string sqlSyntax = @"Select * from users Where Username=@Username AND UserId!=@UserId And DeletedAt IS NULL";
                var result = await LoadData<Models.User, dynamic>(sqlSyntax, p, connString);
                return (result.Count() > 0) ? true : false;
            }
            else {
                var p = new { Username = userName };
                string sqlSyntax = @"Select * from users Where Username=@Username And DeletedAt IS NULL";
                var result = await LoadData<Models.User, dynamic>(sqlSyntax, p, connString);
                return (result.Count() > 0) ? true : false;
            }
        }

        public async Task<Models.User> GetByIdWithRoles(int userId)
        {
            try
            {
                var p = new { UserId = userId};
                string sqlSyntax = @"Select u.*,r.* from users u 
                                        inner join userroles ur on u.UserId = ur.UserId 
                                        inner join roles r on ur.RoleId = r.RoleId
                                        Where u.UserId=@UserId And u.DeletedAt IS NULL AND ur.deletedAt IS NULL Order By u.UserId desc";
                using (IDbConnection conn = new MySqlConnection(connString))
                {
                    var Dictionary = new Dictionary<int, Models.User>();
                    var datas = await conn.QueryAsync<Models.User,Models.Role,Models.User>
                                (sqlSyntax,(user,role)=> {
                                    Models.User userEntry;

                                    if (!Dictionary.TryGetValue(user.UserId, out userEntry)) {
                                        userEntry = user;
                                        userEntry.UserRole = new List<Models.Role>();
                                        Dictionary.Add(userEntry.UserId, userEntry);
                                    }
                                    userEntry.UserRole.Add(role);
                                    return userEntry; },p,null,true,splitOn:"RoleId");
                    return datas?.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                var debug = e.Message;
                return null;
            }
        }
        public async Task<List<Models.User>> GetAllWithRoles()
        {
            try
            {
                var p = new { };
                string sqlSyntax = @"Select u.*,r.* from users u 
                                        inner join userroles ur on u.UserId = ur.UserId 
                                        inner join roles r on ur.RoleId = r.RoleId
                                        Where u.DeletedAt IS NULL AND ur.deletedAt IS NULL Order By u.UserId desc";
                using (IDbConnection conn = new MySqlConnection(connString))
                {
                    var Dictionary = new Dictionary<int, Models.User>();
                    var datas = await conn.QueryAsync<Models.User, Models.Role, Models.User>
                                (sqlSyntax, (user, role) => {
                                    Models.User userEntry;

                                    if (!Dictionary.TryGetValue(user.UserId, out userEntry))
                                    {
                                        userEntry = user;
                                        userEntry.UserRole = new List<Models.Role>();
                                        Dictionary.Add(userEntry.UserId, userEntry);
                                    }
                                    userEntry.UserRole.Add(role);
                                    return userEntry;
                                }, p, null, true, splitOn: "RoleId");
                    return datas.ToList();
                }
            }
            catch (Exception e)
            {
                var debug = e.Message;
                return null;
            }
        }
        public async Task<bool> UpdateUser(Models.User user,int newRoleId)
        {
            var p = user;
            string sqlSyntax;
            sqlSyntax =(user.Password != null) ? @"Update users Set FirstName=@FirstName,LastName=@LastName,Username=@Username,Email=@Email,Password=@Password,updatedAt=@UpdatedAt Where UserId=@UserId":
                                                @"Update users Set FirstName=@FirstName,LastName=@LastName,Username=@Username,Email=@Email,updatedAt=@UpdatedAt Where UserId=@UserId";
            using (IDbConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {   //update userInfo
                    await conn.ExecuteAsync(sqlSyntax, p,transaction:trans);
                    try {
                        //get the current userRole
                        string sqlRoleSyntax = @"Select * from userroles Where UserId=@UserId And DeletedAt IS NULL";
                        var result = await conn.QueryAsync<Models.UserRole>(sqlRoleSyntax, p, transaction: trans);
                        //if currentUserRole is diff from newUserRole update userRole
                        if (result?.FirstOrDefault().RoleId != newRoleId) {
                            //if new role delete the old and create a new record
                            //removing data
                            var p2 = new { deletedAt = DateTime.Now, UserRoleId = result.FirstOrDefault().UserRoleId };
                            string removeOldSyntax = @"Update userroles Set deletedAt=@deletedAt Where UserRoleId=@UserRoleId";
                            var removeQuery = conn.ExecuteAsync(removeOldSyntax, p2, transaction:trans);

                            //adding new data
                            var p3 = new { UserId = user.UserId, RoleId = newRoleId, createdAt = DateTime.Now, updatedAt = DateTime.Now };
                            string addnewRoleSyntax = @"Insert Into userroles(UserId,RoleId,createdAt,updatedAt) VALUES(@UserId,@RoleId,@createdAt,@updatedAt)";
                            var addQuery = conn.ExecuteAsync(addnewRoleSyntax, p3,transaction:trans);
                        }
                        trans.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }
        public async Task<bool> AddUser(Models.User user,int userRole)
        {
            var p = user;
            string sqlSyntax = @"Insert Into users(FirstName,LastName,Username,Email,Password,createdAt,updatedAt) 
                                VALUES(@FirstName,@LastName,@Username,@Email,@Password,@createdAt,@updatedAt); 
                                Select LAST_INSERT_ID()";
            using (IDbConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try { 
                        var newData = await conn.QueryAsync<int>(sqlSyntax, p,transaction:trans);
                        int newlyInsertedUserId = newData.Single();
                        string sqlRoleSyntax = @"Insert Into userroles(UserId,RoleId,createdAt,updatedAt) Values(@UserId,@RoleId,@createdAt,@updatedAt)";
                        var p2 = new { UserId = newlyInsertedUserId, RoleId = userRole, createdAt = user.CreatedAt, updatedAt = user.UpdatedAt };
                        await conn.ExecuteAsync(sqlRoleSyntax, p2, transaction: trans);
                        
                        trans.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        var debug = e.Message;
                        trans.Rollback();
                        return false;
                    }
                }
            }
            
        }
        public async Task<bool> RemoveUser(Models.User user)
        {
            var p = user;
            string sqlSyntax = @"Update users Set deletedAt=@DeletedAt Where UserId=@UserId";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
    }
}
