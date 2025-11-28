using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace IAAI_TW_01.Models
{
    public class MemberInfo
    {
        //會員資料

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
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

        [Display(Name = "國際會籍")]
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


        [Required]
        [Display(Name = "MemberId")]
        public int MemberId { get; set; } //表示外鍵，用來參考 Member 類別中的主鍵

        [JsonIgnore]
        [ForeignKey("MemberId")] //標示 MyMember 是一個外鍵導航屬性
        public virtual Member Member { get; set; } //導航屬性，用來表示與 Member 類別的關聯，這裡是多對一的關聯



        [Display(Name = "相關年資")]
        [DataType(DataType.Date)] // 指定型態是 Date
        public DateTime? RelativeServicedTime { get; set; }

        [Display(Name = "相關年資(年)")]
        public int? RelativeServicedTimeYear { get; set; }

        [Display(Name = "相關年資(月)")]
        public int? RelativeServicedTimeMonth { get; set; }









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