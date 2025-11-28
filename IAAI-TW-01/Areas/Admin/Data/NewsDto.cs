using IAAI_TW_01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAAI_TW_01.Areas.Admin.Data
{
    public class NewsDto
    {
    }

    
    public class NewsCreateDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string CoverName { get; set; }
        public string CoverPath { get; set; }

        public HttpPostedFileBase CoverFile { get; set; } //上傳檔案

        //public ICollection<NewsImgCreateDto> NewsImgs { get; set; } //多檔照片


    }
    public class NewsImgCreateDto
    {
        
        public string ImgName { get; set; }
        public string ImgPath { get; set; }
        public int NewsId { get; set; }
    }

    public class NewsEditDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public string CoverName { get; set; }
        public string CoverPath { get; set; }

        public HttpPostedFileBase CoverFile { get; set; } //上傳檔案
    }

}