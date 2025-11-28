using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI_TW_01.Models
{
    public class EnumList
    {
        public enum Gender
        {
            [Display(Name = "男性")]
            Male = 0,
            [Display(Name = "女性")]
            Female = 1,
            [Display(Name = "其他")]
            Other = 2
        }

        public enum MemberType
        {
            Normal = 0,
            VIP = 1
        }

    }
}