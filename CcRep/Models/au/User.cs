using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.au
{
    [Table("AU_USER")]
    public class User
    {
        [Column("ID_USER")]
        public int Id { get; set; }

        [StringLength(255)]
        [Column("USER_NAME", TypeName = "NVARCHAR")]
        public string UserName { get; set; } // Login User's

        [StringLength(64)]
        [Column("USER_PWD", TypeName = "NVARCHAR")]
        public string UserPwd { get; set; }  // Пароль внутреннего пользователя

        [StringLength(255)]
        [Column("SURNAME", TypeName = "NVARCHAR")]
        public string SurName { get; set; }

        [StringLength(255)]
        [Column("FIRSTNAME", TypeName = "NVARCHAR")]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Column("PATRONYMIC", TypeName = "NVARCHAR")]
        public string Patronymic { get; set; } // отчествов

        [StringLength(4)]
        [Column("FILIAL", TypeName = "NVARCHAR")]
        public string Filial { get; set; }

        [Column("CREATE_DT", TypeName = "DATE")]
        public DateTime CreateDt { get; set; }

        [Column("END_DT", TypeName = "DATE")]
        public DateTime EndDt { get; set; }

        [Column("LOCKED", TypeName = "NVARCHAR")]
        public char Locked { get; set; } // пользователь активный (=0)/заблокирован (=1)

        [Column("SEC_TYPE", TypeName = "NVARCHAR")]
        public char SecType { get; set; } // тип доступа: внешний (=1, вход через LDAP)/внутренний (=0)

        public ICollection<PrmVal> PrmVals { get; set; }
        public ICollection<UsrRl> UsrRls { get; set; }
    }
}