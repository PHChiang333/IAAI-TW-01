using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI_TW_01.Areas.Admin.Data
{
    public class OrgHistoryDto
    {
    }

    public class OrgHistoryCreateDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Title")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "IsTop")]
        public bool IsTop { get; set; }
    }

    public class OrgHistoryEditDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Title")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "IsTop")]
        public bool IsTop { get; set; }
    }
}