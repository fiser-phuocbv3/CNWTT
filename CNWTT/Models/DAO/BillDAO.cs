using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core;
using PagedList;

namespace CNWTT.Models.DAO
{
    public class BillDAO
    {
        //private static bill bill = null;

        public static int addBill(bill bill)
        {
            int id = -1;
            using (var db = new cnwttEntities())
            {
                try
                {                    
                    db.bills.Add(bill);
                    db.SaveChanges();
                    id = bill.id;                   
                }
                catch (EntityCommandExecutionException e){ }
                catch (EntityException e){ }
            }
            return id;
        }
        
        public static bill getBillById(int id)
        {
            bill bill = null;
            using(var db = new cnwttEntities())
            {
                try
                {
                    bill = db.bills.Find(id);                        
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (EntityException e) { }
            }
            return bill;
        } 

        public static IEnumerable<bill> getListPage(int page, int pageSize)
        {
            IEnumerable<bill> list = null;
            using(var db =  new cnwttEntities())
            {
                try
                {
                    //list = db.
                }
                catch (EntityCommandExecutionException e){ }
                catch (EntityException e){ }
            }
            return list;
        }

        public static IEnumerable<bill> getListProductByCreateDate(string startDate, string endDate,
           int page, int size)
        {
            IEnumerable<bill> list = null;
            using (var db = new cnwttEntities())
            {
                string sql = " SELECT * " +
                                " FROM bill b " +
                                " WHERE CONVERT(datetime, b.create_at, 105) " +
                                " BETWEEN CONVERT(datetime,'" + startDate + "', 105) " +
                                " AND CONVERT(datetime,'" + endDate + "', 105) "; 
                try
                {
                    list = db.bills
                        .SqlQuery(sql)
                        .OrderByDescending(b => b.id)
                        .ToPagedList(page, size);
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (EntityException e) { }
            }
            return list;
        }
    }
}