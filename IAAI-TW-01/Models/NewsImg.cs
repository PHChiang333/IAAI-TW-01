using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace IAAI_TW_01.Models
{
    public class NewsImg
    {
        //新聞圖片

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Display(Name = "ImgPath")]
        public string ImgPath { get; set; }



        [Required]
        [Display(Name = "NewsId")]
        public int NewsId { get; set; } //表示外鍵，用來參考 News 類別中的主鍵

        [JsonIgnore]
        [ForeignKey("NewsId")] //標示 MyNews 是一個外鍵導航屬性
        public virtual News News { get; set; } //導航屬性，用來表示與 News 類別的關聯，這裡是多對一的關聯



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