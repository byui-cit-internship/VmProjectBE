using Microsoft.EntityFrameworkCore;
using VmProjectBE.Models;



namespace VmProjectBE.DAL
{
    public class VmEntities : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
        public VmEntities(DbContextOptions<VmEntities> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<Cookie> Cookies { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<FolderUser> FolderUsers { get; set; }
        public DbSet<Pool> Pools { get; set; }
        public DbSet<PoolMembership> PoolMemberships { get; set; }
        public DbSet<IpAddress> IpAddresses { get; set; }
        public DbSet<ResourcePool> ResourcePools { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<SessionToken> SessionTokens { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagCategory> TagCategories { get; set; }
        // public DbSet<TagUser> TagUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSectionRole> UserSectionRoles { get; set; }
        public DbSet<Vlan> Vlans { get; set; }
        public DbSet<VlanVswitch> VlanVswitches { get; set; }
        public DbSet<VmInstance> VmInstances { get; set; }
        public DbSet<VmInstanceIpAddress> VmInstanceIpAddresses { get; set; }
        public DbSet<VmInstanceTag> VmInstanceTags { get; set; }
        public DbSet<VmInstanceVswitch> VmInstanceVswitches { get; set; }
        public DbSet<Vswitch> Vswitches { get; set; }
        public DbSet<VswitchTag> VswitchTags { get; set; }

        public object Configuration { get; }
    }
}