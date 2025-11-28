using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace IAAI_TW_01.Models
{
    public partial class DBModel : DbContext
    {
        // 您的內容已設定為使用應用程式組態檔 (App.config 或 Web.config)
        // 中的 'DBModel' 連接字串。根據預設，這個連接字串的目標是
        // 您的 LocalDb 執行個體上的 'IAAI.Models.DBModel' 資料庫。
        // 
        // 如果您的目標是其他資料庫和 (或) 提供者，請修改
        // 應用程式組態檔中的 'DBModel' 連接字串。

        public DBModel()
            : base("name=DBModel")
        {
        }

        // 針對您要包含在模型中的每種實體類型新增 DbSet。如需有關設定和使用
        // Code First 模型的詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=390109。

        // public virtual DbSet<MyEntity> MyEntities { get; set; }


        //聯絡我們
        public virtual DbSet<Contact> Contacts { get; set; }
        
        //知識庫
        public virtual DbSet<Knowlodge> Knowlodges { get; set; }


        //最新消息
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsImg> NewsImgs { get; set; }

        //關於我們
        public virtual DbSet<AboutUs> AboutUs { get; set; }

        public virtual DbSet<OrgHistory> OrgHistorys { get; set; }

        public virtual DbSet<Expert> Experts { get; set; }

        public virtual DbSet<Supervisor> Supervisors { get; set; }




        //會員
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberInfo> MemberInfos { get; set; }
        public virtual DbSet<MemberService> MemberServices { get; set; }

        public virtual DbSet<MemberDbPost> MemberDbPosts { get; set; }

        public virtual DbSet<MemberDbReply> MemberDbReplys { get; set; }


        //管理員

        public virtual DbSet<AdminMember> AdminMembers { get; set; }

        //權限

        public virtual DbSet<Permission> Permissions { get; set; }


        //協會業務

        public virtual DbSet<BussinessService> BussinessServices { get; set; }
        public virtual DbSet<BussinessTraining> BussinessTrainings { get; set; }

        public virtual DbSet<BussinessConsult> BussinessConsults { get; set; }
        public virtual DbSet<BussinessSurvey> BussinessSurveys { get; set; }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}


}
