using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SARITASA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARITASA.DataAccess
{
    public class ApplicationDbContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        //Configure Dbset and Role, User, Entity 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(entity => { entity.ToTable(name: "User"); });

            #region Identity config
            builder.Entity<IdentityRole<Guid>>()
                .ToTable("Role");

            builder.Entity<IdentityUserRole<Guid>>()
                .ToTable("User_Role")
                .HasKey(x => new { x.UserId, x.RoleId });

            builder.Entity<IdentityUserClaim<Guid>>()
                .ToTable("User_Claim")
                .HasKey(x => x.Id);

            builder.Entity<IdentityUserLogin<Guid>>()
                .ToTable("User_Login")
                .HasKey(x => x.UserId);

            builder.Entity<IdentityUserToken<Guid>>()
                .ToTable("User_Token")
                .HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

            builder.Entity<IdentityRoleClaim<Guid>>()
                .ToTable("Role_Claim")
                .HasKey(x => x.Id);
            #endregion
        }
    }
}
