using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
   public class MyDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
}
}