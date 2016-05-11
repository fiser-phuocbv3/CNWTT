using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core;

namespace CNWTT.Models.DAO
{
    public class AccountInforDAO
    {

        public static int addAccountInfor(accountInfor accIn)
        {
            int idAccInfo = -1;
            using (var db = new cnwttEntities())
            {
                try
                {
                    db.accountInfors.Add(accIn);
                    db.SaveChanges();
                    idAccInfo = accIn.id;
                }
                catch (EntityCommandExecutionException e) { }
                catch (EntityException e) { }
            }
            return idAccInfo;
        }

        public static bool deleteAccountInfoByIdAccount(int idAccount)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        accountInfor accInfor = db.accountInfors
                            .Where(a => a.account_id == idAccount)
                            .Single();
                        db.accountInfors.Remove(accInfor);
                        db.SaveChanges();
                        dbTransaction.Commit();
                        b = true;
                    }
                    catch (InvalidOperationException) { }
                    catch (EntityCommandExecutionException e) { }
                    catch (EntityException e) { }
                }
            }
            return b;
        }

        public static accountInfor getAccountInfoByIdAccount(int idAccount)
        {
            accountInfor accountInfo = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    accountInfo = db.accountInfors
                        .Where(accIn => accIn.account_id == idAccount)
                        .Single();
                }
                catch (InvalidOperationException) { }
                catch (EntityCommandExecutionException e) { }
                catch (EntityException e) { }
            }
            return accountInfo;
        }

        public static IList<accountInfor> getListAll()
        {
            IList<accountInfor> list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.accountInfors
                        .ToList();
                }
                catch (InvalidOperationException) { }
                catch (EntityCommandExecutionException e) { }
                catch (EntityException e) { }
            }
            return list;
        }
    }
}