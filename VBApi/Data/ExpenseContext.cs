using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VBApi.Data.Models;



namespace VBApi.Data
{
    public class ExpenseContext : DbContext
	{
		public ExpenseContext(DbContextOptions<ExpenseContext> Options) : base(Options)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Takes care of auto incrementing id
            modelBuilder.Entity<Expense>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            // Takes care of schema information
            modelBuilder.Entity<Expense>()
                .ToTable("Expenses", schema: "Test");

            // Set primary keys
            modelBuilder.Entity<Expense>()
                .HasKey(e => new { e.TransactionDate, e.Recipient, e.Currency, e.ExpenseType });

        }

        public DbSet<Expense> Expenses { get; set; }
	}
}
