using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI_TW_01.Models.Dto
{
    public class Member_dbPostReplyCreateDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }





        [Required]
        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }



        [Required]
        [Display(Name = "MemberDbPostId")]
        public int MemberDbPostId { get; set; } //表示外鍵，用來參考 MemberDbPost 類別中的主鍵
    }
}