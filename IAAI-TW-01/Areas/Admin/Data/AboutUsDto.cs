using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI_TW_01.Areas.Admin.Data
{
    public class AboutUsDto
    {
    }


    public class AboutUsCreateDto
    {

        [Display(Name = "Id")]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Title")]
        [MaxLength(50)]
        public string Title { get; set; }

        [AllowHtml]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "IsTop")]
        public bool IsTop { get; set; }


    }


    public class AboutUsEditDto
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