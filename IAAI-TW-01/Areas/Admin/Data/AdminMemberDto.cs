using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI_TW_01.Areas.Admin.Data
{
    public class AdminMemberDto
    {

    }

    public class AdminMemberCreateDto
    {


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


    }

    public class AdminMemberEditDto
    {


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


    }



    public class AdminMemberLoginDto
    {
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
    }


}