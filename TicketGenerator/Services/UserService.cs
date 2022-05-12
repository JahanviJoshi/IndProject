using Microsoft.EntityFrameworkCore;
using TicketGenerator.Models;

namespace TicketGenerator.Services
{
    public class UserService : IService<Login, int>
    {
        private readonly TickectCreatingDbContext ctx;

        public UserService(TickectCreatingDbContext ctx)
        {
            this.ctx = ctx;
        }

       async Task<Login> IService<Login, int>.CreateAsync(Login entity)
        {
            var res = await ctx.Logins.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

       async Task<IEnumerable<Login>> IService<Login, int>.Get()
        {
           var res=  await ctx.Logins.ToListAsync();
            return res;
        }

        Task<Login> IService<Login, int>.GetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
