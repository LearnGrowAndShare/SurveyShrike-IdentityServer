
/// <summary>
/// Developed by Ankit
/// It contains the code required for the DB migrations based on db context definations.
/// </summary>
namespace SurveyShrike_IdentityServer.Persistence.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class DesignTimeDbContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        protected const string ConnectionStringName = "IdentityServer";
        protected const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public TContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}SurveyShrike-IdentityServer", Path.DirectorySeparatorChar);
            return Create(basePath, Environment.GetEnvironmentVariable(AspNetCoreEnvironment));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        protected TContext Create(string basePath, string environmentName)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            return Create(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            optionsBuilder.UseSqlite(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}
