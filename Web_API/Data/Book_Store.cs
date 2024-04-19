using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web_API.Data
{
    public class Book_Store : IdentityDbContext<Application_User>
    {
        public Book_Store(DbContextOptions<Book_Store> opt) : base(opt)
        {

        }
        #region
        public DbSet<Book>? Books { get; set; }
        #endregion

    }
}
