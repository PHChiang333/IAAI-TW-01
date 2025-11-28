using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace IAAI_TW_01.Models.Utility
{
    public class Utility
    {
        #region 密碼加密
        public const int DefaultSaltSize = 5;

        /// <summary>
        /// 產生Salt
        /// </summary>
        /// <returns>Salt</returns>
        public static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[DefaultSaltSize];
            rng.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// 產生密碼Hash
        /// </summary>
        /// <param name="password">輸入密碼</param>
        /// <param name="salt">密碼鹽</param>
        /// <returns></returns>
        public static string GenerateHashWithSalt(string password, string salt)
        {
            // merge password and salt together
            string sHashWithSalt = password + salt;
            // convert this merged value to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
            // use hash algorithm to compute the hash
            HashAlgorithm algorithm = new SHA256Managed();
            // convert merged bytes to a hash as byte array
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // return the has as a base 64 encoded string
            return Convert.ToBase64String(hash);
        }
        #endregion

        #region "將使用者資料寫入cookie,產生AuthenTicket"

        /// <summary>
        /// 將使用者資料寫入cookie,產生AuthenTicket
        /// 設定認證票證並創建用戶的認證 Cookie。
        /// </summary>
        /// <param name="userData">使用者資料，要存儲在認證票證中的用戶特定數據。</param>
        /// <param name="userId">UserAccount，用戶的識別符。</param>
        public static void SetAuthenTicket(string userData, string userId, HttpResponseBase response)
        {
            //宣告一個驗證票
            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddHours(3), false, userData);
            
            //加密驗證票
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            //建立Cookie
            HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            // Optional: 設定 Cookie 路徑與 HttpOnly
            authenticationcookie.HttpOnly = true;
            authenticationcookie.Path = FormsAuthentication.FormsCookiePath;

            //將Cookie寫入回應
            //HttpContext.Current.Response.Cookies.Add(authenticationcookie);

            //用傳入的 response，而不是 HttpContext.Current
            response.Cookies.Add(authenticationcookie);


        }

        /// <summary>
        /// 將使用者資料寫入cookie,產生AuthenTicket
        /// 設定認證票證並創建用戶的認證 Cookie。
        /// </summary>
        /// <param name="userData">使用者資料，要存儲在認證票證中的用戶特定數據。</param>
        /// <param name="userId">UserAccount，用戶的識別符。</param>
        public static void SetAuthenTicketOld(string userData, string userId)
        {
            //宣告一個驗證票
            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddHours(3), false, userData);

            //加密驗證票
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            //建立Cookie
            HttpCookie authenticationcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            //將Cookie寫入回應
            HttpContext.Current.Response.Cookies.Add(authenticationcookie);


        }













        /// <summary>
        /// 設定認證票證並創建用戶的認證 Cookie。
        /// </summary>
        /// <param name="userData">要存儲在認證票證中的用戶特定數據。</param>
        /// <param name="userId">用戶的識別符。</param>
        /// <returns>包含加密認證票證的認證 Cookie。</returns>
        public static HttpCookie SetAuthenTicketReturnCookie(string userData, string userId)
        {
            // 聲明一個認證票證。
            // 注意：需要額外引入 using System.Web.Security;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddHours(1), false, userData);

            // 加密認證票證。
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            // 創建一個 Cookie。
            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            // 寫入 Cookie 到回應。
            return authenticationCookie;
        }











        #endregion



        /// <summary>
        /// 驗證使用者帳號密碼(SaltHash)
        /// ***目前限制為  db.Member 專用***
        /// </summary>
        /// <param name="db">輸入的DBModel(內含Member)</param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns>回傳取出的指定member物件,無對應member則是null</returns>
        public static Member ValidateUser(DBModel db,string account, string password)
        {
            //確認帳號是否存在
            Member member = db.Members.FirstOrDefault(m => m.Account == account);
            if (member == null)
            {
                return null;
            }
            //確認密碼是否正確
            //資料庫資料
            //雜湊後的Pw
            string dbPasswordHash = member.PasswordHash;
            string salt = member.PasswordSalt;
            //產生雜湊密碼
            var selPasswordHash = Utility.GenerateHashWithSalt(password, salt);
            if (selPasswordHash != dbPasswordHash)
            {
                return null;
            }
            return member;
        }
    }
}