using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Users.Facades
{
    public class User:IDisposable
    {
        private Data.Users.DataAccess.User dataAccess = new();
        public async Task<List<Models.User>> GetAll(bool forceGet = false) {
            //force get is to either retrieve soft deleted data or not
            return Models.User.ToModelList(await dataAccess.GetAll());
        }
        public async Task<List<Models.User>> GetAllWithRoles()
        {
            return Models.User.ToModelList(await dataAccess.GetAllWithRoles());
        }
        public async Task<Models.User> GetById(int userId)
        {
            return Models.User.ToModel(await dataAccess.GetById(userId));
        }
        public async Task<Models.User> GetByIdWithRoles(int userId) {
            return Models.User.ToModel(await dataAccess.GetByIdWithRoles(userId));
        }
        public async Task<Models.Token> ValidateUser(string username, string password) {
            var user = await dataAccess.GetByUsername(username);
            if (user is null) return null;
            bool checkPass = Helpers.Encryption.CheckMD5(password, user.Password);
            if (!checkPass) return null;
            var token = await Helpers.JwtToken.GenerateToken(user.UserId);

            return token;
        }
        public async Task<Helpers.ResultHandler> UpdateUser(Models.User user)
        {
            if (!isDataValid(user,true)) return new Helpers.ResultHandler().HandleResult(false, "There's an invalid input on your form.");
            if(await dataAccess.isUsernameExist(user.UserName,user.UserId)) return new Helpers.ResultHandler().HandleResult(false, "Username already exist.");
            user.UpdatedAt = DateTime.Now;
            try
            {
                var dataModel = Models.User.ToDataModel(user);
                var query = await dataAccess.UpdateUser(dataModel,user.UserRoleId);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Update, "user");
            }
            catch(Exception e)
            {
                return new Helpers.ResultHandler().HandleResult(false, e.Message);
            }
        }
        public async Task<Helpers.ResultHandler> AddUser(Models.User user)
        {
            if (!isDataValid(user)) return new Helpers.ResultHandler().HandleResult(false, "There's an invalid input on your form.");
            if(await dataAccess.isUsernameExist(user.UserName,null)) return new Helpers.ResultHandler().HandleResult(false, "Username already exist.");
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            try
            {
                var dataMOdel = Models.User.ToDataModel(user);
                var query = await dataAccess.AddUser(dataMOdel, user.UserRoleId);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Create, "user");                
            }
            catch(Exception e)
            {
                return new Helpers.ResultHandler().HandleResult(false, e.Message);
            }
        }
        public async Task<Helpers.ResultHandler> RemoveUser(int userId)
        {
            if (userId == 0) return new Helpers.ResultHandler().HandleResult(false, "Please select a user to remove.");

            try
            {
                var dataMOdel = new Data.Users.Models.User()
                {
                    UserId = userId,
                    DeletedAt = DateTime.Now,
                };
                var query = await dataAccess.RemoveUser(dataMOdel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Delete, "user");
            }
            catch (Exception err)
            {
                return new Helpers.ResultHandler().HandleResult(false, err.Message);
            }
        }
        private bool isDataValid(Models.User user, bool includeId = false)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.UserName))
            {
                isValid = false;
            }
            if (includeId)
            {
                isValid = (user.UserId > 0) ? true : false;
            }
            return isValid;
        }

        public void Dispose() {
            dataAccess = null;
        }
    }
}
