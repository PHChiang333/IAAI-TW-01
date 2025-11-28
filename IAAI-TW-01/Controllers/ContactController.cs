using IAAI_TW_01.Models;
using IAAI_TW_01.Models.Dto;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
//using System.Net.Mail;
//改用MailKit.Net.Smtp;
using MimeKit;
using MailKit.Net.Smtp;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Threading.Tasks;
using IAAI_TW_01.Models.GoogleRe;

namespace IAAI_TW_01.Controllers
{
    public class ContactController : Controller
    {
        DBModel db = new DBModel();

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string name, string email, string phone, string message)
        {
            // 這裡可以處理表單提交的邏輯，例如將數據保存到數據庫或發送電子郵件
            // 例如：
            // db.Contacts.Add(new Contact { Name = name, Email = email, Phone = phone, Message = message });
            // db.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendContact(ContactSendDto contactSendDto)
        {
            // 這裡可以處理表單提交的邏輯，例如將數據保存到數據庫或發送電子郵件
            // 例如：
            // db.Contacts.Add(new Contact { Name = name, Email = email, Phone = phone, Message = message });
            // db.SaveChanges();

            try
            {
                if (!ModelState.IsValid)
                {
                    // ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    ViewBag.ErrorMessageClass = "text-danger";
                    ViewBag.ErrorMessage = "請檢查輸入的資料";
                    //return RedirectToAction("Index");

                    return View("Index", contactSendDto);
                }

                // Step1: 取得前端送過來的g-recaptcha-response
                var response = Request.Form["g-recaptcha-response"];

                if (string.IsNullOrEmpty(response))
                {
                    ModelState.AddModelError("", "請完成驗證碼驗證！");
                    return View("Index", contactSendDto);
                }
                else
                {
                    //驗證成功

                    // Step2: 傳送到Google驗證
                    var secretKey = WebConfigurationManager.AppSettings["SecretKey"];
                    var client = new System.Net.Http.HttpClient();
                    var result = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={response}", null);
                    var jsonString = await result.Content.ReadAsStringAsync();

                    // ReCaptcha response from Google https://developers.google.com/recaptcha/docs/verify
                    var captchaResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaResponse>(jsonString);

                    if (!captchaResult.Success)
                    {
                        ModelState.AddModelError("", "驗證碼驗證失敗，請重新嘗試！");
                        return View("Index", contactSendDto);
                    }

                    // Step3: 通過驗證，可以繼續處理表單資料

                    var addSend = new Contact
                    {
                        Name = contactSendDto.Name,
                        Email = contactSendDto.Email,
                        Tel = contactSendDto.Tel,
                        Content = contactSendDto.Content,

                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        IsDeleted = false,
                        DeleteAt = null
                    };

                    db.Contacts.Add(addSend);
                    db.SaveChanges();


                    //建立信件: 一封通知寄件人"已經收到要求"，另一封寄給網站站方
                    //NuGet: MimeKit
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("IAAI-tw", WebConfigurationManager.AppSettings["SmtpServer"]));  //寄件人: 網站站方
                    message.To.Add(new MailboxAddress(contactSendDto.Name.Trim(), contactSendDto.Email.Trim()));  //寄給客戶
                    message.Cc.Add(new MailboxAddress("收信人名稱", WebConfigurationManager.AppSettings["SmtpServer"]));  //cc給網站站方(副本給自己)
                    message.Subject = "IAAI-tw Auto Email";

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody =
                    "<h1>感謝您的聯繫，已收到您的需求，請靜待回覆!</h1>" +
                    $"<h3>Name : {contactSendDto.Name.Trim()}</h3>" +
                    $"<h3>Email : {contactSendDto.Email.Trim()}</h3>" +
                    $"<h3>Phone : {contactSendDto.Tel?.Trim()}</h3>" +
                    $"<h3>Contect : </h3>" +
                    $"<p>{contactSendDto.Content?.Trim()}</p>";

                    message.Body = bodyBuilder.ToMessageBody();

                    using (var SmtpClient = new SmtpClient())
                    {
                        SmtpClient.CheckCertificateRevocation = false; //不檢查憑證撤銷
                        SmtpClient.Connect("smtp.gmail.com", 587, false);  //SSL
                        SmtpClient.Authenticate(WebConfigurationManager.AppSettings["SmtpServer"], WebConfigurationManager.AppSettings["google2FAAppPw"]);  
                        SmtpClient.Send(message);
                        SmtpClient.Disconnect(true);
                    }

                    TempData["Success"] = "已收到您的詢問，請靜待回應";

                    //ViewBag.ErrorMessage = "已寄送成功，請靜待回應";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Log the error (uncomment dex variable name and write a log.)
                //ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                ModelState.AddModelError("", "Error sending email: " + ex.Message );

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

                ViewBag.ErrorMessageClass = "text-danger";
                ViewBag.ErrorMessage = "寄送失敗，請重新填寫";
                return View("Index", contactSendDto);
            }
        }




}
}