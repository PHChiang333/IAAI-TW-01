using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MvcPaging;

namespace IAAI_TW_01.Models.Dto
{
    public class Member_dbPostDetailDto
    {
        public MemberDbPost MemberDbPost { get; set; }


        public MvcPaging.IPagedList<IAAI_TW_01.Models.MemberDbReply> MemberDbReplys { get; set; }

    }
}