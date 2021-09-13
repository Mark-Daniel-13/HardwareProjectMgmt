using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Helpers
{
    public class ResultHandler
    {
        public bool Result { get; set; }
        public string ResultMsg { get; set; }
        public ResultHandler HandleResult(bool result, Enums.ResultType resultType,string callerName) {
            string _actionTypePast="";
            string _actionTypePresent = "";
            switch (resultType) {
                case Enums.ResultType.Create:
                    _actionTypePast = "added";
                    _actionTypePresent = "adding";
                    break;
                case Enums.ResultType.Delete:
                    _actionTypePast = "removed";
                    _actionTypePresent = "removing";
                    break;
                case Enums.ResultType.Update:
                    _actionTypePast = "updated";
                    _actionTypePresent = "updating";
                    break;
            }
            //0 callername ; 1 actionType :: fail = present : past
            string success = "{0} successfully {1}.";
            string fail = "There's an error in {1} {0}, please contact your administrator.";
            string msg= result ? string.Format(success, callerName, _actionTypePast) : string.Format(fail, callerName, _actionTypePresent);
            return new ResultHandler { Result = result, ResultMsg = msg };
        }
        public ResultHandler HandleResult(bool result, string msg)
        {
            return new ResultHandler { Result = result, ResultMsg = msg };
        }
    }
}
