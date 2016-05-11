using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNWTT;
using System.Data.Entity.Core;
using CNWTT.Models.DO;

namespace CNWTT.Models.DAO
{
    public class DesktopDAO
    {
        private static IList<desktop> list  = null;
        private static desktop dt           = null;

        //get desktop from ProductId
        public static desktop getDesktopProductId(int idD)
        {
            dt = null;
            using(var db = new cnwttEntities())
            {
                try
                {
                    dt = db.desktops.Where(deskt => deskt.product_id == idD).Single();
                }
                catch (InvalidOperationException e){ }

                catch (EntityCommandExecutionException e){ }

                catch(Exception e) { }
            }
            return dt;
        }

        public static bool deleteDesktop_idProduct(int idProduct)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var dt = db.desktops.Where(d => d.product_id == idProduct).Single();
                        if (dt != null)
                        {
                            db.desktops.Remove(dt);
                            db.SaveChanges();
                            dbTransaction.Commit();
                            b = true;
                        }
                    }
                    catch (InvalidOperationException e) { }
                    catch (EntityCommandExecutionException e) { }
                    catch(Exception e)
                    {
                        dbTransaction.Rollback();
                    }
                }               
            }
            return b;
        }

        //add desktop
        public static bool addDesktop(Desktop d)
        {
            bool b = false;
            using(var db = new cnwttEntities())
            {
                try
                {
                    var desktop = new desktop
                    {
                        processor = d.Processor,
                        ram = d.RAM,
                        gpu = d.GPU,
                        hardDisk = d.HardDisk,
                        screen = d.Screen,
                        os = d.OS,
                        connect = d.Connect,
                        pin = d.Pin,
                        product_id = Int32.Parse(d.Product_Id)
                    };
                    if (desktop != null)
                    {
                        db.desktops.Add(desktop);
                        db.SaveChanges();
                        b = true;
                    }
                }
                catch (EntityCommandExecutionException e){ }
            }
            return b;
        }

        public static bool editDesktop_idProduct(int idProduct, desktop desktop)
        {
            bool b = false;
            using(var db = new cnwttEntities())
            {
                try
                {
                    var dt = db.desktops.Where(d => d.product_id == idProduct).Single();
                    if(dt != null)
                    {
                        dt.screen = desktop.screen;
                        dt.gpu = desktop.gpu;
                        dt.hardDisk = desktop.hardDisk;
                        dt.ram = desktop.ram;
                        dt.connect = desktop.connect;
                        dt.processor = desktop.processor;
                        dt.os = desktop.os;
                        dt.pin = desktop.pin;
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