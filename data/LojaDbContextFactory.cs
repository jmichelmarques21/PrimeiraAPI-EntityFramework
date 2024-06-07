using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Loja.data{
    public class LojaDbContextFactory : IDesignTimeDbContextFactory<LojaDbContext>{
        public LojaDbContext CreateDbContext(string[] args){
            var optionsBuilder = new DbContextOptionsBuilder<LojaDbContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 2)));

            return new LojaDbContext(optionsBuilder.Options);

        }
    }
}

