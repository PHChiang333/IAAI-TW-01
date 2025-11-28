using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI_TW_01.Areas.Admin.Data
{
    public class KnowlodgeDto
    {
    }

    public class KnowlodgeCreateDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string FilePath { get; set; }

        public HttpPostedFileBase File { get; set; }

    }

    public class KnowlodgeEditDto
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string FilePath { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}