using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiwanLivingCareer.Core.Helper;
using TaiwanLivingCareer.Core.BIZ.Interface.Account;

namespace TaiwanLivingCareer.Core.BIZ.Class.Account
{
    public class AccountManager:IDisposable, IAccountManager
    {
        ResultHelper result;
        public ResultHelper GetAccount(string accountID)
        {
            result = new ResultHelper();

            return result;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.result != null)
                {
                    this.result.Dispose();
                    this.result = null;
                }
            }
        }
    }
}
