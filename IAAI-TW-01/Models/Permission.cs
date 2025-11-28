using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAAI_TW_01.Models
{
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "權限名稱")]
        public string Subject { get; set; }

        [Display(Name = "上一層編號")]
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Permission ParentPermission { get; set; }

        [MaxLength(5)]
        [Display(Name = "權限代號")]
        public string Code { get; set; }

        [MaxLength(500)]
        [Display(Name = "URL")]
        public string Url { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }


        [Display(Name = "ControllerName")]
        public string ControllerName { get; set; }
    }
}