using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    //Enity Status Description
    //Active: The row of the entity always active and shown into the table
    //InActive: The row of the entity not activated but could be searched from the UI.
    //Deleted: The row of the entity not activated and therel wouldn't be any connection from the front end. The row not to be deleted from the entity.
    public enum EntityStatus { Active = 1, Inactive = 0, Deleted = -1 }
    public class Entity
    {
        public Entity()
        {
            Status = EntityStatus.Active;
            CreateDate = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        // audit fields 
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditDate { get; set; }
        public long? EditedBy { get; set; }
        public EntityStatus Status { get; set; }
    }
}
