using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNWTT.Models.DO;
using CNWTT.Models.DAO;
using CNWTT.App_Start;
using PagedList.Mvc;
using CNWTT.Core.Admin;
using System.Collections;

namespace CNWTT.Controllers
{
    public class AdminController : Controller
    {
        private static IList<manufacturer> listManufacturer = null;
        private static int pageCount_productMobile          = 1;
        private static IList<Product> listProduct           = null;
        private static IList<news> listNews = null;
     

       // private static string selectM       = "";
       // private static string selectC       = "";
       // private static string startCost     = "";
       // private static string endCost       = "";
        private static string productType   = "";


        public ActionResult Login()
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {
                if (acc.accountType == "admin")
                {
                    return RedirectToAction("Home", "Admin");
                }            
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove(CONFIG.SESSION_ADMIN);
            return RedirectToAction("Login","Admin");
        }

        public ActionResult CheckLogin(Account acc)
        {
            if (acc != null && acc.username != null && acc.password != null)
            {
                acc.password = AdminCore.EncodeMD5(acc.password);
                account a = AccountDAO.getAccount(acc);
                if(a != null)
                {
                    if (a.accountType.Equals("admin"))
                    {
                        Session[CONFIG.SESSION_ADMIN] = a;
                        return RedirectToAction("Home", "Admin");
                    }                   
                }                      
            }
            return RedirectToAction("Login","Admin");
        }

        // GET: Admin

        public ActionResult Home()
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {
                accountInfor accInfor = AccountInforDAO.getAccountInfoByIdAccount(acc.id);
                ViewBag.accInfor = accInfor;
                ViewBag.account = acc;
                return View();
            }
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult ProductContent(string content)
        {
            listManufacturer = null;
            if(content != null)
            {
                listManufacturer = ManufacturerDAO.getManufacturerListAll();
                

                switch (content)
                {
                    case "mobile":
                        break;
                    case "tablet":
                        break;
                }
                ViewBag.listManufacturer = listManufacturer;
            }
            return View();
        }

//Product
        //view product
        public ActionResult ViewProduct(int page = 1, int size= 20)
        {
            string content = Request["content"];
            string selectManufacturer = Request["selectManufacturer"];
            string selectPrice = Request["selectPrice"];
            string startCost = "";
            string endCost = "";
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
           
            IEnumerable<product> model = null;

            if (acc != null)
            {             
                listManufacturer = ManufacturerDAO.getManufacturerListAll();
                if (selectManufacturer != null && selectPrice != null && content != null)
                {
                    AdminCore.caculatorPrice(selectPrice,out startCost,out endCost);
                    productType = AdminCore.returnProductType(content);
                    // listProduct = ProductDAO.getListProduct_Manufacturer_Cost(selectManufacturer, productType, startCost, endCost);
                    model = ProductDAO.getListProductByManuAndCostAndType(
                        page, 
                        size, 
                        Int32.Parse(selectManufacturer), 
                        productType, 
                        startCost, 
                        endCost );
                }
                ViewBag.listManufacturer = listManufacturer;
                return View(model);
            }
            return RedirectToAction("Login","Admin");
        }

        //new product
        /*
        public ActionResult NewProduct(string content)
        {
            listManufacturer = null;
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {
                if (content != null)
                {
                    listManufacturer = ManufacturerDAO.getManufacturerListAll();
                }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
                ViewBag.listManufacturer = listManufacturer;
                return View();
            }
            return RedirectToAction("Login","Admin");
        }
        */

