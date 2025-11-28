using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI_TW_01.Models
{
    public class Contact
    {
        //聯絡我們

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "請輸入聯絡電話")]
        [RegularExpression(@"^[0-9]{8,15}$", ErrorMessage = "電話格式錯誤，請輸入8-15位數字")]
        [Display(Name = "Tel")]
        [MaxLength(50)]
        public string Tel { get; set; }

        [Required(ErrorMessage = "請輸入E-mail")]
        [EmailAddress(ErrorMessage = "E-mail格式不正確")]
        [Display(Name = "Email")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }


        
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