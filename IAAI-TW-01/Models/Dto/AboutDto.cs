using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAAI_TW_01.Models.Dto
{
    public class AboutDto
    {

    }

    public class AboutExpertDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ServiceAt { get; set; }
        public string History { get; set; }
    }

    public class AboutSupervisorDto
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public string Name { get; set; }
        public int? Gender { get; set; }
        public string ServiceHistory { get; set; }
        public int? Term { get; set; }
    }
}