using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Entity.Core;
using CNWTT.Models.DAO;
using CNWTT.Models.DO;
using CNWTT.App_Start;
using CNWTT.Core.User;

namespace CNWTT.Controllers
{
    public class HomeController : Controller
    {
        private static IList<product> listAll       = new List<product>();
        private static IList<product> listProduct   = new List<product>();
        private static IList<news> listNews         = null;
        //private static IList<>


        private static int currentPage  = 1;
        private static int pageCount    = 0;      
        private static int page         = 0;

        // GET: Home
        public ActionResult Index()
        {
            IList<product> laptopProduct    = null;
            IList<product> newProducts      = null;
            IList<product> phoneProduct     = null;
            IList<product> tabletProduct    = null;
            IList<product> desktopProduct   = null;
            IList<product> accessoryProduct = null;
            IList<manufacturer> manu = null;

                     
            newProducts = ProductDAO.getNewProduct4();
            phoneProduct = ProductDAO.getPhoneProduct4();                 
            tabletProduct = ProductDAO.getTabletProduct4();                
            laptopProduct = ProductDAO.getLaptopProduct4();            
            desktopProduct = ProductDAO.getDesktopProduct4();              
            accessoryProduct = ProductDAO.getAccessoryProduct4();
            manu = ManufacturerDAO.getManufacturerListAll();
                                    
            ViewBag.newProducts = newProducts;
            ViewBag.phoneProduct = phoneProduct;
            ViewBag.tabletProduct = tabletProduct;
            ViewBag.desktopProduct = desktopProduct;
            ViewBag.laptopProduct = laptopProduct;
            ViewBag.accessoryProduct = accessoryProduct;
            ViewBag.listManufacturer = manu;
          
            return View();
        }

//login
        
        public ActionResult Login(Account acc)
        {
            string msg = "";
            if(acc != null && acc.username != null && acc.password != null)
            {
                acc.password = HomeCore.EncodeMD5(acc.password);
                account account = AccountDAO.getAccount(acc);
                if(account != null)
                {
                    Session[CONFIG.SESSION_USER] = account;                
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    msg = "Đăng nhập không thành công";
                }
            }
            ViewBag.msg = msg;
            return View("Login");
        }

 //logout
        //logout
        public ActionResult Logout()
        {
            Session.Remove(CONFIG.SESSION_USER);
            return RedirectToAction("Index", "Home");
        }
      
//Register
        //[Authorize(Users = "SESSION_USER")]
        public ActionResult Register(accountInfor accInfor)
        {
            account acc = null;
            string msg = "Có lỗi xảy ra";
            
            string userName = Request["userName"];
            string password = Request["password"];
            string confirmPassword = Request["confirmPassword"];

            if (accInfor != null && userName != null && password != null &&
                    confirmPassword != null && password.Equals(confirmPassword))
            {                     
                acc = AccountDAO.getAccountByName(userName);
                if(acc == null)
                {
                    var account = new account();
                    account.username = userName;
                    account.password = HomeCore.EncodeMD5(password);
                    account.accountType = "user";                  
                    int idAccount = AccountDAO.addAccount(account);
                    if (idAccount > 0)
                    {
                        //acc = AccountDAO.getAccountByName(account.username);
                        accInfor.account_id = idAccount;
                        int idAccInfo = AccountInforDAO.addAccountInfor(accInfor);
                        msg = (idAccInfo > 0) ? "Thêm thành công" : "Thêm không thành công";
                    }
                    else
                    {
                        msg = "Thêm không thành công";
                    }
                    acc = null;
                }
                else
                {
                    msg = "Tên này đã được dùng! Bạn vui lòng chọn tên khác";
                }          
            }
            else
            {
                msg = "";
            }
            ViewBag.msg = msg;
            return View("Register");
        }