        public ActionResult AddProduct(Product p)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            string local_img = "";        
            string msg = "";
            bool complete = false;
            listProduct = null;
            listManufacturer = null;
            if (acc != null)
            {
                listManufacturer = ManufacturerDAO.getManufacturerListAll();
                if (p.name != null && p.number != null && p.cost != null && p.manufacturer_id != null)
                {
                    var image = new HttpPostedFile[5];
                    var image_1 = Request.Files["image_1"];
                    var image_2 = Request.Files["image_2"];
                    var image_3 = Request.Files["image_3"];
                    var image_4 = Request.Files["image_4"];
                    var image_5 = Request.Files["image_5"];
                    DateTime time = DateTime.Now;

                    if (image_1.FileName != "")
                    {                        
                        var part = Server.MapPath(CONFIG.PART + image_1.FileName);
                        image_1.SaveAs(part);
                        local_img += image_1.FileName + "|";
                    }
                    if (image_2.FileName != "")
                    {
                        var part = Server.MapPath(CONFIG.PART + image_2.FileName);
                        image_2.SaveAs(part);
                        local_img += image_2.FileName + "|";
                    }
                    if (image_3.FileName != "")
                    {
                        var part = Server.MapPath(CONFIG.PART + image_3.FileName);
                        image_3.SaveAs(part);
                        local_img += image_3.FileName + "|";
                    }
                    if (image_4.FileName != "")
                    {
                        var part = Server.MapPath(CONFIG.PART + image_4.FileName);
                        image_4.SaveAs(part);
                        local_img += image_4.FileName + "|";
                    }
                    if (image_5.FileName != "")
                    {
                        var part = Server.MapPath(CONFIG.PART + image_5.FileName);
                        image_5.SaveAs(part);
                        local_img += image_5.FileName;
                    }

                    p.picture = local_img;
                    p.describle = time.ToString(CONFIG.FORMAT_TIME);
                    string content = Request["content"];

                    switch (content)
                    {
                        case "mobile":
                            p.productType = "1";
                            int idProduct1       = ProductDAO.addProduct(p);
                            string typePhone_m  = Request["typePhone"];
                            string screen_m     = Request["screen"];
                            string camera_m     = Request["camera"];
                            string memory_m     = Request["memory"];
                            string os_m         = Request["os"];
                            string process_m    = Request["process"];

                            if (idProduct1 > 0 && typePhone_m != null && screen_m != null && camera_m != null
                                && memory_m != null && os_m != null && process_m != null)
                            {
                                var phone = new Phone
                                {
                                    PhoneType   = typePhone_m,
                                    Screen      = screen_m,
                                    Camera      = camera_m,
                                    Memory      = memory_m,
                                    OS          = os_m,
                                    Chipset     = process_m,
                                    Product_Id  = idProduct1.ToString()
                                };
                                complete = PhoneDAO.addPhone(phone);
                            }
                            break;
                        case "tablet":
                            p.productType = "2";
                            int idProduct2 = ProductDAO.addProduct(p);
                            string screen_t = Request["screen"];
                            string gpu_t = Request["gpu"];
                            string camera_t = Request["camera"];
                            string ram_t = Request["ram"];
                            string memory_t = Request["memory"];
                            string connect_t = Request["connect"];
                            if (idProduct2 > 0 && screen_t != null && gpu_t != null && camera_t != null &&
                                ram_t != null && memory_t != null && connect_t != null)
                            {
                                var tablet = new Tablet
                                {
                                    Screen = screen_t,
                                    GPU = gpu_t,
                                    RAM = ram_t,
                                    Memory = memory_t,
                                    Camera = camera_t,
                                    Connect = connect_t,
                                    Product_Id = idProduct2.ToString()
                                };
                                complete = TabletDAO.addTablet(tablet);
                            }
                            break;

                        case "laptop":
                            p.productType = "3";
                            int idProduct3 = ProductDAO.addProduct(p);
                            string screen_l = Request["screen"];
                            string gpu_l = Request["gpu"];
                            string hardDisk_l = Request["hardDisk"];
                            string ram_l = Request["ram"];
                            string connect_l = Request["connect"];
                            string processor_l = Request["processor"];
                            string os_l = Request["os"];
                            string pin_l = Request["pin"];

                            if(idProduct3 > 0 && screen_l != null && gpu_l != null && hardDisk_l != null && ram_l != null
                                  && connect_l != null && processor_l != null && os_l != null && pin_l != null)
                            {
                                var laptop = new Laptop
                                {
                                    Processor = processor_l,
                                    RAM = ram_l,
                                    GPU = gpu_l,
                                    HardDisk = hardDisk_l,
                                    Screen = screen_l,
                                    OS = os_l,
                                    Connect = connect_l,
                                    Pin = pin_l,
                                    Product_Id = idProduct3.ToString()
                                };
                                complete = LaptopDAO.addLaptop(laptop);
                            }
                            break;
                        case "desktop":
                            p.productType = "4";
                            int idProduct4 = ProductDAO.addProduct(p);
                            string screen_d     = Request["screen"];
                            string gpu_d        = Request["gpu"];
                            string hardDisk_d   = Request["hardDisk"];
                            string ram_d        = Request["ram"];
                            string connect_d    = Request["connect"];
                            string processor_d  = Request["processor"];
                            string os_d         = Request["os"];
                            string pin_d        = Request["pin"];

                            if(idProduct4 > 0 && screen_d != null && gpu_d != null && hardDisk_d != null && ram_d != null
                                && connect_d != null && processor_d != null && os_d != null && pin_d !=null)
                            {
                                var desktop = new Desktop
                                {
                                    Processor = processor_d,
                                    RAM = ram_d,
                                    GPU = gpu_d,
                                    HardDisk = hardDisk_d,
                                    Screen = screen_d,
                                    OS = os_d,
                                    Connect = connect_d,
                                    Pin = pin_d,
                                    Product_Id = idProduct4.ToString()
                                };
                                complete = DesktopDAO.addDesktop(desktop);
                            }
                            break;
                        case "accessories":
                            p.productType = "5";
                            int idProduct5 = ProductDAO.addProduct(p);
                            string color_a = Request["color"];
                            if(idProduct5 > 0 && color_a != null)
                            {
                                var accessories = new Accessories
                                {
                                    Color = color_a,
                                    Manufacturer = ManufacturerDAO.getManufacturerId(Int32.Parse(p.manufacturer_id)).name,
                                    Product_Id = idProduct5.ToString()
                                };
                                complete = AccessoryDAO.addAccessory(accessories);
                            }
                            break;
                    }
                    msg = complete ? "Thêm sản phẩm thành công" : "Thêm sản phẩm không thành công";
                }
                ViewBag.msg = msg;
                ViewBag.listManufacturer = listManufacturer;
                return View("NewProduct");
            }
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult DeleteProduct(string idProduct)
        {
            string content = Request["content"];
            string selectManufacturer = Request["selectManufacturer"];
            string selectPrice = Request["selectPrice"];
            string page = Request["page"];

            if(page == null)
            {
                page = "1";
            }
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            bool b_1 = false, b_2 = false, b_3 = false, b_4 = false, b_5 = false, complete = false;

            if(acc != null)
            {
                listManufacturer = ManufacturerDAO.getManufacturerListAll();
                if (content != null && idProduct != null)
                {
                    switch (content)
                    {
                        case "mobile":
                            b_1 = PhoneDAO.deletePhone_idProduct(Int32.Parse(idProduct));                   
                            break;
                        case "tablet":
                            b_2 = TabletDAO.deleteTablet_idProduct(Int32.Parse(idProduct));
                            break;
                        case "laptop":
                            b_3 = LaptopDAO.deleteLaptop_idProduct(Int32.Parse(idProduct));
                            break;
                        case "desktop":
                            b_4 = DesktopDAO.deleteDesktop_idProduct(Int32.Parse(idProduct));
                            break;
                        case "accessories":
                            b_5 = AccessoryDAO.deleteAccessories_idProduct(Int32.Parse(idProduct));
                            break;                       
                    }
                    if (b_1 || b_2 || b_3 || b_4 || b_5)
                    {
                        complete = ProductDAO.deleteProduct(Int32.Parse(idProduct));                       
                    }
                }
                return RedirectToAction("ViewProduct","Admin",new { content, selectManufacturer, selectPrice, page});
            }
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult EditProduct(Product p)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {
                string content = Request["content"];
                string idProduct = Request["idProduct"];

                string msg = "";
                bool complete = false;
                
                listManufacturer = ManufacturerDAO.getManufacturerListAll();
                if(content != null && idProduct != null && p.name != null && p.cost != null &&
                    p.number != null && p.manufacturer_id != null)
                {
                    var productOld = ProductDAO.getProductId(idProduct);
                    var productCurrent = new product();
                    productCurrent.name = p.name.ToString();
                    productCurrent.cost = p.cost.ToString();
                    productCurrent.number = p.number.ToString();
                    productCurrent.describle = productOld.describle;
                    productCurrent.productType = productOld.productType;
                    productCurrent.manufacturer_id = Int32.Parse(p.manufacturer_id);
                    
                    var image = new HttpPostedFileBase[5];
                    image[0] = Request.Files["image_1"];
                    image[1] = Request.Files["image_2"];
                    image[2] = Request.Files["image_3"];
                    image[3] = Request.Files["image_4"];
                    image[4] = Request.Files["image_5"];
                    productCurrent.picture = productOld.picture;
                    bool b = ProductDAO.EditProduct(Int32.Parse(idProduct), productCurrent);
                    switch (content)
                    {
                        case "mobile":                       
                            string typePhone_m = Request["typePhone"];
                            string screen_m = Request["screen"];
                            string camera_m = Request["camera"];
                            string memory_m = Request["memory"];
                            string os_m = Request["os"];
                            string process_m = Request["process"];

                            if (b && typePhone_m != null && screen_m != null && camera_m != null
                                && memory_m != null && os_m != null && process_m != null)
                            {
                                var phone1 = PhoneDAO.getPhoneProductId(Int32.Parse(idProduct));
                                var phoneCurrent = new phone();
                                phoneCurrent.phoneType = (typePhone_m.ToString() != "") ? typePhone_m.ToString() : phone1.phoneType;
                                phoneCurrent.screen = (screen_m.ToString() != "") ? screen_m.ToString() : phone1.screen;
                                phoneCurrent.camera = (camera_m.ToString() != "") ? camera_m.ToString() : phone1.camera;
                                phoneCurrent.memory = (memory_m.ToString() != "") ? memory_m.ToString() : phone1.memory;
                                phoneCurrent.os = (os_m.ToString() != "") ? os_m.ToString() : phone1.os;
                                phoneCurrent.chipset = (process_m.ToString() != "") ? process_m.ToString() : phone1.chipset;
                                complete = PhoneDAO.editPhone_idProduct(Int32.Parse(idProduct), phoneCurrent);
                            }
                            break;
                        case "tablet":
                            string screen_t = Request["screen"];
                            string gpu_t = Request["gpu"];
                            string camera_t = Request["camera"];
                            string ram_t = Request["ram"];
                            string memory_t = Request["memory"];
                            string connect_t = Request["connect"];

                            if (b && screen_t != null && gpu_t != null && camera_t != null &&
                                ram_t != null && memory_t != null && connect_t != null)
                            {
                                var tablet1 = TabletDAO.getTabletProductId(Int32.Parse(idProduct));
                                var tabletCurrent = new tablet();
                                tabletCurrent.screen    = (screen_t.ToString() != "") ? screen_t.ToString() : tablet1.screen;
                                tabletCurrent.gpu       = (gpu_t.ToString() != "") ? gpu_t.ToString() : tablet1.gpu;
                                tabletCurrent.camera    = (camera_t.ToString() != "") ? camera_t.ToString() : tablet1.gpu;
                                tabletCurrent.ram       = (ram_t.ToString() != "") ? ram_t.ToString() : tablet1.ram;
                                tabletCurrent.memory    = (memory_t.ToString() != "") ? memory_t.ToString() : tablet1.memory;
                                tabletCurrent.connect   = (connect_t.ToString() != "") ? connect_t.ToString() : tablet1.connect;
                                complete = TabletDAO.editTablet_idProduct(Int32.Parse(idProduct), tabletCurrent);
                            }
                            break;
                        case "laptop":

                            string screen_l = Request["screen"];
                            string gpu_l = Request["gpu"];
                            string hardDisk_l = Request["hardDisk"];
                            string ram_l = Request["ram"];
                            string connect_l = Request["connect"];
                            string processor_l = Request["processor"];
                            string os_l = Request["os"];
                            string pin_l = Request["pin"];

                            if (b && screen_l != null && gpu_l != null && hardDisk_l != null && ram_l != null
                                  && connect_l != null && processor_l != null && os_l != null && pin_l != null)
                            {
                                var laptop1 = LaptopDAO.getLaptopProductId(Int32.Parse(idProduct));
                                var laptopCurrent = new laptop();
                                laptopCurrent.screen = (screen_l.ToString() != "") ? screen_l.ToString() : laptop1.screen;
                                laptopCurrent.gpu = (gpu_l.ToString() != "") ? gpu_l.ToString() : laptop1.gpu;
                                laptopCurrent.hardDisk = (hardDisk_l.ToString() != "") ? hardDisk_l.ToString() : laptop1.hardDisk;
                                laptopCurrent.ram = (ram_l.ToString() != "") ? ram_l.ToString() : laptop1.ram;
                                laptopCurrent.connect = (connect_l.ToString() != "") ? connect_l.ToString() : laptop1.connect;
                                laptopCurrent.processor = (processor_l.ToString() != "") ? processor_l.ToString() : laptop1.processor;
                                laptopCurrent.os = (os_l.ToString() != "") ? os_l.ToString() : laptop1.os;
                                laptopCurrent.pin = (pin_l.ToString() != "") ? pin_l.ToString() : laptop1.pin;
                                complete = LaptopDAO.editLaptop_idProduct(Int32.Parse(idProduct), laptopCurrent);
                            }
                            break;
                        case "desktop":

                            string screen_d = Request["screen"];
                            string gpu_d = Request["gpu"];
                            string hardDisk_d = Request["hardDisk"];
                            string ram_d = Request["ram"];
                            string connect_d = Request["connect"];
                            string processor_d = Request["processor"];
                            string os_d = Request["os"];
                            string pin_d = Request["pin"];

                            if (b && screen_d != null && gpu_d != null && hardDisk_d != null && ram_d != null
                                && connect_d != null && processor_d != null && os_d != null && pin_d != null)
                            {
                                var desktop1 = DesktopDAO.getDesktopProductId(Int32.Parse(idProduct));
                                var desktopCurrent = new desktop();
                                desktopCurrent.screen = (screen_d.ToString() != "") ? screen_d.ToString() : desktop1.screen;
                                desktopCurrent.gpu = (gpu_d.ToString() != "") ? gpu_d.ToString() : desktop1.gpu;
                                desktopCurrent.hardDisk = (hardDisk_d.ToString() != "") ? hardDisk_d.ToString() : desktop1.hardDisk;
                                desktopCurrent.ram = (ram_d.ToString() != "") ? ram_d.ToString() : desktop1.ram;
                                desktopCurrent.connect = (connect_d.ToString() != "") ? connect_d.ToString() : desktop1.connect;
                                desktopCurrent.processor = (processor_d.ToString() != "") ? processor_d.ToString() : desktop1.processor;
                                desktopCurrent.os = (os_d.ToString() != "") ? os_d.ToString() : desktop1.os;
                                desktopCurrent.pin = (pin_d.ToString() != "") ? pin_d.ToString() : desktop1.pin;
                                complete = DesktopDAO.editDesktop_idProduct(Int32.Parse(idProduct), desktopCurrent);
                            }
                            break;
                        case "accessories":

                            string color_a = Request["color"];
                            if (b && color_a != null)
                            {
                                var accessory1 = AccessoryDAO.getAccessoryProductId(Int32.Parse(idProduct));
                                var accessoryCurrent = new accessory();
                                accessoryCurrent.color = (color_a.ToString() != "") ? color_a.ToString() : accessory1.color;
                                accessoryCurrent.manufacturer = ManufacturerDAO.getManufacturerId(Int32.Parse(p.manufacturer_id)).name;
                                complete = AccessoryDAO.editAccessory_idProduct(Int32.Parse(idProduct), accessoryCurrent);
                            }
                            break;
                    }
                    msg = complete ? "Chỉnh sửa thành công" : "Chỉnh sửa không thành công";
                    ViewBag.msg = msg;
                }

                else if (content != null && idProduct != null)
                {
                    var product = ProductDAO.getProductId(idProduct);
                    switch (content)
                    {
                        case "mobile":
                            var phone = PhoneDAO.getPhoneProductId(Int32.Parse(idProduct));
                            ViewBag.phone = phone;
                            break;
                        case "tablet":
                            var tablet = TabletDAO.getTabletProductId(Int32.Parse(idProduct));
                            ViewBag.tablet = tablet;
                            break;
                        case "laptop":
                            var laptop = LaptopDAO.getLaptopProductId(Int32.Parse(idProduct));
                            ViewBag.laptop = laptop;
                            break;
                        case "desktop":
                            var desktop = DesktopDAO.getDesktopProductId(Int32.Parse(idProduct));
                            ViewBag.desktop = desktop;
                            break;
                        case "accessories":
                            var accessories = AccessoryDAO.getAccessoryProductId(Int32.Parse(idProduct));
                            ViewBag.accessories = accessories;
                            break;
                    }
                    ViewBag.product = product;
                }
                ViewBag.listManufacturer = listManufacturer;
                return View("EditProduct");
            }
            return RedirectToAction("Login", "Admin");
        }


//manufacturer

