using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.au
{
    [Table("AU_USRRL")]
    public class UsrRl
    {
        [Key, Column("ID_USER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
      //  [ForeignKey("User")]
        public int IdUser { get; set; }
     //   public User User { get; set; }


        [Column("ID_ROLE")]
        [ForeignKey("Role")]
        public int IdRole { get; set; }
        public Role Role { get; set; }

        [StringLength(20)]
        [Column("USR_AUT", TypeName = "NVARCHAR")]
        public string UsrAut { get; set; }

        [Column("DT_OUT", TypeName = "DATETIME")]
        public DateTime DtOut { get; set; }

        public List<PrmVal> PrmVals { get; set; }
    }
}