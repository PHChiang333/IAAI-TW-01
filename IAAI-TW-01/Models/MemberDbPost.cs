using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI_TW_01.Models
{
    public class MemberDbPost
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        public virtual ICollection<MemberDbReply> MemberDbReplys { get; set; } //反向導航屬性，用來表示與 NewsImg 類別的關聯，這裡是一對多的關聯

        [Required]
        [Display(Name = "Title")]
        [MaxLength(50)]
        public string Title { get; set; }

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