        public string checkManufacturer(string name)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {
                if(name != null)
                {
                    manufacturer manu = ManufacturerDAO.getManufacturerByName(name);
                    if (manu != null) return "false";
                }           
            } 
            return "true";
        }


        //View manufacturer
        public ActionResult Manufacturer(int page = 1, int size = 20)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {        
                IEnumerable<manufacturer> model = ManufacturerDAO.getListManufacturer(page, size);              
                return View(model);
            }
            return RedirectToAction("Login","Admin");
        }

        public ActionResult NewManufacturer(string idM)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {  
                if(idM != null)
                {

                }
                return View("NewManufacturer");
            }
            return RedirectToAction("Login","Admin");
        }

        //add and edit manufacturer
        public ActionResult AEManufacturer(manufacturer m)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            string idM = Request["idM"];
            string msg = "";
            manufacturer manu = null;
            if(acc != null)
            {
                if (m != null && idM != null)
                {
                    m.id = Int32.Parse(idM);
                    bool b = ManufacturerDAO.EditManufacturer(m);
                    msg = (b) ? "Chỉnh sửa thành công" : "Chỉnh sửa không thành công";
                }
                else
                {
                    bool b = ManufacturerDAO.AddManufacturer(m);
                    msg = (b) ? "Thêm hãng sản xuất thành công" : "Thêm hãng sản xuất không thành công";
                }
                ViewBag.msg = msg;
                return View("NewManufacturer");
            }
            return RedirectToAction("Login","Admin");
        }

        //view edit manufacturer
        public ActionResult EditManufacturer(string idM)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            manufacturer m = null;
            if(acc != null)
            {
                if(idM != null)
                {
                    m = ManufacturerDAO.getManufacturerId(Int32.Parse(idM));
                }
                ViewBag.Manufacturer = m;
                return View("NewManufacturer");
            }
            return RedirectToAction("Login", "Admin");
        }

