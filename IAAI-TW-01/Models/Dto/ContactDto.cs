using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI_TW_01.Models.Dto
{
    public class ContactDto
    {
    }

    public class ContactSendDto
    {
        //加上前端用的驗證

        //public int Id { get; set; }
        [Required(ErrorMessage = "姓名必填")]
        public string Name { get; set; }
        [Required(ErrorMessage = "性別必填")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "聯絡電話必填")]
        [RegularExpression(@"^[0-9]{8,15}$", ErrorMessage = "請輸入正確電話格式:09XXXXXXXX")]
        public string Tel { get; set; }
        [Required(ErrorMessage = "E-mail必填")]
        [EmailAddress(ErrorMessage = "請輸入正確Email格式")]
        public string Email { get; set; }
        [Required(ErrorMessage = "詢問內容必填")]
        public string Content { get; set; }
    }
}