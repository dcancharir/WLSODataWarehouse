using Domain.DW;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DataBaseContext;
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
        //modelBuilder.Entity<DWAssociate>(entity => {
        //    entity.HasKey(e => e.Ruc);
        //    entity.ToTable(tb => tb.HasComment("Asociados"));
        //    entity.Property(e => e.Ruc)
        //        .IsFixedLength()
        //        .HasComment("Ruc");
        //    entity.Property(e => e.Name).HasComment("Nombre del asociado");
        //    entity.Property(e => e.Status)
        //        .HasDefaultValueSql("'1'")
        //        .HasComment("Estado del asociado");
        //});
        //modelBuilder.Entity<DWBrand>(entity => {
        //    entity.HasKey(e => e.BrandId);

        //    entity.Property(e => e.BrandId)
        //        .IsFixedLength()
        //        .HasComment("ID de la marca");
        //    entity.Property(e => e.Active).HasComment("1 -> Activo | 0 -> Inactivo");
        //    entity.Property(e => e.Name).HasComment("Nombre Brand");
        //    entity.Property(e => e.ProviderId).HasComment("ID del provider");
        //});
        //modelBuilder.Entity<DWCustomer>(entity => {
        //    entity.ToTable(tb => tb.HasComment("Informacion de Customer"));

        //    entity.Property(e => e.Active).HasDefaultValueSql("'1'");
        //    entity.Property(e => e.AssociateId).IsFixedLength();
        //    entity.Property(e => e.Email).IsFixedLength();
        //    entity.Property(e => e.Excluded).HasDefaultValueSql("'0'");
        //    entity.Property(e => e.Identification).IsFixedLength();
        //    entity.Property(e => e.Phone).IsFixedLength();
        //    entity.Property(e => e.StoreId).IsFixedLength();
        //    entity.Property(e => e.Username).IsFixedLength();
        //    entity.Property(e => e.Verified).HasDefaultValueSql("'0'");
        //});
        //modelBuilder.Entity<DWCustomersGroup>(entity => {
        //    entity.HasKey(e => new { e.PlayerId, e.GroupId });
        //});
        //modelBuilder.Entity<DWGame>(entity => {
        //    entity.HasKey(e => new { e.GameId, e.ProviderId });

        //    entity.ToTable(tb => tb.HasComment("Informacion de los Games"));

        //    entity.Property(e => e.GameId)
        //        .IsFixedLength()
        //        .HasComment("ID del servicio a traves del que se enviaran mensajes");
        //    entity.Property(e => e.ProviderId).HasComment("ID del provider");
        //    entity.Property(e => e.Active)
        //        .HasDefaultValueSql("'1'")
        //        .HasComment("1 -> Activo | 0 -> Inactivo");
        //    entity.Property(e => e.BrandId)
        //        .IsFixedLength()
        //        .HasComment("ID de brand");
        //    entity.Property(e => e.NameBack)
        //        .IsFixedLength()
        //        .HasComment("Descripcion del game");
        //});
        //modelBuilder.Entity<DWGroupsx>(entity => {
        //    entity.HasKey(e => e.GroupId);

        //    entity.Property(e => e.GroupId)
        //        .ValueGeneratedNever()
        //        .HasComment("Id del Tag");
        //    entity.Property(e => e.Status).HasComment("1: Active, 2: Delete");
        //    entity.Property(e => e.Type).HasComment("1 -> Tag, 2 -> Segment");
        //});

        //modelBuilder.Entity<DWPaymentMethod>(entity => {
        //    entity.HasKey(e => new { e.MethodId, e.Type });

        //    entity.Property(e => e.MethodId)
        //        .IsFixedLength()
        //        .HasComment("ID del metodo del pago. Ej: NIUBIZ_CARDS");
        //    entity.Property(e => e.Type)
        //        .HasDefaultValueSql("'PAYIN'")
        //        .IsFixedLength()
        //        .HasComment("Tipo: PAYIN, PAYOUT");
        //    entity.Property(e => e.Active)
        //        .HasDefaultValueSql("'1'")
        //        .HasComment("1 -> True | 0 -> False");
        //    entity.Property(e => e.Name).HasComment("Nombre del metodo de pago");
        //});
        //modelBuilder.Entity<DWPlayer>(entity => {
        //    entity.HasKey(e => e.PlayerId);

        //    entity.ToTable(tb => tb.HasComment("Cuentas de Players de dinero real, estas se geeran cuando el cliente realiza un registro"));

        //    entity.Property(e => e.PlayerId)
        //        .ValueGeneratedNever()
        //        .HasComment("ID autoincremental para el player");
        //    entity.Property(e => e.Coins)
        //        .HasDefaultValueSql("'0'")
        //        .HasComment("Coins reales");
        //    entity.Property(e => e.CoinsPayOut)
        //        .HasDefaultValueSql("'0'")
        //        .HasComment("Balance retirable del usuario");
        //});

        //modelBuilder.Entity<DWProcessor>(entity => {
        //    entity.HasKey(e => e.ProcessorId);

        //    entity.ToTable(tb => tb.HasComment("Tabla de registros del balance los procesadores de pago"));

        //    entity.Property(e => e.ProcessorId).HasComment("Identificador del procesador. Ej: NIUBIZ, CULQUI, IZIPAY, ...");
        //    entity.Property(e => e.Name).HasComment("Nombre del provesador");
        //    entity.Property(e => e.State)
        //        .HasDefaultValueSql("'1'")
        //        .HasComment("Estado actual del processador 0 <- DESACTIVADO | 1 <- ACTIVADO");
        //});
        //modelBuilder.Entity<DWProvider>(entity => {
        //    entity.HasKey(e => e.ProviderId);

        //    entity.ToTable(tb => tb.HasComment("Informacion de Proveedores"));

        //    entity.Property(e => e.ProviderId)
        //        .ValueGeneratedNever()
        //        .HasComment("ID autoincremental del provider");
        //    entity.Property(e => e.Active)
        //        .HasDefaultValueSql("'1'")
        //        .HasComment("1 -> Activo | 0 -> Inactivo");
        //    entity.Property(e => e.Name).HasComment("El nombre del provider");
        //});
        //modelBuilder.Entity<DWRealGameEvent>(entity => {
        //    entity.HasKey(e => e.EventId);

        //    entity.Property(e => e.EventId)
        //        .ValueGeneratedNever()
        //        .HasComment("ID de la transaccion");
        //    entity.Property(e => e.Amount).HasComment("Monto de la apuesta");
        //    entity.Property(e => e.AssociateId)
        //        .IsFixedLength()
        //        .HasComment("ID del asociado");
        //    entity.Property(e => e.CoinsType)
        //        .HasDefaultValueSql("'REAL'")
        //        .IsFixedLength()
        //        .HasComment("\"REAL\": Usado para juegos con su dinero | \"BONUS\": Usado para juegos con bono  ");
        //    entity.Property(e => e.GameId)
        //        .IsFixedLength()
        //        .HasComment("ID del juego");
        //    entity.Property(e => e.InsDatetime).HasComment("Fecha Hora registro");
        //    entity.Property(e => e.InsTimestamp).HasComment("Epoch registro");
        //    entity.Property(e => e.PlayerId).HasComment("Id del player");
        //    entity.Property(e => e.ProviderId).HasComment("ID del proveedor, revisar tabla WLSOProvidersDB.Providers");
        //    entity.Property(e => e.Status)
        //        .HasDefaultValueSql("'0'")
        //        .HasComment("Estado del evento. 0 -> Iniciado, 1 -> Procesado o completado (actualizo el balance del player), 5 -> Denegado (Ej: se solicita un credit cuando la apuesta fue un bet_bonus)");
        //    entity.Property(e => e.StoreId)
        //        .IsFixedLength()
        //        .HasComment("ID del store del player");
        //    entity.Property(e => e.Type)
        //        .IsFixedLength()
        //        .HasComment("DEBIT | CREDIT");
        //});
        //modelBuilder.Entity<DWStore>(entity => {
        //    entity.HasKey(e => e.StoreId);

        //    entity.ToTable(tb => tb.HasComment("Tiendas"));

        //    entity.Property(e => e.StoreId).IsFixedLength();
        //    entity.Property(e => e.AssociateId).IsFixedLength();
        //    entity.Property(e => e.Status).HasDefaultValueSql("'1'");
        //});
        //modelBuilder.Entity<DWStoreTx>(entity => {
        //    entity.HasKey(e => e.TxId);

        //    entity.Property(e => e.TxId)
        //        .ValueGeneratedNever()
        //        .HasComment("ID de la transaccion");
        //    entity.Property(e => e.Amount).HasComment("monto");
        //    entity.Property(e => e.AssociateId).IsFixedLength();
        //    entity.Property(e => e.Coins)
        //        .HasDefaultValueSql("'-1'")
        //        .HasComment("Coins reales despues de la transacction");
        //    entity.Property(e => e.CoinsBefore)
        //        .HasDefaultValueSql("'-1'")
        //        .HasComment("Coins reales antes de la transacction");
        //    entity.Property(e => e.EndDatetime).HasComment("Fecha y hora de finalizacion de compra");
        //    entity.Property(e => e.EndTimestamp).HasComment("Epoch al finalizar la transaccion");
        //    entity.Property(e => e.InsUserId).HasComment("Id del usuario que realizo la operacion");
        //    entity.Property(e => e.PaymentMethodId)
        //        .HasDefaultValueSql("'MANUAL'")
        //        .IsFixedLength()
        //        .HasComment("Metodo de pago");
        //    entity.Property(e => e.PlayerId).HasComment("Id del player");
        //    entity.Property(e => e.PlayerStoreId).IsFixedLength();
        //    entity.Property(e => e.ProcessorId)
        //        .IsFixedLength()
        //        .HasComment("Identificador del procesador. Ej: NIUBIZ, CULQUI, IZIPAY, ...");
        //    entity.Property(e => e.Status).HasComment("0: init | 1: success | 2: error | 3: rejected | 4: pending | 5: expired");
        //    entity.Property(e => e.SubType)
        //        .HasDefaultValueSql("'NORMAL'")
        //        .IsFixedLength()
        //        .HasComment("BONO_FINALIZE");
        //    entity.Property(e => e.Type)
        //        .IsFixedLength()
        //        .HasComment("DEBIT | CREDIT | DEBIT_RESELLER | CREDIT_RESELLER");
        //    entity.Property(e => e.UserStoreId)
        //        .IsFixedLength()
        //        .HasComment("Id de la tienda");
        //});
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
