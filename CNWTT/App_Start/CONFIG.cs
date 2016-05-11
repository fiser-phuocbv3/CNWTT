using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNWTT.App_Start
{
    public class CONFIG
    {
        public static string PART = "~/img/";
        public static string PART_NEWS = "~/imgNews/";

       // public static int NEWS_COUNT = 20;

        public static string FORMAT_TIME = "dd-MM-yyyy";
        /// <summary>
        /// User
        /// </summary>
        public static string CART    = "CART";
        public static string SESSION_USER = "SESSION_USER";
        public static string SESSION_ADMIN = "SESSION_ADMIN";
        public static string CURRENT = "VND";

        public const string MANUFACTURER = "Hãng sản xuất";

        //laptop config
        public static string LAPTOP_PROCESSOR    = "Processor";
        public static string LAPTOP_RAM          = "RAM";
        public static string LAPTOP_GPU          = "GPU";
        public static string LAPTOP_HARDDISK     = "Ổ cứng";
        public static string LAPTOP_SCREEN       = "Màn hình";
        public static string LAPTOP_OS           = "OS";
        public static string LAPTOP_CONNECT      = "Kết nối";
        public static string LAPTOP_PIN          = "Pin";

        //phone config
        public static string PHONE_PHONETYPE = "Kiểu điện thoại";
        public static string PHONE_SCREEN    = "Màn hình hiển thị";
        public static string PHONE_CAMERA    = "Camera";
        public static string PHONE_MEMORY    = "Bộ nhớ trong";
        public static string PHONE_CHIPSET   = "Chipset";
        public static string PHONE_OS        = "Hệ điều hành";

        //tablet config
        public static string TABLET_SCREEN   = "Màn hình hiển thị";
        public static string TABLET_GPU      = "GPU";
        public static string TABLET_RAM      = "RAM";
        public static string TABLET_MEMORY   = "Bộ nhớ trong";
        public static string TABLET_CAMERA   = "Camera";
        public static string TABLET_CONNECT  = "Kết nối";

        //desktop config
        public static string DESKTOP_PROCESSOR   = "Processor";
        public static string DESKTOP_RAM         = "RAM";
        public static string DESKTOP_GPU         = "GPU";
        public static string DESKTOP_HARDDISK    = "Ổ cứng";
        public static string DESKTOP_SCREEN      = "Màn hình";
        public static string DESKTOP_OS          = "Hệ điều hành";
        public static string DESKTOP_CONNECT     = "Kết nối";
        public static string DESKTOP_PIN         = "Pin";

        //accessories config
        public static string ACCESSORY_MANUFACTURER  = "Hãng sản xuất";
        public static string ACCESSORY_COLOR         = "Mầu";

        //menu product content 
        public static string PRODUCT_CONTENT             = "Danh mục sản phẩm";
        public static string PRODUCT_CONTENT_ALL         = "Tất cả sản phẩm";
        public static string PRODUCT_CONTENT_MOBILE      = "Điện thoại di động";
        public static string PRODUCT_CONTENT_TABLET      = "Máy tính bảng";
        public static string PRODUCT_CONTENT_LAPTOP      = "Máy tính xách tay";
        public static string PRODUCT_CONTENT_DESKTOP     = "Máy tính để bàn";
        public static string PRODUCT_CONTENT_ACCESSORY   = "Phụ kiện";


        /// Admin

        public static string MANAGER_SYSTEM = "Hệ thống";

        public static string CONTROL_BROAD = "Home";

        //view login
        public static string SIGN_IN = "Đăng nhập";
        public static string FORGOT_PASSWORD = "Quên mật khẩu";
        public static string USERNAME = "Username";
        public static string PASSWORD = "Password";
        //public const string List

        //view detail
        public static string PRICE = "Giá";
        public static string PRODUCT_DETAIL = "Chi tiết sản phẩm";
        

        public static string MANUFACTUERE_DETAIL = "Chi tiết hãng sản xuất";


        //View new product
        public static string GENERAL_INFORMATION = "Thông tin chung";
        public static string DETAIL_INFORMATION = "Thông tin chi tiết";

        public static string SELECT_FILE = "Chọn file";
        public static string CHANGE_FILE = "Thay đổi";
        public static string REMOVE_FILE = "Xóa";

    }
}