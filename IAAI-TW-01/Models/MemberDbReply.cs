using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace IAAI_TW_01.Models
{
    public class MemberDbReply
    {
        //最新消息

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }



        [Display(Name = "Title")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "AuthorId")]
        public int AuthorId { get; set; }

        [Required]
        [Display(Name = "Author")]
        [MaxLength(50)]
        public string Author { get; set; }




        [Required]
        [Display(Name = "MemberDbPostId")]
        public int MemberDbPostId { get; set; } //表示外鍵，用來參考 MemberDbPost 類別中的主鍵

        [JsonIgnore]
        [ForeignKey("MemberDbPostId")] //標示 MyMemberDbPost 是一個外鍵導航屬性
        public virtual MemberDbPost MemberDbPost { get; set; } //導航屬性，用來表示與 MemberDbPost 類別的關聯，這裡是多對一的關聯



        //必要戳記屬性

        [Display(Name = "CreateAt")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        public DateTime CreateAt { get; set; }

        [Display(Name = "UpdateAt")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        public DateTime UpdateAt { get; set; }


        [Display(Name = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "DeleteAt")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        public DateTime? DeleteAt { get; set; }


    }
}