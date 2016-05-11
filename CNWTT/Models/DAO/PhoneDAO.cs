using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core;
using CNWTT.Models.DO;

namespace CNWTT.Models.DAO
{
    public class PhoneDAO
    {
        private static IList<phone> list = null;
        private static phone ph = null;

        //get phone from productId
        public static phone getPhoneProductId(int idP)
        {
            ph = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    ph = db.phones.Where(p => p.product_id == idP).Single();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }               
            }
            return ph;
        }

        public static bool addPhone(Phone p)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {
                try
                {
                    var phone = new phone()
                    {
                        phoneType = p.PhoneType,
                        screen = p.Screen,
                        camera = p.Camera,
                        memory = p.Memory,
                        os = p.OS,
                        chipset = p.Chipset,
                        product_id = Int32.Parse(p.Product_Id)
                    };
                    if (phone != null)
                    {
                        db.phones.Add(phone);
                        db.SaveChanges();
                        b = true;
                    }
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
            }
            return b;
        }

        public static bool deletePhone_idProduct(int idProduct)
        {
            bool b = false;
            ph = null;

            using (var db = new cnwttEntities())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        ph = db.phones.Where(p => p.product_id == idProduct).Single();
                        if (ph != null)
                        {
                            db.phones.Remove(ph);
                            db.SaveChanges();
                            dbTransaction.Commit();
                            b = true;
                        }
                    }
                    catch (InvalidOperationException e) { dbTransaction.Rollback(); }
                    catch (EntityCommandExecutionException e) { dbTransaction.Rollback(); }
                    catch (Exception e){ dbTransaction.Rollback(); }
                }

            }
            return b;
        }

        public static bool editPhone_idProduct(int idProduct, phone phone)
        {
            bool b = false;          
            using (var db = new cnwttEntities())
            {
                try
                {
                    var ph = db.phones.Where(p => p.product_id == idProduct).Single();
                    if (ph != null)
                    {
                        ph.phoneType = phone.phoneType;
                        ph.screen = phone.screen;
                        ph.camera = phone.camera;
                        ph.memory = phone.memory;
                        ph.os = phone.os;
                        ph.chipset = phone.chipset;
                        db.SaveChanges();
                        b = true;
                    }
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
            }
            return b;
        }
    }
}