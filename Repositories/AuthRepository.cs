using login_app.DTOs;
using login_app.Interfaces;
using login_app.Models;
using Microsoft.EntityFrameworkCore;

namespace login_app.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _dbContext;

        public AuthRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User?> CreateUser(User userDetails)
        {
            try
            {              
                await _dbContext.Users.AddAsync(userDetails);
                await _dbContext.SaveChangesAsync();
                return userDetails;
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<User?> GetUserByName(string? userName)
        {
            try
            {
                var user = await _dbContext.Users.Where(q => q.Username != null && userName != null && q.Username.ToLower() == userName.ToLower()).FirstOrDefaultAsync();
                return user;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
