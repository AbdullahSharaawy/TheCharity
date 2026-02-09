using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheCharityDAL.Entities;

namespace TheCharityDAL.Database
{
    public class TheCharityDbContext : IdentityDbContext<User>
    {
        public TheCharityDbContext(DbContextOptions<TheCharityDbContext> options) : base(options)
        { }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<DonatedItem> DonatedItems { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationContactMethod> OrganizationContactMethods { get; set; }
        public DbSet<PaymentInfo> PaymentsInfo { get; set; }
        public DbSet<SharedCampaign> SharedCampaigns { get; set; }
        public DbSet<SoloCampaign> SoloCampaigns { get; set; }
        public DbSet<UserContactMethod> UserContactMethods { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 1. Campaign TPH with explicit table name
            builder.Entity<Campaign>()
                .ToTable("Campaigns")
                .HasDiscriminator<string>("CampaignType")
                .HasValue<SharedCampaign>("Shared")
                .HasValue<SoloCampaign>("Solo");

            // 2. OPTIONAL but RECOMMENDED: Global query filters for soft delete
            builder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            builder.Entity<Campaign>().HasQueryFilter(c => !c.IsDeleted);
            builder.Entity<Organization>().HasQueryFilter(o => !o.IsDeleted);

            // 3. OPTIONAL: Explicit foreign key configuration (prevents cascade delete issues)
            builder.Entity<SoloCampaign>()
            .HasOne(sc => sc.Organization)
            .WithMany(o => o.SoloCampaigns)
            .HasForeignKey(sc => sc.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

            // 4. OPTIONAL: Configure join table name for many-to-many
            builder.Entity<SharedCampaign>()
                .HasMany(sc => sc.Organizations)
                .WithMany(o => o.SharedCampaigns)
                .UsingEntity(j => j.ToTable("SharedCampaignOrganizations"));
        }
    }
}
