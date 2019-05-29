using CcRep.Models.vk;
using System.Data.Entity;

namespace CcRep.Models._dc
{
    public partial class CcRepContext
    {
        // VK
        public DbSet<HeaderRep> HeaderReps { get; set; }
        public DbSet<CommentsRep> CommentsReps { get; set; }
        public DbSet<BasicRep> BasicReps { get; set; }
        public DbSet<ClientRep> ClientReps { get; set; }
        public DbSet<IssuerRep> IssuerReps { get; set; }
        public DbSet<NoticeRep> NoticeReps { get; set; }
        public DbSet<AddRep> AddReps { get; set; }
        public DbSet<Rep406> Rep406s { get; set; }
        public DbSet<SupdocRep> SupdocReps { get; set; }
        public DbSet<AddInfRep> AddInfReps { get; set; }
    }
}