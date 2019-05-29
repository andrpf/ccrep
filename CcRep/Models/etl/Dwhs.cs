using CcRep.Models.adm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CcRep.Models.etl
{
    [Table("ETL_DWHS", Schema = "etl")]
    public class Dwhs
    {
        public enum ParamNames{
            CCREP_DWH_UNLOAD
        }

        [Column("ID_KEY")]
        public int Id { get; set; }

        [System.ComponentModel.DefaultValue("CCREP_DWH_UNLOAD")]
        [Column("PARAM_NAME", TypeName = "NVARCHAR"), StringLength(80)]
        public string ParamName { get; set; }

        [StringLength(1)]
        [Column("PARAM_VALUE", TypeName = "CHAR")]
        public string ParamValue { get; set; }

        [Column("START_LOAD")]
        public DateTime? StartLoad { get; set; }

        [Column("END_LOAD")]
        public DateTime? EndLoad { get; set; }

        [Column("ID_REP")]
        public int? IdRep { get; set; }
        //      public HeaderRep HeaderRep { get; set; }

        [StringLength(20)]
        [Column("LAUNCH_AUTHOR", TypeName = "NVARCHAR")]
        public string LaunchAuthor { get; set; }

        List<DwhLoad> DwhLoads { get; set; }
    }
}