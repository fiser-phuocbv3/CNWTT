using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CNWTT;
using System.Data.Entity.Core;
using CNWTT.Models.DO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Validation;
using CNWTT.Models.DO;
using PagedList;

namespace CNWTT.Models.DAO
{
    public class ProductDAO
    {
        private static IList<product> list  = null;
        private static product p            = null;
        private static IList<Product> listProduct = new List<Product>();

        //get list new product 4
        public static IList<product> getNewProduct4()
        {
            list = null;
            using (var db = new cnwttEntities())
            {
                try
                {

                    list = db.products.SqlQuery("SELECT TOP 4 * FROM dbo.product ORDER BY id DESC").ToList();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception) { }
            }
            return list;
        }

        //get list phone product 4
        public static IList<product> getPhoneProduct4()
        {
            list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.products.SqlQuery("SELECT TOP 4 * FROM dbo.product WHERE productType LIKE '1' ORDER BY id DESC").ToList();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception) { }
            }
            return list;
        }

        //get list tablet product 4
        public static IList<product> getTabletProduct4()
        {
            list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.products.SqlQuery("SELECT TOP 4 * FROM dbo.product WHERE productType LIKE '2' ORDER BY id DESC").ToList();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception) { }
            }
            return list;
        }

        //get list laptop product 4
        public static IList<product> getLaptopProduct4()
        {
            list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.products.SqlQuery("SELECT TOP 4 * FROM dbo.product WHERE productType LIKE '3' ORDER BY id DESC").ToList();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception) { }
            }
            return list;
        }

        //get list desktop product 4
        public static IList<product> getDesktopProduct4()
        {
            list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.products.SqlQuery("SELECT TOP 4 * FROM dbo.product WHERE productType LIKE '4' ORDER BY id DESC").ToList();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception) { }
            }
            return list;
        }

        public static IList<product> getAccessoryProduct4()
        {
            list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.products.SqlQuery("SELECT TOP 4 * FROM dbo.product WHERE productType LIKE '5' ORDER BY id DESC").ToList();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception) { }
            }
            return list;
        }

        //get list product (4 item) from productType
        public static IList<product> getListProduct4(string productType)
        {
            list = null;
            using (var db = new cnwttEntities())
            {
                string sql = "SELECT TOP 4 * FROM dbo.product WHERE productType LIKE '"+ productType +"' ORDER BY id DESC";
                try
                {
                    list = db.products.SqlQuery(sql).ToList();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception) { }
            }
            return list;
        }

        //get all product list 
        public static IList<product> getAllList()
        {
            list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    //list = db.products.SqlQuery("SELECT * FROM dbo.product ORDER BY id DESC").ToList();
                    list = db.products
                        .OrderByDescending(p => p.id)
                        .ToList();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception) { }
            }
            return list;
        }

        public static IEnumerable<product> getALLByPage(int page, int size)
        {
            IEnumerable<product> list = null;
            using(var db = new cnwttEntities())
            {
                try
                {
                    list = db.products
                        .OrderByDescending(p => p.id)
                        .ToPagedList(page, size);
                }
                catch (EntityException e) { }
                catch (Exception e) { }
            }
            return list;
        }


        //get list product from type product
        public static IList<product> getList(string type)
        {
            list = null;
            using (var db = new cnwttEntities())
            {
               // string sql = "SELECT * FROM dbo.product WHERE productType LIKE '"+ type +"' ORDER BY id DESC";
                try
                {
                   // list = db.products.SqlQuery(sql).ToList();
                    list = db.products
                        .Where(p => p.productType == type)
                        .OrderByDescending(p => p.id)
                        .ToList();
                }
                catch (EntityCommandExecutionException e){ }
            }
            return list;
        }
        
        public static IEnumerable<product> getListProductByType(string type, int page, int size)
        {
            IEnumerable<product> list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.products
                        .Where(p => p.productType == type)
                        .OrderByDescending(p => p.id)
                        .ToPagedList(page, size);
                }
                catch (EntityException e) { }
                catch (Exception e) { }
            }
            return list;
        }

        //get product from name
        public static product getProductName(string name)
        {
            p = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    p = db.products.Where(prod => prod.name == name).Single();
                }
                catch (InvalidOperationException e){ }
                catch(EntityCommandExecutionException e){ }
            }
            return p;
        }

        //get product by id
        public static product getProductId(string id)
        {
            product p = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    int idP = Int32.Parse(id);
                    p = db.products.Find(idP);
                }
                catch (EntityCommandExecutionException e) { }
            }
            return p;
        }

        public static IList<product> getListProductByListBillDetail(IList<billDetail> listBillDetail)
        {
            IList<product> list = null;
            string sql = " SELECT * "
                +" FROM product " 
                +" WHERE id IN ( ";
            foreach(var item in listBillDetail)
            {
                sql += item.product_id + ",";
            }
            sql += " 0 ) ";
            using(var db = new cnwttEntities())
            {
                try
                {
                    list = db.products
                    .SqlQuery(sql)
                    .ToList();
                }
                catch (InvalidOperationException e) { }
                catch (EntityCommandExecutionException e) { }
                catch (Exception) { }
            }
            return list;
        }

        //get list product from manufacturer and cost
        public static IList<Product> getListProduct_Manufacturer_Cost(string manufacturer,string content, string startCost, string endCost)
        {
            listProduct = new List<Product>();
            
            using(var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString))
            {
                string sql = "select * "
                    + " from product p"                      
                    + " where convert(int,cost) "
                    + " between convert(int, @startCost) and convert(int, @endCost)"
                    + " and manufacturer_id LIKE @idManufacturer"
                    + " and productType LIKE @productType";
                conn.Open();
                using (var command = new SqlCommand(sql, conn))
                {
                    Product p = null;
                    try
                    {
                        command.Parameters.AddWithValue("startCost", startCost);
                        command.Parameters.AddWithValue("endCost", endCost);
                        command.Parameters.AddWithValue("idManufacturer", manufacturer);
                        command.Parameters.AddWithValue("productType", content);
                        SqlDataReader reader = command.ExecuteReader();
                            
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                p = new Product
                                {
                                    id = (int)reader["id"],
                                    name = (string)reader["name"],
                                    cost = (string)reader["cost"],
                                    picture = (string)reader["picture"],
                                    number = (string)reader["number"],
                                    describle = (string)reader["describle"],
                                    productType = (string)reader["productType"],
                                    manufacturer_id = reader["manufacturer_id"].ToString()
                                };
                                listProduct.Add(p);
                            }                               
                        }                          
                    }
                    catch(SqlException e){ }
                }                        
            }
            return listProduct;
        }

        //require manu, type, cost
        public static IEnumerable<product> getListProductByManuAndCostAndType(int page, int size, int manu, string type, string startCost, string endCost)
        {
            IEnumerable<product> list = null;
            string sql = "select * "
                    + " from product p"
                    + " where convert(int,cost) "
                    + " between convert(int, " + startCost + ") and convert(int, " + endCost + ")";
                   // + " and manufacturer_id LIKE @idManufacturer "
                    //+ " and productType LIKE @productType";
            using(var db = new cnwttEntities())
            {
                try
                {
                    list = db.products
                        .SqlQuery(sql)
                        .Where(p => p.manufacturer_id == manu)
                        .Where(p => p.productType == type)
                        .OrderByDescending(p => p.id)
                        .ToPagedList(page, size);
                }
                catch (EntityException e) { }
                catch (Exception e) { }
            }
            return list;
        }

        //get list product by list cart
        public static IList<product> getListInCart(IList<Cart> listCart)
        {
            list = null;
            if(listCart != null)
            {
                string listId = "";
                for(int i = 0; i< listCart.Count; i++)
                {
                    listId += "'" +listCart[i].id +"'";
                    if(i < ( listCart.Count - 1 ))
                    {
                        listId += ",";
                    }
                }
                string sql = "";
                try
                {
                    if(listId != "")
                    {
                        sql = "SELECT * FROM dbo.product WHERE id IN (" + listId + ")";
                        using (var db = new cnwttEntities())
                        {
                            list = db.products.SqlQuery(sql).ToList();
                        }
                    }
                }
                catch (EntityCommandExecutionException e) { }
            }
            return list;
        }

        //add product
        public static int addProduct(Product p)
        {
            int idProduct = -1;
            if(p!= null)
            {
                using (var db = new cnwttEntities())
                {
                    try
                    {
                        var prod = new product()
                        {
                            name = p.name,
                            cost = p.cost,
                            picture = p.picture,
                            number = p.number,
                            describle = p.describle,
                            productType = p.productType,
                            manufacturer_id = Int32.Parse(p.manufacturer_id)
                        };
                        if (prod != null)
                        {
                            db.products.Add(prod);
                            db.SaveChanges();
                            idProduct = prod.id;
                        }
                    }
                    catch(DbEntityValidationException e)
                    {
                        Console.WriteLine("Exception-ProductDAO:addProduct-" + e.ToString());
                    }
                    catch (ArgumentNullException e)
                    {
                        Console.WriteLine("Exception-ProductDAO:addProduct-" + e.ToString());
                    }
                    catch(EntityCommandExecutionException e)
                    {
                        Console.WriteLine("Exception-ProductDAO:addProduct-"+ e.ToString());
                    }
                }
            }
            return idProduct;
        }   
        
        public static bool deleteProduct(int idProduct)
        {
            bool b = false;
            using (var db = new cnwttEntities())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        product p = db.products.Find(idProduct);
                        if (p != null)
                        {
                            db.products.Remove(p);
                            db.SaveChanges();
                            dbTransaction.Commit();
                            b = true;
                        }
                    }
                    catch (InvalidOperationException e) { }
                    catch (EntityCommandExecutionException e){ }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                    }
                }
            }
            return b;
        }     
        
        public static bool EditProduct(int idProduct, product prod)
        {
            using (var db = new cnwttEntities())
            {
                try
                {
                    var p = db.products.Find(idProduct);
                    if(p != null)
                    {
                        p.name = prod.name;
                        p.cost = prod.cost;
                        p.picture = prod.picture;
                        p.number = prod.number;
                        p.describle = prod.describle;
                        p.productType = prod.productType;
                        p.manufacturer_id = prod.manufacturer_id;
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (EntityCommandExecutionException e)
                {
                    return false;
                }
            }
            return false;
        }  
        
        //require manu, type
        public static IEnumerable<product> getProductByTypeAndManu(int page, int pageSize, int idmanu, string type)
        {
            IEnumerable<product> list = null;
            using(var db = new cnwttEntities())
            {
                try
                {
                    list = db.products
                        .Where(p => p.productType == type)
                        .Where(p => p.manufacturer_id == idmanu)                    
                        .OrderByDescending( p => p.id)
                        .ToPagedList(page, pageSize);
                }
                catch (EntityException e) { }
                catch (Exception e){ }
            }
            return list;
        }

        //search like name product
        public static IEnumerable<product> searchProductByName(string name, int page, int size)
        {
            IEnumerable<product> list = null;
            using(var db = new cnwttEntities())
            {
                try
                {
                    list = db.products
                        .Where(p => p.name.Contains(name))
                        .OrderByDescending(p => p.id)
                        .ToPagedList(page, size);                        
                }
                catch (EntityException e) { }
                catch (Exception e) { }
            }
            return list;
        }

        //search like name, by manufacturer 
        public static IEnumerable<product> searchProductByNameAndManu(string name, int idManu, int page, int size)
        {
            IEnumerable<product> list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.products
                        .Where(p => p.name.Contains(name))
                        .Where(p => p.manufacturer_id == idManu)
                        .OrderByDescending(p => p.id)
                        .ToPagedList(page, size);
                }
                catch (IndexOutOfRangeException e) { }
                catch (EntityException e) { }
                catch (Exception e) { }
            }
            return list;
        }

        public static IEnumerable<product> searchProductByNameAndManuAndCost(string name, int idManu, string startCost, string endCost, int page, int size)
        {
            IEnumerable<product> list = null;
            using (var db = new cnwttEntities())
            {
                string sql = "select * "
                    + " from product p"
                    + " where convert(int,cost) "
                    + " between convert(int, " + startCost + ") and convert(int, " + endCost + ")";
                try
                {
                    list = db.products
                        .SqlQuery(sql)
                        .Where(p => p.name.Contains(name))
                        .Where(p => p.manufacturer_id == idManu)
                        .OrderByDescending(p => p.id)
                        .ToPagedList(page, size);
                }
                catch (IndexOutOfRangeException e) { }
                catch (EntityException e) { }
                catch (Exception e) { }
            }
            return list;
        }

        //require manu in case all
        public static IEnumerable<product> getAllListProductByManufacturer(int manu, int page, int size)
        {
            IEnumerable<product> list = null;
            using (var db = new cnwttEntities())
            {
                try
                {
                    list = db.products
                        .Where(p => p.manufacturer_id == manu)
                        .OrderByDescending(p => p.id)
                        .ToPagedList(page, size);
                }
                catch (IndexOutOfRangeException e) { }
                catch (EntityException e) { }
                catch (Exception e) { }
            }
            return list;
        }

        public static IEnumerable<product> getAllListProductByManuAndCost(int page, int size, int manu, string startCost, string endCost)
        {
            IEnumerable<product> list = null;
            using (var db = new cnwttEntities())
            {
                string  sql = "select * "
                    + " from product p"
                    + " where convert(int,cost) "
                    + " between convert(int, " + startCost + ") and convert(int, " + endCost + ")";
                try
                {
                    list = db.products
                        .SqlQuery(sql)
                        .Where(p => p.manufacturer_id == manu)
                        .OrderByDescending(p => p.id)
                        .ToPagedList(page, size);
                }
                catch (IndexOutOfRangeException e) { }
                catch (EntityException e) { }
                catch (Exception e) { }
            }
            return list;
        }
    }
}