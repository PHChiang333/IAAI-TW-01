using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI_TW_01.Models
{
    public class AdminMember
    {
        //管理員帳號

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Account")]
        [MaxLength(20)]
        public string Account { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 4)]
        [DataType(DataType.Password)]
        //[MaxLength(100)]
        public string Password { get; set; }

        [Display(Name = "PasswordSalt")]
        [MaxLength(100)]
        public string PasswordSalt { get; set; }

        [Display(Name = "PasswordHash")]
        public string PasswordHash { get; set; }



        [Display(Name = "Permission")]
        [MaxLength(500)]
        public string Permission { get; set; }

        [Required]
        [Display(Name = "IsTopAccess")]
        public bool IsTopAccess { get; set; } //是否為管理員


        [Display(Name = "權限")]
        [MaxLength(500)]
        public string NewPermission { get; set; }




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