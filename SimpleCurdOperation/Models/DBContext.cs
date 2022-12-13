using Microsoft.EntityFrameworkCore;

namespace SimpleCurdOperation.Models
{
    public class DBContext
    {
        private DbContextOptions<StudentContext> options;

        public DBContext(DbContextOptions<StudentContext> options)
        {
            this.options = options;
        }
    }
}