using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CcRep.Models.au
{
    [Table("AU_ROLE")]
    public class Role
    {
        [Key, Column("ID_ROLE")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int IdRole { get; set; }

        [StringLength(70)]
        [Required]
        [Column("ROLE_NAME", TypeName = "NVARCHAR")]
        public string RoleName { get; set; }

        public List<UsrRl> UsrRls { get; set; }
    }
}