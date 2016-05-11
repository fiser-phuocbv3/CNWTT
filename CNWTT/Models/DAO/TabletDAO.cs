using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core;
using CNWTT.Models.DO;

namespace CNWTT.Models.DAO
{
    public class TabletDAO
    {
        private static IList<tablet> list   = null;
        private static tablet tl            = null;

        //get tablet from productId
        public static tablet getTabletProductId(int idT)
        {
            tl = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    tl = db.tablets.Where(tabl => tabl.product_id == idT).Single();
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("Exception-TabletDAO:getTabletProductId-" + e.ToString());
                }
                catch (EntityCommandExecutionException e)
                {
                    Console.WriteLine("Error-TabletDAO:getTabletId-"+e.ToString());
                }
            }
            return tl;
        }

        public static bool addTablet(Tablet t)
        {
            bool b = false;
            if(t != null)
            {
                using (var db = new cnwttEntities())
                {
                    try
                    {
                        var tablet = new tablet
                        {
                            screen = t.Screen,
                            gpu = t.GPU,
                            ram = t.RAM,
                            memory = t.Memory,
                            camera = t.Camera,
                            connect = t.Connect,
                            product_id = Int32.Parse(t.Product_Id)
                        };
                        if(tablet != null)
                        {
                            db.tablets.Add(tablet);
                            db.SaveChanges();
                            b = true;
                        }
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine("Exception-TabletDAO:addTablet-" + e.ToString());
                    }
                    catch (ArgumentNullException e)
                    {
                        Console.WriteLine("Exception-TabletDAO:addTablet-" + e.ToString());
                    }
                    catch (EntityCommandExecutionException e)
                    {
                        Console.WriteLine("Exception-TabletDAO:addTablet-" + e.ToString());
                    }
                }
            }
            return b;
        }

        //delete tablet from product id  
        public static bool deleteTablet_idProduct(int idProduct)
        {
            bool b = false;          
            using (var db = new cnwttEntities())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        tablet tl = db.tablets.Where(t => t.product_id == idProduct).Single();
                        if (tl != null)
                        {
                            db.tablets.Remove(tl);
                            db.SaveChanges();
                            dbTransaction.Commit();
                            b = true;
                        }
                    }
                    catch (InvalidOperationException e) { }
                    catch (EntityCommandExecutionException e) { }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                    }
                }
            }         
            return b;
        }

        public static bool editTablet_idProduct(int idProduct, tablet tablet)
        {
            bool b = false;
            using(var db = new cnwttEntities())
            {
                try
                {
                    var tl = db.tablets.Where(t => t.product_id == idProduct).Single();
                    if(tl != null)
                    {
                        tl.screen = tablet.screen;
                        tl.gpu = tablet.gpu;
                        tl.camera = tablet.camera;
                        tl.ram = tablet.ram;
                        tl.memory = tablet.memory;
                        tl.connect = tablet.connect;
                        db.SaveChanges();
                        b = true;
                    }
                }
                catch (Exception e)
                {

                }
            }
            return b;
        }
    }
}