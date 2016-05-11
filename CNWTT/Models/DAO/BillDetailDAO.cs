using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNWTT.Models.DO;
using System.Data.Entity.Core;

namespace CNWTT.Models.DAO
{
    public class BillDetailDAO
    {
        private static billDetail billDetail = null;
        private static IList<billDetail> listBillDetail = null;

        public static billDetail getBillDetailById(int id)
        {
            billDetail = null;

            return billDetail;
        }

        public static IList<billDetail> getListBillDetailByIdBill(int idBill)
        {
            IList<billDetail> list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.billDetails
                        .Where(b => b.bill_id == idBill)
                        .ToList();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception) { }
            }
            return list;
        }

        public static bool addBillDetail(billDetail bill)
        {
            bool b = false;

            return b;
        }

        public static bool addListBillDetail(IList<billDetail> list)
        {
            bool b = false;
            using(var db = new cnwttEntities())
            {
                try
                {
                    if(list != null && list.Count > 0)
                    {
                        foreach(var l in list)
                        {
                            db.billDetails.Add(l);
                            db.SaveChanges();                                                      
                        }
                        b = true;
                    }
                }
                catch (EntityCommandExecutionException e){ }
                catch (EntityException e){ }
            }
            return b;
        }

        public static bool deleteBillByProductId(int idProduct)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        IList<billDetail> list = db.billDetails
                            .Where(bill => bill.product_id == idProduct)
                            .ToList();
                        if (list != null && list.Count > 0)
                        {
                            foreach (var item in list)
                            {
                                db.billDetails.Remove(item);
                                db.SaveChanges();
                            }
                            dbTransaction.Commit();
                            b = true;
                        }
                    }
                    catch (InvalidOperationException e) { }
                    catch (EntityCommandExecutionException e) { }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                    }
                }
                return b;
            }
        }
    }
}