//news

        //list news
        public ActionResult News(int page = 1, int pageSize = 20)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {
                var model = NewsDAO.getListPage(page, pageSize);
                return View(model);
            }
            return RedirectToAction("Login", "Admin");
        }

        //add news
        public ActionResult AddNews(news news)
        {
            string msg = "";
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {                         
                if (news.title != null && news.describe != null && news.content != null)
                {
                    var image_1 = Request.Files["image_1"];
                    if (image_1.FileName != "")
                    {
                        var part = Server.MapPath(CONFIG.PART_NEWS + image_1.FileName);
                        image_1.SaveAs(part);              
                    }
                    news.picture = (image_1.FileName != "") ? image_1.FileName : "";
                    int idN = NewsDAO.addNews(news);
                    msg = (idN > 0) ? "Thêm bài viết thành công" : "Thêm bài viết không thành công";
                }
                else
                {
                    msg = "";
                }
                ViewBag.msg = msg;
                return View("ViewNews");
            }
            return RedirectToAction("Login", "Admin");
        }

        //edit news
        public ActionResult ViewNews(news news)
        {
            
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {
                string idNews = Request["idNews"];
                string msg = "";
                news n = null;
                if (idNews != null)
                {
                    
                    if (news.title != null && news.describe != null && news.content != null)
                    {                   
                        var image_1 = Request.Files["image_1"];
                        if (image_1.FileName != "")
                        {
                            var part = Server.MapPath(CONFIG.PART_NEWS + image_1.FileName);
                            image_1.SaveAs(part);
                        }
                        news n_1 = NewsDAO.getNewsId(Int32.Parse(idNews));
                        news.picture = (image_1.FileName != "") ? image_1.FileName : n_1.picture;
                        news.id = n_1.id;
                        bool b = NewsDAO.editNews(news);
                        msg = (b)?"Chỉnh Sửa Thành Công":"Chỉnh Sửa Không Thành Công";
                    }
                    else
                    {
                        n = NewsDAO.getNewsId(Int32.Parse(idNews));
                    }
                }
                ViewBag.msg = msg;
                ViewBag.news = n;     
                return View();
            }
            return RedirectToAction("Login","Admin");
        }

        //delete news
        public ActionResult DeleteNews(string idNews)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if (acc != null)
            {
                if(idNews != null)
                {
                    bool b = NewsDAO.deleteNews(Int32.Parse(idNews));        
                }
                return RedirectToAction("News","Admin");
            }
            return RedirectToAction("Login", "Admin");
        }

