using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core;
using CNWTT.Models.DO;
using System.Data.SqlClient;
using System.Configuration;
using PagedList;

namespace CNWTT.Models.DAO
{
    public class ManufacturerDAO
    {
        private static IList<manufacturer> list = null;
        private static manufacturer manu        = null;
        private static Manufacturer manufac     = null;

        //get manufacturer from id Manufacturer
        public static manufacturer getManufacturerId(int idM)
        {
            manu = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    manu = db.manufacturers.Find(idM);
                }
                catch(EntityCommandExecutionException e){ }
            }
            return manu;
        }

        public static manufacturer getManufacturerByName(string name)
        {
            manufacturer manu = null;
            using(var db = new cnwttEntities())
            {
                try
                {
                    manu = db.manufacturers
                        .Where(m => m.name == name)
                        .Single();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception e) { }
            }
            return manu;
        }

        public static Manufacturer getManufacturerFromIdProduct(int idProduct)
        {
            manufac = null;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString))
            {
                string sql = "select m.id, m.name"
                    + " from manufacturer m"
                    + " join product p on p.manufacturer_id = m.id"
                    + " where p.id LIKE @idProduct";
                conn.Open();
                using(var command = new SqlCommand(sql, conn))
                {
                    try
                    {
                        command.Parameters.AddWithValue("idProduct", idProduct);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                manufac = new Manufacturer
                                {                                  
                                    id = (int)reader["id"],
                                    name = (string)reader["name"]                                 
                                };
                              
                            }
                        }
                        reader.Close();
                    }
                    catch (SqlException e){ }
                }
            }
            return manufac;
        }

        public static IEnumerable<manufacturer> getListManufacturer(int page, int size)
        {
            IEnumerable<manufacturer> list = null;
            using(var db = new cnwttEntities())
            {
                try
                {
                    list = db.manufacturers
                        .OrderBy(m => m.name)
                        .ToPagedList(page, size);
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception e) { }
            }
            return list;
        }


        //get All
        public static IList<manufacturer> getManufacturerListAll()
        {
            list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.manufacturers.OrderBy(m => m.name).ToList();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception) { }
            }
            return list;
        }

        public static bool AddManufacturer(manufacturer m)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {
                try
                {
                    db.manufacturers.Add(m);
                    db.SaveChanges();
                    b = true;
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception) { }
            }            
            return b;
        }

        public static bool EditManufacturer(manufacturer m)
        {
            bool b = false;
            manufacturer manu = null;          
            using (var db = new cnwttEntities())
            {
                try
                {
                    manu = db.manufacturers.Find(m.id);             
                    manu.name = m.name;
                    db.SaveChanges();
                    b = true;                     
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception) { }
            }           
            return b;
        }
    }
}