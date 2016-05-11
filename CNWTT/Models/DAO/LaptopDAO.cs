using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core;
using CNWTT.Models.DO;

namespace CNWTT.Models.DAO
{
    public class LaptopDAO
    {
        private static IList<laptop> list   = null;
       // private static laptop lt            = null;

        //get laotop from productId
        public static laptop getLaptopProductId(int idL)
        {
            laptop lt = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    lt = db.laptops
                        .Where(lapt => lapt.product_id == idL)
                        .Single();
                }
                catch (InvalidOperationException e){ }
                catch (EntityCommandExecutionException e){ }
            }
            return lt;
        }

        //add new Laptop
        public static bool addLaptop(Laptop l)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {
                try
                {
                    var laptop = new laptop
                    {
                        processor = l.Processor,
                        ram = l.RAM,
                        gpu = l.GPU,
                        hardDisk = l.HardDisk,
                        screen = l.Screen,
                        os = l.OS,
                        connect = l.Connect,
                        pin = l.Pin,
                        product_id = Int32.Parse(l.Product_Id)
                    };
                    if(laptop != null)
                    {
                        db.laptops.Add(laptop);
                        db.SaveChanges();
                        b = true;
                    }
                }
                catch (InvalidOperationException e) { }                
                catch (ArgumentNullException e) { }                 
                catch (EntityCommandExecutionException e) { }                           
            }
            return b;
        }

        public static bool deleteLaptop_idProduct(int idProduct)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var lt = db.laptops
                            .Where(l => l.product_id == idProduct)
                            .Single();
                        if (lt != null)
                        {
                            db.laptops.Remove(lt);
                            db.SaveChanges();
                            dbTransaction.Commit();
                            b = true;
                        }
                    }
                    catch (InvalidOperationException e) { dbTransaction.Rollback(); }
                    catch (EntityCommandExecutionException e) { dbTransaction.Rollback(); }
                    catch (Exception e) { dbTransaction.Rollback(); }
                }           
            }
            return b;
        }

        public static bool editLaptop_idProduct(int idProduct, laptop laptop)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {
                try
                {
                    var lap = db.laptops.Where(l => l.product_id == idProduct).Single();
                    if(lap != null)
                    {
                        lap.screen = laptop.screen;
                        lap.gpu = laptop.gpu;
                        lap.hardDisk = laptop.hardDisk;
                        lap.ram = laptop.ram;
                        lap.connect = laptop.connect;
                        lap.processor = laptop.processor;
                        lap.os = laptop.os;
                        lap.pin = laptop.pin;
                        db.SaveChanges();
                        b = true;
                    }
                }
                catch (Exception e){ }
            }
            return b;
        }
    }
}