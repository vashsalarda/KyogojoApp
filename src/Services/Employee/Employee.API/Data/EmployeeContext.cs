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
		public DbSet<User> Users { get; set; } = null!;
	
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
				e.Property(e => e.ContactNumber).HasColumnName("contactnumber");
				e.Property(e => e.Email).HasColumnName("email");
				e.Property(e => e.Branch).HasColumnName("branch");
				e.Property(e => e.City).HasColumnName("city");
				e.Property(e => e.Province).HasColumnName("province");
				e.Property(e => e.Region).HasColumnName("region");
				e.Property(e => e.Title).HasColumnName("title");
				e.Property(e => e.Department).HasColumnName("department");
				e.Property(e => e.Position).HasColumnName("position");
				e.Property(e => e.Designation).HasColumnName("designation");
				e.Property(e => e.DateHired).HasColumnName("datehired");
				e.Property(e => e.SSS).HasColumnName("sss");
				e.Property(e => e.PHIC).HasColumnName("phic");
				e.Property(e => e.PAGIBIG).HasColumnName("pagibig");
				e.Property(e => e.TIN).HasColumnName("tin");
				e.Property(e => e.BankName).HasColumnName("bankname");
				e.Property(e => e.AccountName).HasColumnName("accountname");
				e.Property(e => e.AccountNumber).HasColumnName("accountnumber");
				e.ToTable("employees")
					.HasIndex(x => x.Id)
					.IsUnique();
			});

			// modelBuilder.Entity<EmployeeModel>().HasOne(u => u.UserId);

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>(e =>
			{
				e.Property(e => e.Id).HasColumnName("id");
				e.Property(e => e.Email).HasColumnName("email");
				e.Property(e => e.FirstName).HasColumnName("firstname");
				e.Property(e => e.LastName).HasColumnName("lastname");
				e.Property(e => e.MobileNumber).HasColumnName("mobilenumber");
				e.Property(e => e.Country).HasColumnName("country");
				e.Property(e => e.Province).HasColumnName("province");
				e.Property(e => e.City).HasColumnName("city");
				e.Property(e => e.Role).HasColumnName("role");
				e.Property(e => e.Designation).HasColumnName("designation");
				e.Property(e => e.Password).HasColumnName("password");
				e.ToTable("users")
					.HasIndex(x => x.Email)
					.IsUnique();
			});
		}
		
	}
}
