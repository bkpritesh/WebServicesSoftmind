using Nitin.Sms.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;


namespace SoftMindSolution
{
    /// <summary>
    /// Summary description for SoftMindWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SoftMindWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public int SetMessage(string Name,string Subject,string Description,string MobileNo,string ReminderDate) {

            int Result = 0;
            try
            {
                    SoftmindWebservicesEntities Entities = new SoftmindWebservicesEntities();
                    Message DBMessage = new Message();
                    DBMessage.Name = Name;
                    DBMessage.Subject = Subject;
                    DBMessage.Description = Description;
                    DBMessage.MobileNo = MobileNo;
                    DBMessage.Date = ReminderDate;
                    Entities.Messages.Add(DBMessage);
                    Entities.SaveChanges();
                    Result = 1;
               
            }
            catch (Exception)
            {
                
            }
            return Result;
        }

        [WebMethod]
        public int SendSMS() {
            int Result = 0;
            try
            {
                SoftmindWebservicesEntities Entities = new SoftmindWebservicesEntities();               
                var MessageResult= Entities.GetTodayMessage();
                Way2Sms oway2 = new Way2Sms("8866671361", "9825146475");
                oway2.Login();
                foreach (GetTodayMessage_Result Msg in MessageResult)
                {
                   
                    String ContactNo = Msg.MobileNo;
                    string Description = Msg.Description;
                    oway2.SendSms(ContactNo,Description);
                }              
            }
            catch (Exception ex)
            {
                
            }
            return Result;

        }

    }
}
