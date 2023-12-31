﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheBookshelf.Models;

namespace TheBookshelfWeb.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Sci-Fi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 },
                new Category { Id = 4, Name = "Fantasy", DisplayOrder = 4 },
                new Category { Id = 5, Name = "Horror", DisplayOrder = 5 },
                new Category { Id = 6, Name = "Romance", DisplayOrder = 6 },
                new Category { Id = 7, Name = "Mystery", DisplayOrder = 7 },
                new Category { Id = 8, Name = "Drama", DisplayOrder = 8 }
                );

            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Tech Solution", StreetAddress = "123 Tech St", City = "Tech City", PostalCode = "12121", State = "IL", PhoneNumber = "1112223333" },
                new Company { Id = 2, Name = "Vivid Books", StreetAddress = "999 Vid St", City = "Vid City", PostalCode = "56565", State = "NY", PhoneNumber = "4445556666" },
                new Company { Id = 3, Name = "Readers Club", StreetAddress = "999 Main St", City = "Test City", PostalCode = "44003", State = "CA", PhoneNumber = "7778889999" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Holly",
                    Author = "Stephen King",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis faucibus accumsan nulla, non ultrices sem. Donec at felis ut turpis condimentum elementum.",
                    ISBN = "9788556511928",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 24,
                    Price100 = 21,
                    CategoryId = 5,
                },
                new Product
                {
                    Id = 2,
                    Title = "The Eye of the World",
                    Author = "Robert Jordan",
                    Description = "Suspendisse eu magna vitae ex consequat placerat. Morbi vel pretium nunc, in imperdiet dui...",
                    ISBN = "0812511816",
                    ListPrice = 8,
                    Price = 7,
                    Price50 = 6,
                    Price100 = 5.5,
                    CategoryId = 4,
                },
                new Product
                {
                    Id = 3,
                    Title = "A Game of Thrones: The Illustrated Edition: A Song of Ice and Fire: Book One",
                    Author = "George R. R. Martin",
                    Description = "Donec eu dapibus arcu. Praesent egestas, sem sed auctor malesuada, sapien ligula dignissim urna, sed feugiat ante massa id nisl.",
                    ISBN = "0553808044",
                    ListPrice = 35,
                    Price = 32,
                    Price50 = 28,
                    Price100 = 25,
                    CategoryId = 4,
                },
                new Product
                {
                    Id = 4,
                    Title = "Fairy Tale",
                    Author = "Stephen King",
                    Description = "Fusce accumsan orci diam, sed commodo massa gravida id. Nunc ut ante cursus, scelerisque nisl sit amet, ornare eros.",
                    ISBN = "9781668002179",
                    ListPrice = 17,
                    Price = 15,
                    Price50 = 14,
                    Price100 = 12,
                    CategoryId = 4,
                },
                new Product
                {
                    Id = 5,
                    Title = "Assistant to the Villian",
                    Author = "Hannah Nicole Maehrer",
                    Description = "Ut mattis placerat odio, et lobortis ipsum volutpat ut. Nullam laoreet metus eu urna pellentesque auctor. Pellentesque et diam nec nisl dictum tincidunt.",
                    ISBN = "9788556511928",
                    ListPrice = 17,
                    Price = 16,
                    Price50 = 14,
                    Price100 = 11,
                    CategoryId = 6,
                },
                new Product
                {
                    Id = 6,
                    Title = "House of Flame and Shadow",
                    Author = "Sarah J. Maas",
                    Description = "Sed maximus purus eget libero placerat, accumsan viverra nisl pulvinar. Ut arcu nulla, feugiat at metus vel, volutpat iaculis quam.",
                    ISBN = "9781639732869",
                    ListPrice = 29,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 22,
                    CategoryId = 4,
                },
                new Product
                {
                    Id = 7,
                    Title = "The River We Remember",
                    Author = "William Kent Krueger",
                    Description = "Donec ipsum mauris, rutrum ut arcu in, imperdiet euismod orci. Fusce et odio quis tellus feugiat luctus.",
                    ISBN = "9781668047903",
                    ListPrice = 21,
                    Price = 20,
                    Price50 = 18,
                    Price100 = 17,
                    CategoryId = 7,
                },
                new Product
                {
                    Id = 8,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Description = "Curabitur sodales sodales lectus, vel feugiat arcu aliquam eget. Pellentesque eu turpis eu nunc aliquam dignissim.",
                    ISBN = "0446310786",
                    ListPrice = 13,
                    Price = 11,
                    Price50 = 10,
                    Price100 = 9,
                    CategoryId = 8,
                }
                );
        }
    }
}
