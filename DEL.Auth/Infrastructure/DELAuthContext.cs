using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
//using DEL.Auth.Infrastructure.Models_and_Mappings.Models;
using DEL.Auth.Util;

namespace DEL.Auth.Infrastructure
{
    public interface IDELAuthContext
    {
    }

    public partial class DELAuthContext : DbContext, IDELAuthContext
    {
        #region ctor
        static DELAuthContext()
        {
            Database.SetInitializer<DELAuthContext>(null);
        }

        public DELAuthContext()
            : base("Name=DELAuthContext")
        {
            Database.SetInitializer(new DELAuthContextInitializer());
        }
        #endregion

        #region Models
        public DbSet<OfficeProfile> CompanyProfiles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<SubModule> SubModules { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }
        public DbSet<RoleMenuTask> RoleMenuTasks { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProxy> UserProxies { get; set; }
        public DbSet<UserProxyRoleMenu> UserProxyRoleMenus { get; set; }
        public DbSet<UserProxyRoleMenuTask> UserProxyRoleMenuTasks { get; set; }
        public DbSet<DbBackupConfig> DbBackupConfig { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<OwnOffice> OwnOffice { get; set; }
        public DbSet<OwnOfficeBranch> OwnOfficeBranch { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<HrOffice> HrOffice { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<Resources> Resources { get; set; }
        public DbSet<OfficeAssets> OfficeAssets { get; set; }
        public DbSet<Assets> Assets { get; set; }
        //public DbSet<WorkRecord> WorkRecord { get; set; }
        public DbSet<WorkRecordDetails> WorkRecordDetails { get; set; }
        #endregion
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            FluentConfigurator.ConfigureGenericSettings(modelBuilder);
            FluentConfigurator.ConfigureMany2ManyMappings(modelBuilder);
            FluentConfigurator.ConfigureOne2OneMappings(modelBuilder);
        }
    }

    internal class DELAuthContextInitializer : CreateDatabaseIfNotExists<DELAuthContext> //CreateDatabaseIfNotExists<CardiacCareContext>
    {
        protected override void Seed(DELAuthContext context)
        {
            var seedDataPath = AuthSystem.SeedDataPath;
            var dropDb = AuthSystem.DropDB;
            if (string.IsNullOrWhiteSpace(seedDataPath))
                return;
            var folders = Directory.GetDirectories(seedDataPath).ToList().OrderBy(x => x);
            var msg = "";
            bool error = false;
            try
            {
                foreach (var folder in folders)
                {
                    msg += string.Format("processing for: {0}{1}", folder, Environment.NewLine);

                    var fileDir = Path.Combine(new[] { seedDataPath, folder });
                    var sqlFiles = Directory.GetFiles(fileDir, "*.sql").OrderBy(x => x).ToList();
                    foreach (var file in sqlFiles)
                    {
                        try
                        {
                            msg += string.Format("processing for: {0}{1}", file, Environment.NewLine);
                            context.Database.ExecuteSqlCommand(File.ReadAllText(file));
                            msg += string.Format("Done....{0}", Environment.NewLine);
                        }
                        catch (Exception ex)
                        {
                            error = true;
                            msg += string.Format("Failed!....{0}", Environment.NewLine);
                            msg += string.Format("{0}{1}", ex.Message, Environment.NewLine);
                        }
                    }
                }
                if (error)
                {
                    throw new Exception(msg);
                }
                base.Seed(context);
            }
            catch (Exception ex)
            {
                msg = "Error Occured while inserting seed data" + Environment.NewLine;
                if (dropDb)
                {
                    context.Database.Delete();
                    msg += ("Database is droped" + Environment.NewLine);
                }
                var errFile = seedDataPath + "\\seed_data_error.txt";
                msg += ex.Message;
                File.WriteAllText(errFile, msg);

            }
        }

    }
}