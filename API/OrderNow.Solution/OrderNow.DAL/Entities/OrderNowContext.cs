using System.Data.Entity; 
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Ef6;

namespace OrderNow.DAL.Entities
{
    public class OrderNowContext : DataContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; } 
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestDetail> RequestDetails { get; set; }
        public DbSet<Package> Packages { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryTranslation> CountryTranslations { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionTranslation> RegionTranslations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CityTranslation> CityTranslations { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemTranslation> ItemTranslations { get; set; } 
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantTranslation> RestaurantTranslations { get; set; } 
        public DbSet<Size> Sizes { get; set; }
        public DbSet<SizeTranslation> SizeTranslations { get; set; }

        public DbSet<SideItem> SideItems { get; set; }
        public DbSet<SideItemTranslation> SideItemTranslations { get; set; }
        public DbSet<ItemSideItem> ItemSideItems { get; set; }
        public DbSet<ItemSize> ItemSizes { get; set; }
        public DbSet<Background> Backgrounds { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<AreaTranslation> AreaTranslations { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchTranslation> BranchTranslations { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; } 
        public DbSet<Table> Tables { get; set; }

        public OrderNowContext() : base("name=OrderNowDB")
        {
            Database.SetInitializer<OrderNowContext>(null);

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          
            modelBuilder.Entity<RequestDetail>()
                .HasRequired(c => c.Request)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Package>()
                .HasRequired(c => c.Admin)
                .WithMany()
                .WillCascadeOnDelete(false);
             

            modelBuilder.Entity<Size>()
                .HasRequired(c => c.Restaurant)
                .WithMany()
                .WillCascadeOnDelete(false);  

            modelBuilder.Entity<Restaurant>()
                .HasRequired(c => c.RestaurantAdmin)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FeedBack>()
                .HasRequired(c => c.Branch)
                .WithMany()
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Branch>()
            //    .HasRequired(c => c.Area)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
        }
    }
}
