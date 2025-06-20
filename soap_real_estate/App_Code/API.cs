using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for API
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class API : System.Web.Services.WebService
{

    private real_estateEntities1 db = new real_estateEntities1();

    public API()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //get
    [WebMethod]
    public List<V_AllProperties> GetAllProperties()
    {
        List<V_AllProperties> result = db.V_AllProperties.ToList();
        return result;
    }

    [WebMethod]
    public V_AllProperties Get(int id)
    {
        V_AllProperties result = db.V_AllProperties.SingleOrDefault(i => i.id == id);
        if (result == null)
            return null;
        return result;
    }
    //post
    [WebMethod]
    private void insert_properties(int category_id,
         string title, string image_path, string properties_type)
    {
        List<property> list_properties = new List<property>() {
            new property(){
            category_id = category_id, image_path = image_path, title = title,
                properties_type=properties_type
            }
            };
        foreach (var item in list_properties)
        {
            db.properties.Add(item);
        }

        db.SaveChanges();
    }
    [WebMethod]
    private void insert_agent_info(string agent,
        int national_number, string agent_address, string email_address,
        string phone_number_agent, string whatsapp_ink)
    {
        List<agent_info> list_agent_info = new List<agent_info>() {
            new agent_info(){
             address = agent_address,email=email_address,
             phone_number=phone_number_agent,
             agent = agent,national_number=national_number,whatsapp_ink = whatsapp_ink
            }
           };
        foreach (var item in list_agent_info)
        {
            db.agent_info.Add(item);
        }

        db.SaveChanges();
    }
    [WebMethod]
    private void insert_properties_info(int id_properties, string images,
        string city, string address, string info_propertie, int year_built, int Bedrooms,
        int Bathrooms, int Area, int Floor, int Parking, int price, string currency,
        string Payment_method, string long_address, string building_type, string Display_type,
        string Furnishing, string publich_date, string map_link, string youtube_video_link)
    {
        List<properties_info> list_properties_info = new List<properties_info>() {
            new properties_info(){
                id_properties = id_properties,
            city = city,address = address, long_address = long_address, year_built = year_built,
            Floor = Floor,Area = Area,Bedrooms = Bedrooms,Bathrooms = Bathrooms,Parking = Parking,
            price = price,info_propertie = info_propertie, images = images,
            Payment_method = Payment_method, currency = currency,building_type = building_type,
            Display_type = Display_type,Furnishing = Furnishing, publich_date = publich_date,
            map_link = map_link, youtube_video_link = youtube_video_link,
            }
           };
        foreach (var item in list_properties_info)
        {
            db.properties_info.Add(item);
        }

        db.SaveChanges();
    }

    string string_connetion = ConfigurationManager.ConnectionStrings["real_estate"].ConnectionString;
    [WebMethod]
    public V_AllProperties Post(int id_properties,int category_id,
        string agent,int national_number,string agent_address, string email_address, 
        string phone_number_agent,string whatsapp_ink, string properties_type, string title,
        string image_path, string images, string city, string address, string info_propertie,
        int year_built, int Area, int Bedrooms, int Bathrooms, int Floor, int Parking,
        int price, string currency, string Payment_method, string long_address,
        string building_type, string Display_type, string Furnishing,
        string publich_date, string map_link, string youtube_video_link)
    {
        insert_properties(category_id, title, image_path, properties_type);
        insert_agent_info(agent,national_number, agent_address, email_address,
         phone_number_agent, whatsapp_ink);
        insert_properties_info(id_properties, images, city, address, info_propertie,
            year_built, Bedrooms, Bathrooms, Area, Floor, Parking, price, currency,
         Payment_method, long_address, building_type, Display_type, Furnishing,
         publich_date, map_link, youtube_video_link);


        int record = sql.DataBase.GetLastRecord(string_connetion, "V_AllProperties", "id");
        V_AllProperties result = db.V_AllProperties.SingleOrDefault(i => i.id == record);

        return result;
    }

    //put
    [WebMethod]
    private void edit_properties(int id_properties, int category_id, 
         string title, string image_path,string properties_type,int properties_agent_id)
    {
        List<property> list_properties = new List<property>() {
            new property(){id_properties = id_properties,
            category_id = category_id, image_path = image_path, title = title,
                properties_type=properties_type,agent_id=properties_agent_id
            }
            };
        foreach (var item in list_properties)
        {
            db.Entry(item).State = EntityState.Modified;

        }

        db.SaveChanges();
    }
    [WebMethod]
    private void edit_agent_info(int id_agent, string agent,
        int national_number, string agent_address,string email_address, 
        string phone_number_agent,string whatsapp_ink)
    {
        List<agent_info> list_agent_info = new List<agent_info>() {
            new agent_info(){agent_id=id_agent,
             address = agent_address,email=email_address,
             phone_number=phone_number_agent,
             agent = agent,national_number=national_number,whatsapp_ink = whatsapp_ink
            }
           };
        foreach (var item in list_agent_info)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        db.SaveChanges();
    }
    [WebMethod]
    private void edit_properties_info(int id_properties_info, int id_properties, string images,
        string city, string address, string info_propertie, int year_built, int Bedrooms,
        int Bathrooms, int Area, int Floor, int Parking, int price, string currency,
        string Payment_method, string long_address, string building_type, string Display_type,
        string Furnishing, string publich_date, string map_link, string youtube_video_link)
    {
        List<properties_info> list_properties_info = new List<properties_info>() {
            new properties_info(){id=id_properties_info,
                id_properties = id_properties,
            city = city,address = address, long_address = long_address, year_built = year_built,
            Floor = Floor,Area = Area,Bedrooms = Bedrooms,Bathrooms = Bathrooms,Parking = Parking,
            price=price,info_propertie = info_propertie, images = images,
            Payment_method = Payment_method,currency = currency,building_type = building_type,
            Display_type = Display_type,Furnishing = Furnishing, publich_date = publich_date,
            map_link = map_link, youtube_video_link = youtube_video_link,
            }
           };
        foreach (var item in list_properties_info)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        db.SaveChanges();
    }
    [WebMethod]
    public void put(int id_properties, int category_id,
         string title, string image_path, string properties_type,int properties_agent_id,
        int id_agent, string agent,
        int national_number, string agent_address, string email_address,
        string phone_number_agent, string whatsapp_ink,
        int id_properties_info, string images,
        string city, string address, string info_propertie, int year_built, int Bedrooms,
        int Bathrooms, int Area, int Floor, int Parking, int price, string currency,
        string Payment_method, string long_address, string building_type, string Display_type,
        string Furnishing, string publich_date, string map_link, string youtube_video_link)
    {
        edit_properties(id_properties,category_id, title, image_path, properties_type,
            properties_agent_id);
        edit_agent_info(id_agent, agent, national_number, agent_address, email_address,
         phone_number_agent, whatsapp_ink);
        edit_properties_info(id_properties_info, id_properties, images, city, address, info_propertie,
            year_built, Bedrooms, Bathrooms, Area, Floor, Parking, price, currency,
         Payment_method, long_address, building_type, Display_type, Furnishing,
         publich_date, map_link, youtube_video_link);
    }

    //delete
    [WebMethod]
    public void delete_properties(int id_properties)
    {
        sql.DataBase.DeleteRecord(string_connetion, "properties", "id_properties", id_properties);
    }
    [WebMethod]
    public void delete_agent_info(int id_agent)
    {
        sql.DataBase.DeleteRecord(string_connetion, "agent_info", "agent_id", id_agent);
    }
    [WebMethod]
    public void delete_properties_info(int id_properties_info)
    {
        sql.DataBase.DeleteRecord(string_connetion, "properties_info", "id", id_properties_info);
    }

}
