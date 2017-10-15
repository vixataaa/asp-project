using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Models;
using SecondHand.Data.Models.Contracts;

namespace SecondHand.Data
{
    public class MsSqlDbContext : IdentityDbContext<ApplicationUser>
    {
        public MsSqlDbContext()
            : base("LocalConnection", throwIfV1Schema: false)
        {            
        }

        public IDbSet<Advertisement> Advertisements { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Photo> Photos { get; set; }

        public IDbSet<Chat> Chats { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<ChatNotification> Notifications { get; set; }

        public static MsSqlDbContext Create()
        {
            return new MsSqlDbContext();
        }
        
        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditable)entry.Entity;
                if (entry.State == EntityState.Added && !entity.CreatedOn.HasValue)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
