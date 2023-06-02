using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ShopContext db = new ShopContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                User x1 = new User {email = "setanta@umail.ru", UserLogin = "Setanta", Password = "GaerBolg", Money = 0, role=(int)roles.user};
                User Admin = new User { email = "Admin@umail.ru", UserLogin = "Admin", Password = "Admin", Money = 1000000, role = (int)roles.admin };
                User sedrik = new User { email = "Sedrik@umail.ru", UserLogin = "Sedrik", Password = "54321", Money = 1500, role = (int)roles.author };
                db.Users.Add(x1);
                db.Users.Add(Admin);
                db.Users.Add(sedrik);
                db.SaveChanges();
            }
        }
    }
    public enum roles
    {
        user,
        author,
        admin

    }
    public class User : IdentityDbContext
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string UserLogin { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Money { get; set; }
        [Required]
        public int role { get; set; }
        public List<PurchaseList> PurchaseList { get; set; } = new();
        public List<Author> Author { get; set; } = new();
        public List<PurchaseListA> PurchaseListA { get; set; } = new();
    }

    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public string BookDescription { get; set; }
        [Required]
        public string BookAuthor { get; set; }
        [Required]
        public string BookDate { get; set; }
        [Required]
        public string BookGenre { get; set; }
        [Required]
        public int BookPrice { get; set; }
        [Required]
        public string BookLenght { get; set; }
        [Required]
        public int PublishingHouseId { get; set; }
        [ForeignKey(nameof(PublishingHouseId))]
        public PublishingHouse PublishingHouse { get; set; }
        public List<PurchaseList> PurchaseList { get; set; } = new();
    }
    public class PurchaseList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseId { get; set; }
        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        public int? BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book? Book { get; set; }
    }

    public class PublishingHouse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublishingHouseId  { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public double procent { get; set; }
        public List<Book> Book { get; set; } = new();
    }

    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Familiya { get; set; }
        [Required]
        public string Otchestvo { get; set; }
        [Required]
        public double procent { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public List<AuthorBook> AuthorBooks { get; set; }
    }

    public class AuthorBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorBookId { get; set; }
        public string BookName { get; set; }
        [Required]
        public string BookDescription { get; set; }
        [Required]
        public string BookDate { get; set; }
        [Required]
        public string BookGenre { get; set; }
        [Required]
        public int BookPrice { get; set; }
        [Required]
        public string BookLenght { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }
        public List<PurchaseListA> PurchaseListA { get; set; } = new();
    }

    public class PurchaseListA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseId { get; set; }
        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        public int? AuthorBookId { get; set; }
        [ForeignKey(nameof(AuthorBookId))]
        public AuthorBook? AuthorBook { get; set; }
    }




    public class ShopContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<PurchaseList> PurchaseLists => Set<PurchaseList>();
        public DbSet<PublishingHouse> PublishingHouses => Set<PublishingHouse>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<AuthorBook> AuthorBooks => Set<AuthorBook>();
        public DbSet<PurchaseListA> PurchaseListA => Set<PurchaseListA>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ShopBase;Trusted_Connection=True;");
        }
    }
}