//order
    
        //list order
        public ActionResult ViewBill(int page = 1, int size = 20)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {
                string startDate = Request["startDate"];
                string endDate = Request["endDate"];
                IEnumerable<bill> model = null;
                IList<accountInfor> listAccountInfor = null;
                IList<account> listAccount = null;
                if(startDate != null && endDate != null)
                {
                    listAccount = AccountDAO.getListAll();
                    listAccountInfor = AccountInforDAO.getListAll();
                    model = BillDAO.getListProductByCreateDate(startDate, endDate, page, size);
                }
                ViewBag.listAccount = listAccount;
                ViewBag.listAccountInfor = listAccountInfor;
                return View(model);
            }
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult ViewBillDetail(int page = 1, int size = 20)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {
                string idBill = Request["idBill"];
                bill bill = null;
                account account = null;
                accountInfor accInfor = null;
                IList<product> listProduct = null;
                IList<billDetail> listBillDetail = null;
                Hashtable htProduct = new Hashtable();           
                if(idBill != null)
                {
                    bill = BillDAO.getBillById(Int32.Parse(idBill));
                    account = AccountDAO.getAccountById(Int32.Parse(bill.account_id.ToString()));
                    accInfor = AccountInforDAO.getAccountInfoByIdAccount(Int32.Parse(bill.account_id.ToString()));
                    listBillDetail = BillDetailDAO.getListBillDetailByIdBill(Int32.Parse(idBill));
                    listProduct = ProductDAO.getListProductByListBillDetail(listBillDetail);
                }
                if(listProduct != null && listProduct.Count > 0)
                {
                    foreach(var item in listProduct)
                    {
                        htProduct.Add(item.id, item.name);
                    }
                }
                ViewBag.htProduct = htProduct;
                ViewBag.account = account;
                ViewBag.accInfor = accInfor;
                ViewBag.bill = bill;
                ViewBag.listBillDetail = listBillDetail;
                return View();
            }
            return RedirectToAction("Login","Admin");
        }


