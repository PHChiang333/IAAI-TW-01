using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace IAAI_TW_01.Models
{
    public class MemberService
    {
        //會員履歷
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [JsonIgnore]
        [ForeignKey("MemberId")] //標示 MyMember 是一個外鍵導航屬性
        public virtual Member Member { get; set; } //導航屬性，用來表示與 Member 類別的關聯，這裡是多對一的關聯



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