        public string checkUserName(string username)
        {
            if(username != null)
            {
                account acc = AccountDAO.getAccountByName(username);
                if (acc != null) return "false";
            }
            return "true";
        }

//Cart
        public ActionResult AddCart(string id)
        {
            string count = Request["count"];
            Cart c = null;
            bool b = false;
            if(id != null)
            {
                var cart = (IList<Cart>)Session[CONFIG.CART];
                if (cart == null)
                {
                    cart = new List<Cart>();
                }

                if (count != null)
                {
                    c = new Cart(id, count);
                }
                else
                {
                    foreach (var l in cart)
                    {
                        if(l.id == id)
                        {
                            l.count = (Int32.Parse(l.count) + 1).ToString();
                            b = true;
                        }
                    }
                    if (!b)
                    {
                        c = new Cart(id, "1");
                    }
                }             
                if(!b)
                {
                    cart.Add(c);
                }              
                Session.Remove(CONFIG.CART);
                Session.Add(CONFIG.CART,cart);          
            }
            return RedirectToAction("Cart","Home");
        }

        //delete item in cart by idItem
        public ActionResult DeleteCart(string idCart)
        {          
            if (idCart != null)
            {
                var list = (IList<Cart>)Session[CONFIG.CART];
                int c = list.Count;
                if(list != null)
                {
                    foreach(var l in list)
                    {
                        if(l.id == idCart)
                        {
                            list.Remove(l);
                            break;
                        }
                    }
                }
                Session.Remove(CONFIG.CART);
                Session.Add(CONFIG.CART, list);
            }
            return RedirectToAction("Cart","Home");
        }

        //view cart
      
