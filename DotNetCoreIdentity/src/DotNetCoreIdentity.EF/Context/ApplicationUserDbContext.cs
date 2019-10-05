using DotNetCoreIdentity.Domain.BlogEntries;
using DotNetCoreIdentity.Domain.Identity;
using DotNetCoreIdentity.Domain.PostTypes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreIdentity.EF.Context
{
    public class ApplicationUserDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : base(options)
        {

        }

        

        // veritabani degisikligi icin migration almaliyiz
        // Package Manager Console icin migration almak(Context'i olusturdugunuz proje default project alaninda secili olmali)
        // Add-Migration MigrationAdi
        // CLI icin(context'i olusturdugunuz projenin dizininde olmalisiniz)
        // dotnet ef migrations add MigrationAdi

        // olusturulan migration ile veritabanina yansitilacak degisimlerin kodlari uretilir. Bu kodlarin veritabaninda calismasi icin asagidaki komutu calistirin
        // Package Manager Console icin migration almak(Context'i olusturdugunuz proje default project alaninda secili olmali)
        // Update-Database
        // CLI icin(context'i olusturdugunuz projenin dizininde olmalisiniz)
        // dotnet ef database update
        /*
         DBSet'ler buraya
         */

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }

    }
}
