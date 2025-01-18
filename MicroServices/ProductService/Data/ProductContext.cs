using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductService.Models;


namespace ProductService.Data
{
    //Hereda del DbContext que es del EntityFrameworkCore(pquete de referencia)
    public class ProductContext : DbContext
    { 
        //Constructor que va a usar nuestra clase padre. Exprecion options y se pasa a la clase base(al dbcontext)
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        { 

        }
        //Se crear el dbset para el producto
        public DbSet<Product> Products { get; set; } 

        //Se crea un metodo OnModelCreating y se le pasa un parametro que va a ser un ModelBuilder con la siguiente configuracion para mi modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Toma la entidad producto y se establece a la propiedad precio y se le da un columtype decimal de 18 dijitos con 2 decimales
            modelBuilder.Entity<Product>()
            .Property(x => x.Price)
            .HasColumnType("decimal(18,2)");
        }

    }
}