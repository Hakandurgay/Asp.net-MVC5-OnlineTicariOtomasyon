namespace MVC5OnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC5OnlineTicariOtomasyon.Models.Siniflar.Context>
    {
        public Configuration()
        {
            //önce enable-migrations yazılır. ondan sonra bu class oluşturulur
            AutomaticMigrationsEnabled = true;  //false true yapılır. package manager console'a update-database yazılır. bundan sonra sqle atar veritabanını
        }

        protected override void Seed(MVC5OnlineTicariOtomasyon.Models.Siniflar.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
