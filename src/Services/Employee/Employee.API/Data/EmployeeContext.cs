using Microsoft.EntityFrameworkCore;
using Employee.API.Entities;

namespace Employee.API.Data
{
	public class EmployeeContext : DbContext
	{
		protected readonly IConfiguration _configuration;

		public EmployeeContext(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public DbSet<EmployeeModel> Employees { get; set; } = null!;
	
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<EmployeeModel>(e =>
			{
				e.Property(e => e.Id).HasColumnName("id");
				e.Property(e => e.UserId).HasColumnName("userid");
				e.Property(e => e.Region).HasColumnName("region");
				e.Property(e => e.Level).HasColumnName("level");
				e.Property(e => e.Title).HasColumnName("title");
				e.Property(e => e.Division).HasColumnName("division");
				e.Property(e => e.Position).HasColumnName("position");
				e.Property(e => e.Designation).HasColumnName("designation");
				e.ToTable("employees")
					.HasIndex(x => x.Id)
					.IsUnique();
			});
			
			base.OnModelCreating(modelBuilder);
		}
	}
}
