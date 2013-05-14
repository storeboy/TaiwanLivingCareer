using System;
namespace TaiwanLivingCareer.Core.BIZ.Interface.Account
{
    interface IAccountManager
    {
        TaiwanLivingCareer.Core.Helper.ResultHelper GetAccount(string accountID);
    }
}
