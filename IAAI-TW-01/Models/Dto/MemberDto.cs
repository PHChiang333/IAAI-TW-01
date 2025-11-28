using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAAI_TW_01.Models.Dto
{
    public class MemberDto
    {
    }

    public class MemberRegDto
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Account")]
        [MaxLength(20)]
        public string Account { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "PasswordSalt")]
        [MaxLength(100)]
        public string PasswordSalt { get; set; }

        [Display(Name = "PasswordHash")]
        public string PasswordHash { get; set; }


        [Display(Name = "Permission")]
        [MaxLength(500)]
        public string Permission { get; set; }


        [Display(Name = "IsAdmin")]
        public bool? IsAdmin { get; set; } //是否為管理員

        [Display(Name = "權限")]
        [MaxLength(500)]
        public string NewPermission { get; set; }

        public MemberRegInfo memberRegInfo { get; set; } //會員資料

        public List<MemberRegService> memberRegServices { get; set; } //會員履歷


    }

    public class MemberRegInfo
    {
        //會員資料
        [Required][Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "姓名")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "性別")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "生日")]
        [DataType(DataType.Date)] // 指定型態是 Date
        public DateTime? Birth { get; set; }

        [Required]
        [Display(Name = "申請類型")]
        public string MemberType { get; set; }

        [Required]
        [Display(Name = "通訊處")]
        [MaxLength(50)]
        public string ContactAddress { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "請輸入E-mail")]
        [EmailAddress(ErrorMessage = "E-mail格式不正確")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required][Display(Name = "國際會籍")]
        public bool IsInternationalMember { get; set; }

        [Required]
        [Display(Name = "現職單位")]
        [MaxLength(50)]
        public string ServiceAt { get; set; }

        [Required]
        [Display(Name = "職稱")]
        [MaxLength(50)]
        public string PositionName { get; set; }

        [Required]
        [Display(Name = "最高學歷")]
        [MaxLength(50)]
        public string TopLevelEducation { get; set; }




        [Display(Name = "相關年資")]
        [DataType(DataType.Date)] // 指定型態是 Date
        public DateTime? RelativeServicedTime { get; set; }

        [Display(Name = "相關年資(年)")]
        public int? RelativeServicedTimeYear { get; set; }

        [Display(Name = "相關年資(月)")]
        public int? RelativeServicedTimeMonth { get; set; }


        [Required]
        [Display(Name = "MemberId")]
        public int MemberId { get; set; } //表示外鍵，用來參考 Member 類別中的主鍵
    }

    public class MemberRegService
    {
        //會員履歷

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "服務單位")]
        [MaxLength(50)]
        public string ServiceAt { get; set; }

        [Display(Name = "職稱")]
        [MaxLength(50)]
        public string PositionName { get; set; }

        [Display(Name = "服務期間:起")]
        [DataType(DataType.Date)] // 指定型態是 Date
        public DateTime? ServicedTimeStart { get; set; }

        [Display(Name = "服務期間:起(年)")]
        public int? ServicedTimeStartYear { get; set; }

        [Display(Name = "服務期間:起(月)")]
        public int? ServicedTimeStartMonth { get; set; }

        [Display(Name = "服務期間:迄")]
        [DataType(DataType.Date)] // 指定型態是 Date
        public DateTime? ServicedTimeEnd { get; set; }

        [Display(Name = "服務期間:迄(年)")]
        public int? ServicedTimeEndYear { get; set; }

        [Display(Name = "服務期間:迄(月)")]
        public int? ServicedTimeEndMonth { get; set; }

        [Display(Name = "服務期間")]
        public DateTime? ServicedTimePeriod { get; set; }

        [Required]
        [Display(Name = "MemberId")]
        public int MemberId { get; set; } //表示外鍵，用來參考 Member 類別中的主鍵

    }



    public class LoginDto
    {
        [Required]
        [Display(Name = "Account")]
        [MaxLength(20)]
        public string Account { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }




}