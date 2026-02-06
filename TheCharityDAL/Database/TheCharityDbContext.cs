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
        public override DbSet<User> Users { get; set; }
        public DbSet<UserContactMethod> UserContactMethods { get; set; }
    }
}
