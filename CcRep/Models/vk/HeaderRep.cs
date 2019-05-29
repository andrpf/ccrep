using CcRep.Components.UserAuditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Mvc;

namespace CcRep.Models.vk
{
    [Table("VK_HEADER_REP")]
    public class HeaderRep : IUserAuditing, IDateAuditing
    {
        public HeaderRep()
        {
            AddInfReps = new List<AddInfRep>();

            // По умолчанию отчет создается за прошлый месяц
            BeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            EndDate = BeginDate.AddMonths(1).AddDays(-1);
        }

        [Key, Column("ID_REP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ICollection<AddInfRep> AddInfReps { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayName("Название")]
        [NotMapped]
        public string Name
        {
            get
            {
                return BeginDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("ru")) +
                    " " +
                    BeginDate.Year.ToString();
            }
            private set { }
        }

        [Column("BEGIN_DATE", TypeName = "DATE")]
        [Required]
        [Display(Name = "Начало")]
        [Index("IX_Name_Begin_End_Dates", 0, IsUnique = true)]
        [Remote("CheckEndBeginDate", "api/HeaderReps", areaName: "Reports", AdditionalFields = "EndDate", ErrorMessage = "Даты заполнены неправильно")]
        public DateTime BeginDate { get; set; }

        [Column("END_DATE", TypeName = "DATE")]
        [Required]
        [Display(Name = "Конец")]
        [Index("IX_Name_Begin_End_Dates", 1, IsUnique = true)]
        public DateTime EndDate { get; set; }

        [ForeignKey("UserLastEdited")]
        [Column("LAST_EDITED_BY")]
        public string UserLastEditedId { get; set; }
        public virtual CcRepUser UserLastEdited { get; set; }

        [ForeignKey("UserCreated")]
        [Column("CREATED_BY")]
        public string UserCreatedId { get; set; }
        public virtual CcRepUser UserCreated { get; set; }

        [ForeignKey("UserArch")]
        [Column("USER_ARH", TypeName = "NVARCHAR")]
        public string StatusUserArh { get; set; }
        public virtual CcRepUser UserArch { get; set; }

        [Display(Name = "Дата создания")]
        [Column("CREATE_DATE", TypeName = "DATETIME")]
        public DateTime? CreateDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [NotMapped]
        [Display(Name = "Статус")]
        public string Status
        {
            get
            {
                return ArchiveDate.HasValue ? "ARCHIVE" : "CURRENT";
            }
            private set { }
        }

        [Display(Name = "Дата архивации")]
        [Column("ARCHIVE_DATE", TypeName = "DATE")]
        public DateTime? ArchiveDate { get; set; }

        [StringLength(100)]
        [Display(Name = "Комментарий")]
        [Column("COMMENT", TypeName = "NVARCHAR")]
        public string Comment { get; set; }
    }
}