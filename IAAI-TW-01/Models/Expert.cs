using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI_TW_01.Models
{
    public class Expert
    {
        //關於我們:專家

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        //---------------------------------------
        //簡歷

        [Required]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        //現職
        [Display(Name = "ServiceAt")]
        public string ServiceAt { get; set; }

        //照片
        [Display(Name = "CoverName")]
        public string CoverName { get; set; }

        [Display(Name="CoverPath")]
        public string CoverPath { get; set; }

        //---------------------------------------
        //其他資料(基本資料等)
        //先用CKE處理
        [AllowHtml]
        [Display(Name = "History")]
        public string History { get; set; }











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