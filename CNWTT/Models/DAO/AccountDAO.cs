using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNWTT.Models.DO;
using System.Data.Entity.Core;
using PagedList;

namespace CNWTT.Models.DAO
{
    public class AccountDAO
    {
        //get account from Account input
        public static account getAccount(Account a)
        {
            account account = null;          
            using(var db = new cnwttEntities())
            {
                try
                {
                    account = db.accounts
                        .Where(ac => ac.username == a.username)
                        .Where(ac => ac.password == a.password)
                        .Single();
                }
                catch (InvalidOperationException e){ }
                catch (EntityCommandExecutionException e){ }
                catch(EntityException e){ }
            }         
            return account;
        }

        //get account by name
        public static account getAccountByName(string userName)
        {
            account acc = null;           
            using(var db = new cnwttEntities())
            {
                try
                {
                    acc = db.accounts
                        .Where(ac => ac.username == userName)
                        .Single();
                }
                catch (InvalidOperationException e){  }
                catch (EntityCommandExecutionException e){ }
                catch (EntityException e){ }
            }           
            return acc;
        }

        //add account
        public static int addAccount(account acc)
        {
            int id = -1;
            using(var db = new cnwttEntities())
            {
                try
                {                   
                    db.accounts.Add(acc);
                    db.SaveChanges();
                    id = acc.id;                   
                }
                catch (EntityCommandExecutionException e) { }
                catch (EntityException e) { }
            }
            return id;
        }

        public static bool deleteAccount(int idAccount)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {                
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        account account = db.accounts.Find(idAccount);
                        db.accounts.Remove(account);
                        db.SaveChanges();
                        dbTransaction.Commit();
                        b = true;
                    }
                    catch (EntityCommandExecutionException e) { }
                    catch (EntityException e) { }
                }               
            }
            return b;
        }

        public static IList<account> getAllAccountIsUser()
        {
            IList<account> list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.accounts
                        .Where(a => a.accountType == "user")
                        .ToList();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (EntityException e) { }
            }
            return list;
        }

        public static IEnumerable<account> getListAccountIsUser(int page, int size)
        {
            IEnumerable<account> list = null;
            using(var db = new cnwttEntities())
            {
                try
                {
                    list = db.accounts
                        .Where(a => a.accountType == "user")
                        .OrderBy(a => a.username)
                        .ToPagedList(page, size);
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (EntityException e) { }
            }
            return list;
        }

        public static account getAccountById(int idAccount)
        {
            account account = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    account = db.accounts
                        .Where(a => a.id == idAccount)
                        .Single();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (EntityException e) { }
            }
            return account;
        }

        public static IList<account> getListAll()
        {
            IList<account> list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.accounts.ToList();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (EntityException e) { }
            }
            return list;
        }

        public static bool testPasswordOfAccount(account account, string password)
        {
            bool b = false;
            using(var db = new cnwttEntities())
            {
                try
                {
                    account acc = db.accounts.Find(account.id);
                    b = (acc != null && acc.password == password) ? true : false;                       
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (EntityException e) { }
            }
            return b;
        }

        public static bool changePasswordOfAccount(account account, string password)
        {
            bool b = false;
            using(var db = new cnwttEntities())
            {
                try
                {
                    account acc = db.accounts.Find(account.id);
                    acc.password = password;
                    db.SaveChanges();
                    b = true;
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (EntityException e) { }
            }
            return b;
        }
    }
}