//contact

        public ActionResult ViewFeedBack(int page = 1, int size = 20)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {
                IEnumerable<feedback> model = FeedBackDAO.getListFeedBack(page, size);
                IList<account> listAccount = AccountDAO.getAllAccountIsUser();
                Hashtable ht = new Hashtable();
                foreach (var item in listAccount)
                {
                    ht.Add(item.id.ToString(), item.username);
                }               
                ViewBag.hashtable = ht;
                return View(model); 
            }
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult DeleteFeedBack(string p,string idFeedBack)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {
                if(p != null && idFeedBack != null)
                {
                    bool b = FeedBackDAO.deleteFeedBack(Int32.Parse(idFeedBack));
                    return RedirectToAction("ViewFeedBack", "Admin", new { page = p });
                }
                return RedirectToAction("ViewFeedBack", "Admin");
            }
            return RedirectToAction("Login","Admin");
        }


//account

        public ActionResult ViewAccount(int page = 1, int size = 20)
        {
            var account = (account)Session[CONFIG.SESSION_ADMIN];
            if(account != null)
            {
                Hashtable htAccInfor = new Hashtable();
                IEnumerable<account> model = AccountDAO.getListAccountIsUser(page, size);
                IList<accountInfor> list = AccountInforDAO.getListAll();
                foreach(var item in list)
                {
                    htAccInfor.Add(item.account_id, item.name);
                }
                ViewBag.htAccInfor = htAccInfor;
                ViewBag.account = account;
                return View(model);
            }
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult AddAccount(accountInfor accInfor)
        {
            //account acc = null;
            string msg = "";
            var account = (account)Session[CONFIG.SESSION_ADMIN];
            if (account != null)
            {
                string userName = Request["userName"];
                string password = Request["password"];
                string confirmPassword = Request["confirmPassword"];
                string accountType = Request["accountType"];

                if (accInfor.name != null && accInfor.address != null && accInfor.identify != null 
                    &&accInfor.phone != null && accInfor.email != null && userName != null
                    && password != null && accountType != null && password.Equals(confirmPassword))
                {
                    var acc = AccountDAO.getAccountByName(userName);
                    if(acc == null)
                    {
                        var a = new account();
                        a.username = userName;
                        a.password = AdminCore.EncodeMD5(password);
                        a.accountType = accountType;
                        int idAccount = AccountDAO.addAccount(a);
                        if (idAccount > 0)
                        {
                            accInfor.account_id = idAccount;
                            int idAccInfo = AccountInforDAO.addAccountInfor(accInfor);
                            msg = (idAccInfo > 0) ? "Thêm thành công" : "Thêm không thành công";
                        }
                        else
                        {
                            msg = "Thêm không thành công";
                        }
                    }
                    else
                    {
                        msg = "Tên này đã được dùng! Bạn vui lòng chọn tên khác";
                    }
                }
                ViewBag.msg = msg;
                return View();
            }
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult AccountDetail(string idAccount)
        {
            var account = (account)Session[CONFIG.SESSION_ADMIN];
            if(account != null)
            {
                var acc = AccountDAO.getAccountById(Int32.Parse(idAccount));
                var accInfor = AccountInforDAO.getAccountInfoByIdAccount(Int32.Parse(idAccount));
                ViewBag.acc = acc;
                ViewBag.accInfor = accInfor;
                return View("AddAccount");
            }
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult DeleteAccount(string idAccount, string page)
        {
            var account = (account)Session[CONFIG.SESSION_ADMIN];
            if(account != null)
            {
                if(idAccount != null)
                {
                    bool b_1 = AccountInforDAO.deleteAccountInfoByIdAccount(Int32.Parse(idAccount));
                    if (b_1)
                    {
                        bool b_2 = AccountDAO.deleteAccount(Int32.Parse(idAccount));
                    }
                }
                return RedirectToAction("ViewAccount", "Admin", new { page = page });
            }
            return RedirectToAction("Login","Admin");
        }

        public string checkUserName(string userName)
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {
                if(userName != null)
                {
                    account account = AccountDAO.getAccountByName(userName);
                    if (account != null)
                        return "false";
                }
            }
            return "true";
        }

        //account admin
        public ActionResult EditAccount()
        {
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if(acc != null)
            {
                ViewBag.type = "editAccount";
                return View();
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmNewPassword)
        {           
            var acc = (account)Session[CONFIG.SESSION_ADMIN];
            if (acc != null)
            {
                string msg = "ko vao";
                //bool b = AccountDAO.testPasswordOfAccount(acc, AdminCore.EncodeMD5(oldPassword.ToString()));
               // if( oldPassword != null && newPassword != null 
               //     && confirmNewPassword != null 
               //     && (newPassword.ToString().Equals(confirmNewPassword.ToString())))
                //{
                    //bool b1 = AccountDAO.changePasswordOfAccount(acc, AdminCore.EncodeMD5(newPassword.ToString()));
                  //  msg = b1 ? "Thay đổi mât khẩu thành công" : "Thay đổi mật khẩu không thành công";
               // }
                ViewBag.type = "changePassword";
                ViewBag.msg = msg;
                return View("EditAccount");
            }
            return RedirectToAction("Login", "Home");
        }
    }
}