        public ActionResult Cart()
        {
            IList<product> listNewProduct = null;
            var cart = (IList<Cart>)Session[CONFIG.CART];
            IList<product> list = null;
            if(cart != null)
            {
                list = ProductDAO.getListInCart(cart);              
            }
            listNewProduct = ProductDAO.getNewProduct4();
            ViewBag.listNew = listNewProduct;
            ViewBag.listInCart = list;
            return View();
        } 

        
        public ActionResult putProduct()
        {
            IList<billDetail> listBillDetail = new List<billDetail>();
            listProduct = null;
            string msg = "";
            var accUser = (account)Session[CONFIG.SESSION_USER];
            if (accUser != null)
            {
                int sumMoney = 0;

                var listCart = (IList<Cart>)Session[CONFIG.CART];
                if(listCart != null && listCart.Count > 0)
                {
                    DateTime datetime = DateTime.Now;
                    listProduct = ProductDAO.getListInCart(listCart);
                    sumMoney = HomeCore.sumMoney(listCart, listProduct);
                    var bill = new bill
                    {
                        totalMoney = sumMoney.ToString(),
                        accountAddress = "1",
                        create_at = datetime.ToString(CONFIG.FORMAT_TIME),
                        update_at = datetime.ToString(CONFIG.FORMAT_TIME),
                        account_id = accUser.id
                    };
                    int idBill = BillDAO.addBill(bill);
                    listBillDetail = HomeCore.getListBillByListCartAndListProduct(listCart, listProduct, idBill);
                    bool b = BillDetailDAO.addListBillDetail(listBillDetail);
                    if (b)
                    {
                        msg = "Đặt hàng thành công";
                        Session.Remove(CONFIG.CART);
                    }
                    else
                    {
                        msg = "Đặt hàng không thành công";
                    }
                }
                else
                {
                    msg = "Giỏ hàng trống";
                }
                ViewBag.msg = msg;      
                return RedirectToAction("Cart", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }       
        }


//product      
       //view product detail
        public ActionResult productDetail(string p)
        {
            IList<product> list = null;
            listAll     = null;
            pageCount   = 0;
            if (p != null)
            {
                page = 1;
                switch (p)
                {
                    case "all":
                        listAll = ProductDAO.getAllList();
                        list = getProductList(page);       
                        break;
                    case "mobile":
                        listAll = ProductDAO.getList("1");                      
                        list = getProductList(page);
                        break;
                    case "laptop":
                        listAll = ProductDAO.getList("3");
                        list = getProductList(page);
                        break;
                    case "tablet":
                        listAll = ProductDAO.getList("2");
                        list = getProductList(page);
                        break;
                    case "desktop":
                        listAll = ProductDAO.getList("4");
                        list = getProductList(page);
                        break;
                    case "accessories":
                        listAll = ProductDAO.getList("5");
                        list = getProductList(page);
                        break;
                }
                if(listAll != null)
                {
                    pageCount = (listAll.Count - 1) / 12 + 1;
                }
                ViewBag.pageCount = pageCount;
                ViewBag.listProduct = list;
            }
            return View();
        }

        //loc san pham theo loai, hang san xuat, gia
        public ActionResult ViewProduct(int page = 1, int size = 20)
        {
            string manu = Request["manu"];
            string type = Request["type"];
            string cost = Request["cost"];
            string search = Request["search"];

            string startCost = "";
            string endCost = "";

            IEnumerable<product> model = null;
            IList<manufacturer> listManufacturer = ManufacturerDAO.getManufacturerListAll();
   
            if(type != null)
            {
                if(cost != null)
                {                 
                    HomeCore.caculatorPrice(cost, out startCost, out endCost);
                }
                string t = HomeCore.returnProductType(type.ToString());
                if (type.ToString() == "all")
                {
                    if(search != null)
                    {
                        if(manu != null)
                        {
                            if(cost != null)
                            {
                                model = ProductDAO.searchProductByNameAndManuAndCost(
                                    search,
                                    Int32.Parse(manu),
                                    startCost,
                                    endCost,
                                    page,
                                    size);
                            }
                            else
                            {
                                model = ProductDAO.searchProductByNameAndManu(
                                    search, 
                                    Int32.Parse(manu), 
                                    page, 
                                    size);
                            }
                        }
                        else
                        {
                            model = (IEnumerable<product>)TempData["listProduct"];
                        }
                    }
                    else if(manu != null)
                    {
                        if(cost != null)
                        {
                            //manu and cost
                            model = ProductDAO.getAllListProductByManuAndCost(page, size, Int32.Parse(manu), startCost, endCost);
                        }
                        else
                        {
                            model = ProductDAO.getAllListProductByManufacturer(Int32.Parse(manu), page, size);
                        }                     
                    }
                    else
                    {
                        model = ProductDAO.getALLByPage(page, size);
                    }                                
                }            
                else if (manu != null)
                {                   
                    if (cost != null)
                    {
                        
                        model = ProductDAO.getListProductByManuAndCostAndType(page, size, Int32.Parse(manu), t, startCost, endCost);
                    }
                    else
                    {
                        model = ProductDAO.getProductByTypeAndManu(page, size, Int32.Parse(manu), t);
                    }
                }
                else
                {
                    model = ProductDAO.getListProductByType(t, page, size);
                }
            }
            ViewBag.listManufacturer = listManufacturer;  
            return View(model);
        }


        public ActionResult Preview(string p)
        {
            product prod            = null;
            Manufacturer manufac    = null;
            listNews                = null;
            IList<product> list = null;

            if(p != null)
            {
                prod = ProductDAO.getProductId(p);
                if(prod != null)
                {
                    manufac = ManufacturerDAO.getManufacturerFromIdProduct(prod.id);
                    string str = prod.productType;
                    switch (prod.productType)
                    {
                        case "1":
                            phone ph = PhoneDAO.getPhoneProductId(prod.id);
                            ViewBag.productDetail = ph;
                            list = ProductDAO.getListProduct4("1");
                            break;
                        case "2":
                            tablet tab = TabletDAO.getTabletProductId(prod.id);
                            ViewBag.productDetail = tab;
                            list = ProductDAO.getListProduct4("2");
                            break;
                        case "3":
                            laptop lap = LaptopDAO.getLaptopProductId(prod.id);
                            ViewBag.productDetail = lap;
                            list = ProductDAO.getListProduct4("3");
                            break;
                        case "4":
                            desktop desk = DesktopDAO.getDesktopProductId(prod.id);
                            ViewBag.productDetail = desk;
                            list = ProductDAO.getListProduct4("4");
                            break;
                        case "5":
                            accessory acc = AccessoryDAO.getAccessoryProductId(prod.id);
                            ViewBag.productDetail = acc;
                            list = ProductDAO.getListProduct4("5");
                            break;
                    }
                }
            }
            listNews = NewsDAO.getListNews5();

            ViewBag.list            = list;
            ViewBag.news            = listNews;
            ViewBag.manufacturer    = manufac;
            ViewBag.product         = prod;
            return View();
        }

        public IList<product> getProductList(int page)
        {
            currentPage = 1;
            IList<product> list = new List<product>();
            if (page > 0 && listAll != null) {
                int start = (page - 1) * 12;
                int end = page * 12;
                for(int i = start; i< end; i++)
                {
                    try
                    {
                        list.Add(listAll[i]);                      
                    }
                    catch(ArgumentOutOfRangeException e){ }
                    catch (NullReferenceException e){ }
                }        
            }
            return list;
        }

        //return data - page
        public string getListPage(string page)
        {
            string result = "";
            int p = Int32.Parse(page);
            if(p > 0 || p < pageCount)
            {
                IList<product> list = null;
                list = getProductList(p);
                result = pageProduct(list);
            }          
            return result;
        }
          
        //return a string - list   
        public string pageProduct(IList<product> list)
        {
            string result = "";
            if(list != null)
            {
                int c = (list.Count - 1) / 4;
                for(int i = 0; i< c; i++)
                {
                    result += "<div class=\"header_bottom_right\">";         
                    for(int j = i * 4; j< (i* 4 + 4); j++)
                    {
                        string img = list[j].picture;
                        string name = list[j].name;
                        string cost = list[j].cost;
                        int id = list[j].id;
                        result += "<div class=\"grid_1_of_4 images_1_of_4\">"
                               + "<a href = \"/Home/AddCart?id="+ id +"\" ><img src=\"" + img + "\"/></a>"
                               + "<h2>" + name + "</h2>"
                               + "<div class=\"price-details\">"
                               + "<div class=\"price-number\">"
                               + "<p><span class=\"rupees\">" + cost + " VND</span></p>"
                               + "</div>"
                               + "<div class=\"add-cart\">"
                               + "<h4><a href=\"/Home/AddCart?id="+ id +"\"> Thêm vào giỏ</a></h4>"
                               + "</div>"
                               + "<div class=\"clear\"></div>"
                               + "</div>"
                               + "</div>";           
                    }
                    result += "</div>";
                }

                result += "<div class=\"header_bottom_right\">";
                for(int i = c * 4; i<list.Count; i++)
                {
                    string img = list[i].picture;
                    string name = list[i].name;
                    string cost = list[i].cost;
                    int id = list[i].id;
                    result += "<div class=\"grid_1_of_4 images_1_of_4\">"
                            + "<a href = \"preview.html\"><img src=\"" + img + "\" /></a>"
                            + "<h2>" + name + "</h2>"
                            + "<div class=\"price-details\">"
                            + "<div class=\"price-number\">"
                            + "<p><span class=\"rupees\">" + cost + "</span></p>"
                            + "</div>"
                            + "<div class=\"add-cart\">"
                            + "<h4><a href = \"/Home/AddCart?id=" + id + "\" >Thêm vào giỏ</a></h4>"
                            + "</div>"
                            + "<div class=\"clear\"></div>"
                            + "</div>"
                            + "</div>";
                }
                result += "</div>";
            }
            return result;
        }


//process news
        
        public ActionResult News(int page = 1, int size = 10)
        {
            IEnumerable<news> model = NewsDAO.getListPage(page, size);  
            return View(model);
        }

        public ActionResult ViewNews(string idNews)
        {
            news n = null;
            if (idNews != null)
            {
                n = NewsDAO.getNewsId(Int32.Parse(idNews));
            }
            ViewBag.News = n;
            return View();
        }

//

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }


//feedback


        public ActionResult FeedBack(string content, string idUser)
        {
            var accUser = (account)Session[CONFIG.SESSION_USER];
            if(accUser != null)
            {
                if(content != null && idUser != null)
                {
                    feedback fb = new feedback();
                    fb.content = content;
                    fb.account_id = Int32.Parse(idUser);
                    bool b = FeedBackDAO.addFeedBack(fb);
                }
                return View();
            }
            return RedirectToAction("Login","Home");          
        }

//search

        public ActionResult Search(int page = 1, int size = 20)
        {
            string name = Request["name"];
            if (name != null)
            {
                IEnumerable<product> model = ProductDAO.searchProductByName(name, page, size);
                TempData["listProduct"] = model;
            }
            return RedirectToAction("ViewProduct", "Home", new { type = "all", search = name});
        }
    }


}