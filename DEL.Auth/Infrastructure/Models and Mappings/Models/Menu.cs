using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("Menu")]
    public class Menu : Entity
    {
        public Menu()
        {
            this.Roles = new List<Role>();
            this.Users = new List<User>();
        }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int Sl { get; set; }
        public string Url { get; set; }
        public string HeadingText { get; set; }
        public string NoteHtml { get; set; }
        public long SubModuleId { get; set; }

        [ForeignKey("SubModuleId")]
        public virtual SubModule SubModule { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
    //[Table("MenuReport")]
    //public class MenuReport : Entity
    //{
    //    public long? ParentId { get; set; }
    //    [ForeignKey("ParentId")]
    //    public virtual MenuReport Parent { get; set; }
    //    public string MenuCode { get; set; }
    //    public string ModuleCode { get; set; }
    //    public string MenuName { get; set; }
    //    public bool Visible { get; set; }
    //    public int MenuSequence { get; set; }
    //    public string MenuType { get; set; }
    //    public string ImagePath { get; set; }
    //    public string Target { get; set; }
    //    public string ToolTip { get; set; }
    //    public string UserLevel { get; set; }
    //    public string OperationLevel { get; set; }
    //    public string florabank_ID { get; set; }
    //    public int Depth { get; set; }
    //    public bool Expanded { get; set; }
    //    public string NavigateUrl { get; set; }
    //    public string MenuNameIslamic { get; set; }
    //    public string IslamicYN { get; set; }
    //    public string ToolTipIslamic { get; set; }
    //    public string servertype { get; set; }        
    //    public virtual ICollection<MenuReport> Childs { get; set; }
    //}
}
