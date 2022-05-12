using Microsoft.EntityFrameworkCore;
using TicketGenerator.Models;

namespace TicketGenerator.Services
{
    public class TickectGenerate : IService<Tickect, int>
    {
        private readonly TickectCreatingDbContext ctx;

        public TickectGenerate(TickectCreatingDbContext ctx)
        {
            this.ctx = ctx;
        }

        async Task<Tickect> IService<Tickect, int>.CreateAsync(Tickect entity)
        {
            var res = await ctx.Tickects.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        async Task<IEnumerable<Tickect>> IService<Tickect, int>.Get()
        {
            return await ctx.Tickects.ToListAsync();
        }

        Task<Tickect> IService<Tickect, int>.GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public int RolesbyUserId(int UserID)
        {
            var UserRole = ctx.UserRoles.Where(U => U.Uid == UserID).FirstOrDefault();
            if(UserRole == null)
            {
                return 0;
            }
            return UserRole.RoleId;
        }
       


        
    }
}
