using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAAI_TW_01.Models.Auth
{
    public class AuthTicketDto
    {


    }
    public class AuthTicketUserDataDto
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string IsAdmin { get; set; }

        public string IsTopAccess { get; set; }
    }


    
}