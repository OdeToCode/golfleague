﻿using BeaverLeague.Data;
using BeaverLeague.Tests.Helpers;
using BeaverLeague.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BeaverLeague.Tests.Pages
{
    public class BeaverLeagueWebFactory : WebApplicationFactory<Startup>
    {
        public  virtual string Name => GetType().FullName ?? nameof(BeaverLeagueWebFactory);

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<LeagueDbContext>(options =>
                {
                    options.UseInMemoryDatabase(Name);
                });
            });
        }

        protected override void Dispose(bool disposing)
        {
            using var scope = Services.GetScopedDbContext<LeagueDbContext>();
            scope.Db.Database.EnsureDeleted();
        }
    }
}
