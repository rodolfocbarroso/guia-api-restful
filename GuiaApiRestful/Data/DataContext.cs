using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuiaApiRestful.Models;
using Microsoft.EntityFrameworkCore;

namespace GuiaApiRestful.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
