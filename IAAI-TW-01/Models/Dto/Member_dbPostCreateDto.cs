using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI_TW_01.Models.Dto
{
    public class Member_dbPostCreateDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        public virtual ICollection<MemberDbReply> MemberDbReplys { get; set; } //反向導航屬性，用來表示與 NewsImg 類別的關聯，這裡是一對多的關聯

        [Required]
        [Display(Name = "Title")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }


    }
}