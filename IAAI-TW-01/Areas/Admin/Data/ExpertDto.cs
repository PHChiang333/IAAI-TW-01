using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI_TW_01.Areas.Admin.Data
{

    public class ExpertDto
    {

    }

    public class ExpertCreateDto
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string ServiceAt { get; set; }

        public string CoverName { get; set; } //上傳檔案名稱
        public string CoverPath { get; set; } //上傳檔案路徑

        public HttpPostedFileBase CoverFile { get; set; } //上傳檔案
        
        [Required]
        [AllowHtml]
        public string History { get; set; }


    }


    public class ExpertEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ServiceAt { get; set; }

        public string CoverName { get; set; } //上傳檔案名稱
        public string CoverPath { get; set; } //上傳檔案路徑

        public HttpPostedFileBase CoverFile { get; set; } //上傳檔案

        [Required]
        [AllowHtml]
        public string History { get; set; }
    }
}