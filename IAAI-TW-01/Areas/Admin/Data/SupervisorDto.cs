using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI_TW_01.Areas.Admin.Data
{
    public class SupervisorDto
    {

    }

    public class SupervisorCreateDto
    {
        [Required]
        public string PositionName { get; set; }
        [Required] 
        public string Name { get; set; }
        public string Gender { get; set; }
        public string ServiceHistory { get; set; }
        public int? Term { get; set; }
    }

    public class SupervisorEditDto
    {
        public int Id { get; set; }
        [Required]
        public string PositionName { get; set; }
        [Required]
        public string Name { get; set; }
        public string Gender { get; set; }
        public string ServiceHistory { get; set; }
        public int? Term { get; set; }
    }



}