using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using real_estate.Models;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;


namespace real_estate.Controllers
{
    public class HomeController : Controller
    {
        //sql
        //string string_connetion = ConfigurationManager.ConnectionStrings["real_estate"].ConnectionString;
        //mysql
        //string string_connetion = ConfigurationManager.ConnectionStrings["real_estate_mysql"].ConnectionString;
        //ora
        string string_connetion = ConfigurationManager.ConnectionStrings["real_estate_ora"].ConnectionString;
        public JsonResult AllProperties()
        {
           
            string jsonData = sql.Convert.Table.ToJson(string_connetion,
                "select * from V_AllProperties",Case_char.lower);
            //System.Diagnostics.Debug.WriteLine(jsonData);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AllProperties2()
        {

            string jsonData = sql.Convert.Table.ToJson(string_connetion,
                "select * from V_AllProperties", Case_char.lower);
            //System.Diagnostics.Debug.WriteLine(jsonData);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult TopOneroperties()
        {
            //sql
            // string jsonData = sql.Convert.Table.ToJson(string_connetion,
            //     "exec SP_TopOneProperties_by_currency 'ج.م'" , Case_char.lower);

            //mysql
            //string jsonData = mysql.Convert.Table.ToJson(string_connetion,
            //   "call SP_TopOneProperties_by_currency('ج.م')", Case_char.lower);

            //ora
            string[] _p_valuse = { "p_currency" };
            OracleDbType[] _oracleDbTypes = { OracleDbType.NVarchar2 };
            object[] _valuse = { "ج.م  " };

            string jsonData = ora.Convert.SP.ToJson(string_connetion,
               "SP_TOPONEPROPERTIES_BY_C", _p_valuse, _oracleDbTypes, _valuse, Case_char.lower);
            //System.Diagnostics.Debug.WriteLine(jsonData);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult auto_search(string search)
       {
            //sql/mysql
            //var search_list = ora.Convert.Table.ToList<REALESTATEModel_ora.VALLPROPERTY>(string_connetion,
            //    "select * from V_ALLPROPERTIES where city like N'%" + search+"%'");

            //ora
            var search_list = ora.Convert.Table.ToList<REALESTATEModel_ora.VALLPROPERTY>(string_connetion,
                "select * from V_ALLPROPERTIES where \"city\" like N'%" + search+"%'");

            List<string> data_search = new List<string>();
            foreach(var data in search_list.Result.Item1)
            {
                data_search.Add(data.City);
            }

             HashSet<string> search_set = new HashSet<string>(data_search);
            List<string> data_search_no_duplicates = search_set.ToList();

           // System.Diagnostics.Debug.WriteLine(data_search_no_duplicates);

            return Json(data_search_no_duplicates, JsonRequestBehavior.AllowGet);
        }
        static string company_data;

        public JsonResult company()
        {
           // System.Diagnostics.Debug.WriteLine(company_data);
            return Json(company_data, JsonRequestBehavior.AllowGet);
        }
        List<string> search_list = new List<string>();
        public ActionResult Index()
        {
            Random random = new Random();
            //sql
            // int RowCount = sql.DataBase.GetRowCount(string_connetion, "V_AllProperties");
            //var Data1 = sql.Convert.Table.ToDictionary(string_connetion,
            //   "exec SP_Get_Properties_by_id @currency = 'ج.م', @Propertie_id = " + random.Next(1, RowCount));
            //var Data2 = sql.Convert.Table.ToDictionary(string_connetion,
            //   "exec SP_Get_Properties_by_id @currency = 'ج.م', @Propertie_id = " + random.Next(1, RowCount));
            //var Data3 = sql.Convert.Table.ToDictionary(string_connetion,
            //    "exec SP_Get_Properties_by_id @currency = 'ج.م', @Propertie_id = " + random.Next(1, RowCount));

            //mysql
            //int RowCount = mysql.DataBase.GetRowCount(string_connetion, "V_AllProperties");
            //var Data1 = mysql.Convert.Table.ToDictionary(string_connetion,
            //  "call SP_Get_Properties_by_id (" + random.Next(1, RowCount) + ",'ج.م')");
            //var Data2 = mysql.Convert.Table.ToDictionary(string_connetion,
            //   "call SP_Get_Properties_by_id (" + random.Next(1,RowCount) + ",'ج.م')");
            //var Data3 = mysql.Convert.Table.ToDictionary(string_connetion,
            //    "call SP_Get_Properties_by_id (" + random.Next(1, RowCount) + ",'ج.م')");

            //ora
            int RowCount = ora.DataBase.GetRowCount(string_connetion, "V_ALLPROPERTIES");

            string[] _p_valuse1 = { "p_currency", "Propertie_id" };
            OracleDbType[] _oracleDbTypes1 = { OracleDbType.NVarchar2 ,OracleDbType.Int32};
            object[] _valuse1 = { "ج.م  ", random.Next(1, RowCount) };
            var Data1 = ora.Convert.SP.ToDictionary(string_connetion,
               "SP_GET_PROPERTIES_BY_ID", _p_valuse1, _oracleDbTypes1, _valuse1);

            string[] _p_valuse2 = { "p_currency", "Propertie_id" };
            OracleDbType[] _oracleDbTypes2 = { OracleDbType.NVarchar2, OracleDbType.Int32 };
            object[] _valuse2 = { "ج.م  ", random.Next(1, RowCount) };
            var Data2 = ora.Convert.SP.ToDictionary(string_connetion,
               "SP_GET_PROPERTIES_BY_ID", _p_valuse2, _oracleDbTypes2, _valuse2);

            string[] _p_valuse3 = { "p_currency", "Propertie_id" };
            OracleDbType[] _oracleDbTypes3 = { OracleDbType.NVarchar2, OracleDbType.Int32 };
            object[] _valuse3 = { "ج.م  ", random.Next(1, RowCount) };
            var Data3 = ora.Convert.SP.ToDictionary(string_connetion,
               "SP_GET_PROPERTIES_BY_ID", _p_valuse3, _oracleDbTypes3, _valuse3);

            ViewBag.slide_category_name1 = Data1.Result.Item1["category_name"];
            ViewBag.slide_city1 = Data1.Result.Item1["city"];
            ViewBag.slide_address1 = Data1.Result.Item1["address"];
            ViewBag.slide_image1 = Data1.Result.Item1["image_path"];
            ViewBag.slide_category_name2 = Data2.Result.Item1["category_name"];
            ViewBag.slide_city2 = Data2.Result.Item1["city"];
            ViewBag.slide_address2 = Data2.Result.Item1["address"];
            ViewBag.slide_image2 = Data2.Result.Item1["image_path"];
            ViewBag.slide_category_name3 = Data3.Result.Item1["category_name"];
            ViewBag.slide_city3 = Data3.Result.Item1["city"];
            ViewBag.slide_address3 = Data3.Result.Item1["address"];
            ViewBag.slide_image3 = Data3.Result.Item1["image_path"];
            //------------------------
            ViewBag.location = "أدخل الموقع";
            ViewBag.Bedrooms_Bathrooms = "عدد الغرف و الحمامات";
            ViewBag.Bedrooms = "عدد الغرف";
            ViewBag.Bathrooms = "عدد الحمامات";
            ViewBag.g0 = "الكل";
            ViewBag.g1 = "شقة";
            ViewBag.g2 = "فيلا";
            ViewBag.g3 = "بنتهاوس";
            ViewBag.g4 = "توين هاوس";
            ViewBag.g5 = "دوبلكس";
            ViewBag.g6 = "شاليه";
            ViewBag.g7 = "كمبوند";
            ViewBag.t2g0 = "الكل";
            ViewBag.t2g1 = "مكتب";
            ViewBag.t2g2 = "مطعم و كافيه";
            ViewBag.t2g3 = "مجمع تجاري";
            ViewBag.t2g4 = "محلات تجارية";
            ViewBag.t2g5 = "مستودع";
            ViewBag.t2g6 = "عيادة";
            ViewBag.t2g7 = "مصنع";
            ViewBag.t2g8 = "جراج";
            ViewBag.type1 = "سكني";
            ViewBag.type2 = "تجاري";
            ViewBag.pay1 = "للبيع";
            ViewBag.pay2 = "للايجار";
            ViewBag.area_d = "المساحة (متر مربع)";
            ViewBag.area_min = "الحد الأدنى متر مربع";
            ViewBag.area_max = "الحد الأعلى متر مربع";
            ViewBag.area_min2 = "متر مربع الحد الأدنى ";
            ViewBag.area_max2 = "متر مربع الحد الأعلى";
            ViewBag.price_min = "الحد الأدنى";
            ViewBag.price_max = "الحد الأعلى";
            ViewBag.price_d = "السعر";
            ViewBag.search = "ابحث";
            //----------------------
            ViewBag.highest = "احصل على ";
            ViewBag.highest2 = "على أعلى مستوى";
            ViewBag.FEATURED = "متميز";
            ViewBag.title = "أفضل ملكية";
            ViewBag.about = "وصف العقار";
            ViewBag.Total_Space = "إجمالي المساحة";
            ViewBag.Contract = "العقد جاهز";
            ViewBag.Contract2 = "عقد";
            ViewBag.Control = "تحت السيطرة";
            ViewBag.Safety = "أمان";
            ViewBag.Price = "السعر";
            //---------------------
            ViewBag.View_video = "احصل على رؤية أقرب وشعور مختلف";
            ViewBag.Video = "رؤيتنا";
            //----------------------
            ViewBag.BEST_DEAL = "أفضل صفقة";
            ViewBag.Find_BEST_DEAL = "اعثر على أفضل صفقة لك الآن";
            ViewBag.Appartment = "شقة";
            ViewBag.Villa = "فيلا";
            ViewBag.Penthouse = "بنتهاوس";
            //-----------------------
            ViewBag.PROPERTIES = "العقارات";
            ViewBag.like = "نحن نقدم أفضل العقارات التي تريدها";
            ViewBag.book = "حجز موعد زياره";
            ViewBag.view = "عرض";
            //-----------------------
            ViewBag.Contact_Us = "اتصل بنا";
            ViewBag.agents = "تواصل مع وكلائنا";
            ViewBag.Phone_Number = "رقم الهاتف";
            ViewBag.Business_Email = "البريد الالكتروني";
            ViewBag.Full_Name = "الاسم الكامل";
            ViewBag.Subject = "عنوان الرسالة";
            ViewBag.Message = "الرسالة";
            ViewBag.submit = "ارسال الرسالة";
            //-----------------------------
            ViewBag.currency = "ج.م";
            ViewBag.m = "متر مربع";
            ViewBag.Bedrooms_card = "غرف نوم";
            ViewBag.Bathrooms_card = "الحمامات";
            ViewBag.Area_card = "المساحة";
            ViewBag.Floor_card = "الطابق";
            ViewBag.Parking_card = "موقف سيارات";
            //---------------------------
            ViewBag.Total_Flat_Space = "إجمالي المساحة";
            ViewBag.Parking_Available = "مواقف السيارات متاحة";
            ViewBag.Payment_Process = "طريقه الدفع";
            //------------------------------
            ViewBag.t1 = "سكني";
            ViewBag.t2 = "تجاري";


            string jsonData = ora.Convert.Table.ToJson(string_connetion,
                "select * from \"company_info\"",Case_char.normal);

            company_data = jsonData;

            return View();
        }

        static string search_jsonData;
        public JsonResult search()
        {
            
            //System.Diagnostics.Debug.WriteLine(search_jsonData);
            return Json(search_jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult properties(REALESTATEModel_ora.SPALLPROPERTIESBYCURRENCYResult SP_Search, search_data search_Data)
        {
            string min_price = search_Data.min_price;
            string max_price = search_Data.max_price;
            string Bedrooms = Convert.ToString(SP_Search.Bedrooms);
            string Bathrooms = Convert.ToString(SP_Search.Bathrooms);
            string min_area = Convert.ToString(search_Data.min_area);
            string max_area = Convert.ToString(search_Data.max_area);
            string category_name = SP_Search.CategoryName;

            if (min_price == "0")
            {
                 min_price = "null";
            }
            if(max_price == "0")
            {
                max_price = "null";
            }
            if(Bedrooms == "0")
            {
                Bedrooms = "null";
            }
            if(Bathrooms == "0")
            {
                Bathrooms = "null";
            }
            if(min_area == "0")
            {
                min_area = "null";
            }
            if(max_area == "0")
            {
                max_area = "null";
            }
            if(category_name == "الكل")
            {
                category_name = "null";
            }

            //sql
            //string jsonData = sql.Convert.Table.ToJson(string_connetion,
            // "exec SP_search @currency = N'ج.م',  @properties_type = N'"+SP_Search.properties_type + "' , @category_name = N'" + category_name +
            // "' , @city = N'" + SP_Search.city + "' , @Bedrooms = " + Bedrooms + " , @Bathrooms = " + Bathrooms +
            // " , @min_price = " + min_price + " , @max_price = " + max_price +
            // " , @min_area = " + min_area + " , @max_area = " + max_area + " , @Display_type = N'" + SP_Search.Display_type +"'",Case_char.lower);

            //mysql
            //string jsonData = mysql.Convert.Table.ToJson(string_connetion,
            //"call SP_search  ('ج.م','" + SP_Search.PropertiesType + "','" + category_name + "','" + SP_Search.City + "'," + Bedrooms + "," 
            //+ Bathrooms +"," + min_price + "," + max_price +"," + min_area + "," + max_area + ",'" + SP_Search.DisplayType + "');", Case_char.lower);

            //ora

            string[] _p_valuse = { "p_currency", "p_properties_type", "p_category_name" , "p_city" , "p_Bedrooms", "p_Bathrooms",
            "p_min_price","p_max_price","p_min_area","p_max_area","p_Display_type"};
            OracleDbType[] _oracleDbTypes = { OracleDbType.NVarchar2,OracleDbType.NVarchar2,OracleDbType.NVarchar2,OracleDbType.NVarchar2,
            OracleDbType.Int32,OracleDbType.Int32,OracleDbType.Int32,OracleDbType.Int32,OracleDbType.Int32,OracleDbType.Int32,OracleDbType.Int32};
            object[] _valuse = { "ج.م  ", SP_Search.PropertiesType, category_name , SP_Search.City , Bedrooms ,
            Bathrooms,min_price,max_price,min_area,max_area,SP_Search.DisplayType};

            string jsonData = ora.Convert.SP.ToJson(string_connetion,
               "SP_SEARCH", _p_valuse, _oracleDbTypes, _valuse, Case_char.lower);

            // System.Diagnostics.Debug.WriteLine(jsonData);

            search_jsonData = jsonData;

            if(category_name == "شقة")
            {
                ViewBag.type = "شقق";
            }else if(category_name == "فيلا")
            {
                ViewBag.type = "فيلات";
            }
            else if (category_name == "بنتهاوس")
            {
                ViewBag.type = "بنتهاوس";
            }
            else if (category_name == "توين هاوس")
            {
                ViewBag.type = "توين هاوس";
            }
            else if (category_name == "دوبلكس")
            {
                ViewBag.type = "دوبلكس";
            }
            else if (category_name == "شاليه")
            {
                ViewBag.type = "شاليهات";
            }
            else if (category_name == "كمبوند")
            {
                ViewBag.type = "كمبوند";
            }
            else
            {
                ViewBag.type = "عقارات";
            }

            if(SP_Search.DisplayType == "للبيع")
            {
                ViewBag.all = "للبيع في مَصر";
            }
            else
            {
                ViewBag.all = "للايجار في مَصر";
            }

            ViewBag.home = "الصفحة الرئيسيه";
            ViewBag.currency = "ج.م";
            ViewBag.m = "متر مربع";
            ViewBag.Bedrooms_card = "غرف نوم";
            ViewBag.Bathrooms_card = "الحمامات";
            ViewBag.Area_card = "المساحة";
            ViewBag.Floor_card = "الطابق";
            ViewBag.Parking_card = "موقف سيارات";
            ViewBag.book = "حجز موعد زياره";
            ViewBag.view = "عرض";
            //---------------------------
            ViewBag.location = "أدخل الموقع";
            ViewBag.Bedrooms_Bathrooms = "عدد الغرف و الحمامات";
            ViewBag.Bedrooms = "عدد الغرف";
            ViewBag.Bathrooms = "عدد الحمامات";
            ViewBag.g0 = "الكل";
            ViewBag.g1 = "شقة";
            ViewBag.g2 = "فيلا";
            ViewBag.g3 = "بنتهاوس";
            ViewBag.g4 = "توين هاوس";
            ViewBag.g5 = "دوبلكس";
            ViewBag.g6 = "شاليه";
            ViewBag.g7 = "كمبوند";
            ViewBag.t2g0 = "الكل";
            ViewBag.t2g1 = "مكتب";
            ViewBag.t2g2 = "مطعم و كافيه";
            ViewBag.t2g3 = "مجمع تجاري";
            ViewBag.t2g4 = "محلات تجارية";
            ViewBag.t2g5 = "مستودع";
            ViewBag.t2g6 = "عيادة";
            ViewBag.t2g7 = "مصنع";
            ViewBag.t2g8 = "جراج";
            ViewBag.type1 = "سكني";
            ViewBag.type2 = "تجاري";
            ViewBag.pay1 = "للبيع";
            ViewBag.pay2 = "للايجار";
            ViewBag.area_d = "المساحة (متر مربع)";
            ViewBag.area_min = "الحد الأدنى متر مربع";
            ViewBag.area_max = "الحد الأعلى متر مربع";
            ViewBag.area_min2 = "متر مربع الحد الأدنى ";
            ViewBag.area_max2 = "متر مربع الحد الأعلى";
            ViewBag.price_min = "الحد الأدنى";
            ViewBag.price_max = "الحد الأعلى";
            ViewBag.price_d = "السعر";
            ViewBag.search = "ابحث";
            ViewBag.t1 = "سكني";
            ViewBag.t2 = "تجاري";

            return View();
        }

        public ActionResult property_details(REALESTATEModel_ora.VALLPROPERTY properties_Data)
        {

            //sql
            //var Data = sql.Convert.Table.ToDictionary(string_connetion,
            //   "exec SP_Get_Properties_by_id @Propertie_id = " + properties_Data.id);

            //mysql
            //var Data = mysql.Convert.Table.ToDictionary(string_connetion,
            //   "call SP_Get_Properties_by_id (" + properties_Data.Id + ",'ج.م')");

            //ora
            string[] _p_valuse = { "p_currency", "Propertie_id" };
            OracleDbType[] _oracleDbTypes = { OracleDbType.NVarchar2, OracleDbType.Int32 };
            object[] _valuse = { "ج.م  ", properties_Data.Id };
            var Data = ora.Convert.SP.ToDictionary(string_connetion,
               "SP_GET_PROPERTIES_BY_ID", _p_valuse, _oracleDbTypes, _valuse);

            ViewBag.data = Data.Result.Item1;
            ViewBag.home = "الصفحة الرئيسيه";
            ViewBag.video = "فيديو";
            ViewBag.map = "الخريطة";
            ViewBag.image = "صور العقار";
            ViewBag.m = "متر مربع";
            ViewBag.Area_card = "المساحة";
            ViewBag.type = "نوع العقار";
            ViewBag.building = "حالة البناء";
            ViewBag.Furnishing = "التأثيث";
            ViewBag.publich = "تاريخ الإضافة";
            ViewBag.Display_type = "نوع العرض";
            ViewBag.book = "حجز موعد زياره";
            ViewBag.agent = ":وكيل العقار";
            ViewBag.model = "#myModel";

            List<string> images = new List<string>(Data.Result.Item1["images"].Split(','));
            ViewBag.images = images;


            return View();
        }

        public ActionResult contact()
        {
            return View();
        }

    }

    public class search_data
    {
       public string min_price { get; set; }
       public string max_price { get; set; }
       public string min_area { get; set; }
       public string max_area { get; set; }

    }

}