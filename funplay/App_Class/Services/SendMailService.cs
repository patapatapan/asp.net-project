using funplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SendMailService : BaseClass
{
    /// <summary>
    /// 會員註冊寄發驗證的電子郵件
    /// </summary>
    /// <param name="validateCode">驗證碼</param>
    /// <returns></returns>
    public string UserRegister(string validateCode)
    {
        using (z_repoUsers users = new z_repoUsers())
        {
            using (GmailService gmail = new GmailService())
            {
                //驗證
                var userData = users.repo.ReadSingle(m => m.ValidateCode == validateCode);
                if (userData == null) { return "查無驗證碼!!"; }
                if ((bool)userData.IsValid) { return "此驗證碼已通過驗證!!"; }
                if (string.IsNullOrEmpty(userData.ContactEmail)) { return "此會員未輸入電子信箱!!"; }
                if (string.IsNullOrEmpty(AppService.WebSiteUrl)) { return "Web.config 未設定 WebSiteUrl 參數!!"; }
                //變數
                string str_member_no = userData.UserNo;
                string str_member_name = userData.UserName;
                string str_member_email = userData.ContactEmail;
                string str_reg_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

                string str_controller = "Web";
                string str_action = "ValidateEmail";
                string str_url = AppService.WebSiteUrl;
                string str_validate_url = $"{str_url}/{str_controller}/{str_action}/{validateCode}";

                //信件內容
                gmail.MessageText = "";
                gmail.ReceiverName = str_member_name;
                gmail.ReceiverEmail = str_member_email;
                gmail.Subject = string.Format("{0} 會員註冊驗證通知信", AppService.AppName);
                gmail.Body = string.Format("敬愛的會員 {0} 您好!! <br /><br />", str_member_name);
                gmail.Body += string.Format("您於 {0} 在我們網站註冊了會員帳號<br />", str_reg_date);
                gmail.Body += string.Format("您的會員帳號為：{0}<br />", str_member_no);
                gmail.Body += "請您點擊以下連結進行帳號電子郵件驗證<br /><br />";
                gmail.Body += string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a><br /><br />", str_validate_url, str_validate_url);
                gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
                gmail.Body += "-------------------------------------------<br />";
                gmail.Body += string.Format("{0}<br />", AppService.AppName);
                gmail.Body += string.Format("{0}<br />", str_url);
                gmail.Body += "-------------------------------------------<br />";
                //寄信
                gmail.Send();
                return gmail.MessageText;
            }
        }
    }

    /// <summary>
    /// 帳號忘記密碼寄發密碼重設的電子郵件
    /// </summary>
    /// <param name="emailAddress">電子郵件</param>
    /// <param name="validateCode">驗證碼</param>
    /// <param name="UserName">會員名稱</param>
    /// <param name="userPassword">新的密碼</param>
    /// <returns></returns>
    public string UserForget(string emailAddress, string validateCode, string UserName, string userPassword)
    {
        using (GmailService gmail = new GmailService())
        {
            if (string.IsNullOrEmpty(AppService.WebSiteUrl)) { return "Web.config 未設定 WebSiteUrl 參數!!"; }
            //變數
            string str_reg_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            string str_controller = "Shop";
            string str_action = "ValidateForget";
            string str_url = AppService.WebSiteUrl;
            string str_validate_url = $"{str_url}/{str_controller}/{str_action}/{validateCode}";

            //信件內容
            gmail.MessageText = "";
            gmail.ReceiverName = UserName;
            gmail.ReceiverEmail = emailAddress;
            gmail.Subject = string.Format("{0} 帳號忘記密碼重新設定通知信", AppService.AppName);
            gmail.Body = string.Format("敬愛的會員 {0} 您好!! <br /><br />", UserName);
            gmail.Body += string.Format("您於 {0} 在我們網站執行了忘記密碼的功能，<br /><br />", str_reg_date);
            gmail.Body += string.Format("您新的密碼為： {0} <br /><br />", userPassword);
            gmail.Body += "請您點擊以下連結進行忘記密碼驗證，並再自行變更您熟悉的密碼！！<br /><br />";
            gmail.Body += string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a><br /><br />", str_validate_url, str_validate_url);
            gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
            gmail.Body += "-------------------------------------------<br />";
            gmail.Body += string.Format("{0}<br />", AppService.AppName);
            gmail.Body += string.Format("{0}/Shop<br />", str_url);
            gmail.Body += "-------------------------------------------<br />";
            //寄信
            gmail.Send();
            return gmail.MessageText;
        }
    }

    /// <summary>
    ///連絡我們的電子郵件
    /// </summary>
    /// <param name="model">輸入資料</param>
    /// <returns></returns>
    public string ContactUs(vmContact model)
    {
        using (GmailService gmail = new GmailService())
        {
            using (z_repoApplications app = new z_repoApplications())
            {
                var appData = app.GetEnabledApplication();
                //寄信給管理員
                gmail.MessageText = "";
                gmail.ReceiverName = appData.MailReceiverName;
                gmail.ReceiverEmail = appData.MailReceiverEmail;
                gmail.Subject = string.Format("{0} 連絡我們的通知信-{1}", AppService.AppName, model.ContactorName);
                gmail.Body = string.Format("敬愛的 {0} 您好!! <br /><br />", appData.MailReceiverName);
                gmail.Body += string.Format("{0} 在我們網站 {1} 提交了一封連絡訊息，<br /><br />", model.ContactorName, AppService.AppName);
                gmail.Body += string.Format("訊息的內容如下：<br /><br />");
                gmail.Body += string.Format("訊息提交時間： {0} <br />", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                gmail.Body += string.Format("提訊人姓名： {0} <br />", appData.MailReceiverName);
                gmail.Body += string.Format("提訊人信箱： {0} <br />", appData.MailReceiverEmail);
                gmail.Body += string.Format("訊息主旨： {0} <br />", model.ContactorSubject);
                gmail.Body += string.Format("訊息內文：<br /></hr>");
                gmail.Body += model.ContactorMessage;
                gmail.Body += "<br /><br />";
                gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
                gmail.Body += "-------------------------------------------<br />";
                gmail.Body += string.Format("{0}<br />", AppService.AppName);
                gmail.Body += "-------------------------------------------<br />";
                //寄信
                gmail.Send();
                if (!string.IsNullOrEmpty(gmail.MessageText)) return gmail.MessageText;

                //寄信給提交訊息的人員備查
                gmail.MessageText = "";
                gmail.ReceiverName = appData.MailReceiverName;
                gmail.ReceiverEmail = appData.MailReceiverEmail;
                gmail.Subject = string.Format("{0} 連絡我們訊息已提交, 請靜待回覆!!", AppService.AppName);
                gmail.Body = string.Format("敬愛的 {0} 您好!! <br /><br />", model.ContactorName);
                gmail.Body += string.Format("您在我們網站 {0} 提交了一封連絡訊息，<br /><br />", AppService.AppName);
                gmail.Body += string.Format("訊息的內容如下：<br /><br />");
                gmail.Body += string.Format("訊息提交時間： {0} <br />", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                gmail.Body += string.Format("提訊人姓名： {0} <br />", appData.MailReceiverName);
                gmail.Body += string.Format("提訊人信箱： {0} <br />", appData.MailReceiverEmail);
                gmail.Body += string.Format("訊息主旨： {0} <br />", model.ContactorSubject);
                gmail.Body += string.Format("訊息內文：<br /></hr>");
                gmail.Body += model.ContactorMessage;
                gmail.Body += "<br /><br />";
                gmail.Body += "我們已收到您的來信，將會在最短的時間內給您回覆，感謝您的來信!!<br /><br />";
                gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
                gmail.Body += "-------------------------------------------<br />";
                gmail.Body += string.Format("{0}<br />", AppService.AppName);
                gmail.Body += "-------------------------------------------<br />";
                //寄信
                gmail.Send();

                return gmail.MessageText;
            }
        }
    }

    /// <summary>
    ///訂閱
    /// </summary>
    /// <param name="email">電子信箱</param>
    /// <param name="isAddEmail">是否訂閱/退訂</param>
    /// <returns></returns>
    public string Subscription(string email, bool isAddEmail)
    {
        using (GmailService gmail = new GmailService())
        {
            using (z_repoApplications app = new z_repoApplications())
            {
                var appData = app.GetEnabledApplication();
                //寄信給管理員
                string str_data = (isAddEmail) ? "訂閱" : "退訂";
                gmail.MessageText = "";
                gmail.ReceiverName = appData.MailReceiverName;
                gmail.ReceiverEmail = appData.MailReceiverEmail;
                gmail.Subject = string.Format("{0} {1}網站的通知信", AppService.AppName, str_data);
                gmail.Body = string.Format("敬愛的 {0} 您好!! <br /><br />", appData.MailReceiverName);
                gmail.Body += string.Format("我們網站 {0} 有人提交了一份{1}資訊，<br /><br />", AppService.AppName, str_data);
                gmail.Body += string.Format("訂閱人信箱： {0} <br />", email);
                gmail.Body += "<br /><br />";
                gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
                gmail.Body += "-------------------------------------------<br />";
                gmail.Body += string.Format("{0}<br />", AppService.AppName);
                gmail.Body += "-------------------------------------------<br />";
                //寄信
                gmail.Send();
                return gmail.MessageText;
            }
        }
    }
}
