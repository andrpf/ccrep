using CcRep.Components.UserAuditing;
using CcRep.Models._db;
using CcRep.Models.adm;
using CcRep.Models.dic;
using CcRep.Models.etl;
using CcRep.Models.out_;
using CcRep.Models.vk;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CcRep.Models._dc
{
    public partial class CcRepContext : IdentityDbContext<CcRepUser, CcRepRole, string, CcRepUserLogin, UsersToRoles, CcRepUserClaim>
    {
        public CcRepContext() : base("DefaultConnection")
        {
             Database.SetInitializer(new CcRepContextDBInitializer());
          //  Database.SetInitializer(new CcRepTestContextDBInitializer());
        }

        public static CcRepContext Create()
        {
            return new CcRepContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Properties<string>().Configure(p => p.IsUnicode(true));

            removeUnnecessaryUserFields(modelBuilder);

            modelBuilder.Entity<BasicRep>()
               .Property(p => p.Share)
               .HasPrecision(18, 6);

            modelBuilder.Entity<BODwh>()
                .Property(p => p.PartsAmt)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Tbcvk>()
              .Property(p => p.AmountCD)
              .HasPrecision(16, 2);

            changeDefaultTablesNames(modelBuilder);

            modelBuilder.Entity<CcRepRole>().HasKey(p => p.Id);
        }

        public override int SaveChanges()
        {
            AuditingUtilities.AddAuditingData(this.ChangeTracker.Entries());

            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync()
        {
            AuditingUtilities.AddAuditingData(this.ChangeTracker.Entries());

            return await base.SaveChangesAsync();
        }

        // ASP
        public DbSet<CcRepUserLogin> UserLogins { get; set; }
        public DbSet<CcRepUserClaim> UserClaims { get; set; }
        public DbSet<UsersToRoles> UserRoles { get; set; }
        public override IDbSet<CcRepRole> Roles { get; set; }

        // ADM
        public DbSet<DwhLoad> DwhLoads { get; set; }
        public DbSet<CheckList> CheckLists { get; set; }
        public DbSet<CheckStep> CheckSteps { get; set; }
        public DbSet<CheckHeader> CheckHeaders { get; set; }
        public DbSet<CheckError> CheckErrors { get; set; }

        // ETL
        public DbSet<Dwhs> DWHSs { get; set; }
        public DbSet<BODwh> BODwhs { get; set; }
        public DbSet<BO_3130_406> BO_3130_406s { get; set; }
        public DbSet<BO_3132> BO_3132s { get; set; }
        public DbSet<Tbcvk> Tbcvks { get; set; }

        // OUT
        public DbSet<Log> Logs { get; set; }
        public DbSet<UsersToFilials> UserrsToFilials { get; set; }

        private void removeUnnecessaryUserFields(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CcRepUser>()
                .Ignore(p => p.PhoneNumber)
                .Ignore(p => p.PhoneNumberConfirmed)
                .Ignore(p => p.EmailConfirmed)
                .Ignore(p => p.LockoutEnabled)
                .Ignore(p => p.LockoutEndDateUtc)
                .Ignore(p => p.AccessFailedCount)
                .Ignore(p => p.TwoFactorEnabled);
        }

        private void changeDefaultTablesNames(DbModelBuilder modelBuilder)
        {
            // Change many-to-many relation table
            modelBuilder.Entity<AcctReport>()
                .HasMany(c => c.ClientTypes).WithMany(i => i.AcctReports)
                .Map(t => t.MapLeftKey("ACCT_REPORT_ID")
                .MapRightKey("CLIENT_TYPE_ID")
                .ToTable("DIC_ACCT_REPORT_TO_CLIENT_TYPE"));

            modelBuilder.Entity<SettingCodeVO>()
                .HasMany(c => c.CodeToolings).WithMany(i => i.SettingCodeVOs)
                .Map(t => t.MapLeftKey("SETTING_CODE_VO_ID")
                .MapRightKey("CODE_TOOLING_ID")
                .ToTable("DIC_SETTING_CODE_VO_TO_CODE_TOOLINGS"));

            modelBuilder.Entity<TypeClient>()
                .HasMany(c => c.StatusRezs).WithMany(i => i.TypeClients)
                .Map(t => t.MapLeftKey("TYPE_CLIENT")
                .MapRightKey("STATUS")
                .ToTable("DIC_TYPE_CLIENT_TO_STATUS"));

            // Change default asp.net mvc identity tables names
            modelBuilder.Entity<CcRepUser>().ToTable("ASP_USERS");
            modelBuilder.Entity<CcRepRole>().ToTable("ASP_ROLES");
            modelBuilder.Entity<UsersToRoles>().ToTable("ASP_USERS_TO_ROLES");
            modelBuilder.Entity<CcRepUserClaim>().ToTable("ASP_USER_CLAIMS");
            modelBuilder.Entity<CcRepUserLogin>().ToTable("ASP_USER_LOGINS");
        }
    }
}