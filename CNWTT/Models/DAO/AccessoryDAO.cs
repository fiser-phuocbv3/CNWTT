using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core;
using System.Web;
using CNWTT;
using CNWTT.Models.DO;

namespace CNWTT.Models.DAO
{
    public class AccessoryDAO
    {
        private static IList<accessory> list    = null;
        private static accessory acc            = null; 

        //get accessory from productId
        public static accessory getAccessoryProductId(int idA)
        {           
            using (var db = new cnwttEntities())
            {
                try
                {
                    var acc = db.accessories.Where(acce => acce.product_id == idA).Single();
                    return acc;
                }
                catch(InvalidOperationException e) { }
                catch (EntityCommandExecutionException e){ }               
            }
            return null;
        }
        
        public static bool deleteAccessories_idProduct(int idProduct)
        {
            using (var db = new cnwttEntities())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var acc = db.accessories.Where(a => a.product_id == idProduct).Single();                       
                        db.accessories.Remove(acc);
                        db.SaveChanges();
                        dbTransaction.Commit();
                        return true;                      
                    }
                    catch (InvalidOperationException e) { }
                    catch (EntityCommandExecutionException e) { }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                    }
                }
            }
            return false;
        }

        public static bool addAccessory(Accessories a)
        {
            using (var db = new cnwttEntities())
            {
                try
                {
                    var acc = new accessory();
                    acc.color = a.Color;
                    acc.manufacturer = a.Manufacturer;
                    acc.product_id = Int32.Parse(a.Product_Id);                               
                    db.accessories.Add(acc);
                    db.SaveChanges();
                    return true;                   
                }
                catch (EntityCommandExecutionException e){  }
            }
            return false;
        }
        
        public static bool editAccessory_idProduct(int idProduct, accessory accessory)
        {
            using (var db = new cnwttEntities())
            {
                try
                {
                    var acc = db.accessories.Where(a => a.product_id == idProduct).Single();
                    acc.color = accessory.color;
                    acc.manufacturer = accessory.manufacturer;
                    db.SaveChanges();
                    return true;
                }catch(Exception e) { }
            }
            return false;
        }
    }
}