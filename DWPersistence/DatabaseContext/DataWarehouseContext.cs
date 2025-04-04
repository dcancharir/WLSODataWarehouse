using DWDomain;
using Microsoft.EntityFrameworkCore;

namespace DWPersistence.DataBaseContext;
public partial class DataWarehouseContext : DbContext{
    public DataWarehouseContext(DbContextOptions<DataWarehouseContext> options) : base(options)
    {
    }
    public virtual DbSet<DWAssociate> DWAssociates { get; set; }

    public virtual DbSet<DWBrand> DWBrands { get; set; }

    public virtual DbSet<DWCustomer> DWCustomers { get; set; }

    public virtual DbSet<DWCustomersGroup> DWCustomersGroups { get; set; }

    public virtual DbSet<DWGame> DWGames { get; set; }

    public virtual DbSet<DWGroupsx> DWGroupsxes { get; set; }

    public virtual DbSet<DWPaymentMethod> DWPaymentMethods { get; set; }

    public virtual DbSet<DWPlayer> DWPlayers { get; set; }

    public virtual DbSet<DWProcessor> DWProcessors { get; set; }

    public virtual DbSet<DWProvider> DWProviders { get; set; }

    public virtual DbSet<DWRealGameEvent> DWRealGameEvents { get; set; }

    public virtual DbSet<DWStore> DWStores { get; set; }

    public virtual DbSet<DWStoreTx> DWStoreTxs { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<DWAssociate>(entity => {
            entity.HasKey(e => e.Ruc);
        });
        modelBuilder.Entity<DWBrand>(entity => {
            entity.HasKey(e => e.BrandId);
        });
        modelBuilder.Entity<DWCustomer>(entity => {
        });
        modelBuilder.Entity<DWCustomersGroup>(entity => {
            entity.HasKey(e => new { e.PlayerId, e.GroupId });
        });
        modelBuilder.Entity<DWGame>(entity => {
            entity.HasKey(e => new { e.GameId, e.ProviderId });
        });
        modelBuilder.Entity<DWGroupsx>(entity => {
            entity.HasKey(e => e.GroupId);
        });
        modelBuilder.Entity<DWPaymentMethod>(entity => {
            entity.HasKey(e => new { e.MethodId, e.Type });
        });
        modelBuilder.Entity<DWPlayer>(entity => {
            entity.HasKey(e => e.PlayerId);
            entity.Property(e => e.PlayerId).ValueGeneratedNever();
        });
        modelBuilder.Entity<DWProcessor>(entity => {
            entity.HasKey(e => e.ProcessorId);
        });
        modelBuilder.Entity<DWProvider>(entity => {
            entity.HasKey(e => e.ProviderId);
            entity.Property(e => e.ProviderId).ValueGeneratedNever();
        });
        modelBuilder.Entity<DWRealGameEvent>(entity => {
            entity.HasKey(e => e.EventId);
            entity.Property(e => e.EventId).ValueGeneratedNever();
        });
        modelBuilder.Entity<DWStore>(entity => {
            entity.HasKey(e => e.StoreId);
        });
        modelBuilder.Entity<DWStoreTx>(entity => {
            entity.HasKey(e => e.TxId);
            entity.Property(e => e.TxId).ValueGeneratedNever();
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
