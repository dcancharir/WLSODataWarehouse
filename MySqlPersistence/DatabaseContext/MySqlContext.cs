using Microsoft.EntityFrameworkCore;
using MySqlDomain;
namespace MySqlPersistence.DataBaseContext;
public partial class MySqlContext : DbContext {
    public MySqlContext(DbContextOptions<MySqlContext> options):base(options) { }
    public virtual DbSet<Associate> Associates { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomersGroup> CustomersGroups { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Groupsx> Groupsxes { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Processor> Processors { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<RealGameEvent> RealGameEvents { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoreTx> StoreTxs { get; set; }
    public virtual DbSet<Bonuse> Bonuses { get; set; }
    public virtual DbSet<BonusesStatus> BonusesStatuses { get; set; }
    public virtual DbSet<BonusStatusLog> BonusStatusLog { get;}
    public virtual DbSet<User> Users { get; set; }
    //protected override void OnModelCreating(ModelBuilder modelBuilder) {
    //    modelBuilder
    //        .UseCollation("utf8mb4_general_ci")
    //        .HasCharSet("utf8mb4");

    //    modelBuilder.Entity<Associate>(entity => {
    //        entity.HasKey(e => e.Ruc).HasName("PRIMARY");

    //        entity.ToTable(tb => tb.HasComment("Asociados"));

    //        entity.Property(e => e.Ruc)
    //            .IsFixedLength()
    //            .HasComment("Ruc");
    //        entity.Property(e => e.Name).HasComment("Nombre del asociado");
    //        entity.Property(e => e.Status)
    //            .HasDefaultValueSql("'1'")
    //            .HasComment("Estado del asociado");
    //    });

    //    modelBuilder.Entity<Brand>(entity => {
    //        entity.HasKey(e => e.BrandId).HasName("PRIMARY");

    //        entity.Property(e => e.BrandId)
    //            .IsFixedLength()
    //            .HasComment("ID de la marca");
    //        entity.Property(e => e.Active).HasComment("1 -> Activo | 0 -> Inactivo");
    //        entity.Property(e => e.Name).HasComment("Nombre Brand");
    //        entity.Property(e => e.ProviderId).HasComment("ID del provider");
    //    });

    //    modelBuilder.Entity<Customer>(entity => {
    //        entity.ToTable(tb => tb.HasComment("Informacion de Customer"));

    //        entity.Property(e => e.Active).HasDefaultValueSql("'1'");
    //        entity.Property(e => e.AssociateId).IsFixedLength();
    //        entity.Property(e => e.Email).IsFixedLength();
    //        entity.Property(e => e.Excluded).HasDefaultValueSql("'0'");
    //        entity.Property(e => e.Identification).IsFixedLength();
    //        entity.Property(e => e.Phone).IsFixedLength();
    //        entity.Property(e => e.StoreId).IsFixedLength();
    //        entity.Property(e => e.Username).IsFixedLength();
    //        entity.Property(e => e.Verified).HasDefaultValueSql("'0'");
    //    });

    //    modelBuilder.Entity<CustomersGroup>(entity => {
    //        entity.HasKey(e => new { e.PlayerId, e.GroupId })
    //            .HasName("PRIMARY")
    //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
    //    });

    //    modelBuilder.Entity<Game>(entity => {
    //        entity.HasKey(e => new { e.GameId, e.ProviderId })
    //            .HasName("PRIMARY")
    //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

    //        entity.ToTable(tb => tb.HasComment("Informacion de los Games"));

    //        entity.Property(e => e.GameId)
    //            .IsFixedLength()
    //            .HasComment("ID del servicio a traves del que se enviaran mensajes");
    //        entity.Property(e => e.ProviderId).HasComment("ID del provider");
    //        entity.Property(e => e.Active)
    //            .HasDefaultValueSql("'1'")
    //            .HasComment("1 -> Activo | 0 -> Inactivo");
    //        entity.Property(e => e.BrandId)
    //            .IsFixedLength()
    //            .HasComment("ID de brand");
    //        entity.Property(e => e.NameBack)
    //            .IsFixedLength()
    //            .HasComment("Descripcion del game");
    //    });

    //    modelBuilder.Entity<Groupsx>(entity => {
    //        entity.HasKey(e => e.GroupId).HasName("PRIMARY");

    //        entity.Property(e => e.GroupId)
    //            .ValueGeneratedNever()
    //            .HasComment("Id del Tag");
    //        entity.Property(e => e.Status).HasComment("1: Active, 2: Delete");
    //        entity.Property(e => e.Type).HasComment("1 -> Tag, 2 -> Segment");
    //    });

    //    modelBuilder.Entity<PaymentMethod>(entity => {
    //        entity.HasKey(e => new { e.MethodId, e.Type })
    //            .HasName("PRIMARY")
    //            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

    //        entity.Property(e => e.MethodId)
    //            .IsFixedLength()
    //            .HasComment("ID del metodo del pago. Ej: NIUBIZ_CARDS");
    //        entity.Property(e => e.Type)
    //            .HasDefaultValueSql("'PAYIN'")
    //            .IsFixedLength()
    //            .HasComment("Tipo: PAYIN, PAYOUT");
    //        entity.Property(e => e.Active)
    //            .HasDefaultValueSql("'1'")
    //            .HasComment("1 -> True | 0 -> False");
    //        entity.Property(e => e.Name).HasComment("Nombre del metodo de pago");
    //    });

    //    modelBuilder.Entity<Player>(entity => {
    //        entity.HasKey(e => e.PlayerId).HasName("PRIMARY");

    //        entity.ToTable(tb => tb.HasComment("Cuentas de Players de dinero real, estas se geeran cuando el cliente realiza un registro"));

    //        entity.Property(e => e.PlayerId)
    //            .ValueGeneratedNever()
    //            .HasComment("ID autoincremental para el player");
    //        entity.Property(e => e.Coins)
    //            .HasDefaultValueSql("'0'")
    //            .HasComment("Coins reales");
    //        entity.Property(e => e.CoinsPayOut)
    //            .HasDefaultValueSql("'0'")
    //            .HasComment("Balance retirable del usuario");
    //    });

    //    modelBuilder.Entity<Processor>(entity => {
    //        entity.HasKey(e => e.ProcessorId).HasName("PRIMARY");

    //        entity.ToTable(tb => tb.HasComment("Tabla de registros del balance los procesadores de pago"));

    //        entity.Property(e => e.ProcessorId).HasComment("Identificador del procesador. Ej: NIUBIZ, CULQUI, IZIPAY, ...");
    //        entity.Property(e => e.Name).HasComment("Nombre del provesador");
    //        entity.Property(e => e.State)
    //            .HasDefaultValueSql("'1'")
    //            .HasComment("Estado actual del processador 0 <- DESACTIVADO | 1 <- ACTIVADO");
    //    });

    //    modelBuilder.Entity<Provider>(entity => {
    //        entity.HasKey(e => e.ProviderId).HasName("PRIMARY");

    //        entity.ToTable(tb => tb.HasComment("Informacion de Proveedores"));

    //        entity.Property(e => e.ProviderId)
    //            .ValueGeneratedNever()
    //            .HasComment("ID autoincremental del provider");
    //        entity.Property(e => e.Active)
    //            .HasDefaultValueSql("'1'")
    //            .HasComment("1 -> Activo | 0 -> Inactivo");
    //        entity.Property(e => e.Name).HasComment("El nombre del provider");
    //    });

    //    modelBuilder.Entity<RealGameEvent>(entity => {
    //        entity.HasKey(e => e.EventId).HasName("PRIMARY");

    //        entity.Property(e => e.EventId)
    //            .ValueGeneratedNever()
    //            .HasComment("ID de la transaccion");
    //        entity.Property(e => e.Amount).HasComment("Monto de la apuesta");
    //        entity.Property(e => e.AssociateId)
    //            .IsFixedLength()
    //            .HasComment("ID del asociado");
    //        entity.Property(e => e.CoinsType)
    //            .HasDefaultValueSql("'REAL'")
    //            .IsFixedLength()
    //            .HasComment("\"REAL\": Usado para juegos con su dinero | \"BONUS\": Usado para juegos con bono  ");
    //        entity.Property(e => e.GameId)
    //            .IsFixedLength()
    //            .HasComment("ID del juego");
    //        entity.Property(e => e.InsDatetime).HasComment("Fecha Hora registro");
    //        entity.Property(e => e.InsTimestamp).HasComment("Epoch registro");
    //        entity.Property(e => e.PlayerId).HasComment("Id del player");
    //        entity.Property(e => e.ProviderId).HasComment("ID del proveedor, revisar tabla WLSOProvidersDB.Providers");
    //        entity.Property(e => e.Status)
    //            .HasDefaultValueSql("'0'")
    //            .HasComment("Estado del evento. 0 -> Iniciado, 1 -> Procesado o completado (actualizo el balance del player), 5 -> Denegado (Ej: se solicita un credit cuando la apuesta fue un bet_bonus)");
    //        entity.Property(e => e.StoreId)
    //            .IsFixedLength()
    //            .HasComment("ID del store del player");
    //        entity.Property(e => e.Type)
    //            .IsFixedLength()
    //            .HasComment("DEBIT | CREDIT");
    //    });

    //    modelBuilder.Entity<Store>(entity => {
    //        entity.HasKey(e => e.StoreId).HasName("PRIMARY");

    //        entity.ToTable(tb => tb.HasComment("Tiendas"));

    //        entity.Property(e => e.StoreId).IsFixedLength();
    //        entity.Property(e => e.AssociateId).IsFixedLength();
    //        entity.Property(e => e.Status).HasDefaultValueSql("'1'");
    //    });

    //    modelBuilder.Entity<StoreTx>(entity => {
    //        entity.HasKey(e => e.TxId).HasName("PRIMARY");

    //        entity.Property(e => e.TxId)
    //            .ValueGeneratedNever()
    //            .HasComment("ID de la transaccion");
    //        entity.Property(e => e.Amount).HasComment("monto");
    //        entity.Property(e => e.AssociateId).IsFixedLength();
    //        entity.Property(e => e.Coins)
    //            .HasDefaultValueSql("'-1'")
    //            .HasComment("Coins reales despues de la transacction");
    //        entity.Property(e => e.CoinsBefore)
    //            .HasDefaultValueSql("'-1'")
    //            .HasComment("Coins reales antes de la transacction");
    //        entity.Property(e => e.EndDatetime).HasComment("Fecha y hora de finalizacion de compra");
    //        entity.Property(e => e.EndTimestamp).HasComment("Epoch al finalizar la transaccion");
    //        entity.Property(e => e.InsUserId).HasComment("Id del usuario que realizo la operacion");
    //        entity.Property(e => e.PaymentMethodId)
    //            .HasDefaultValueSql("'MANUAL'")
    //            .IsFixedLength()
    //            .HasComment("Metodo de pago");
    //        entity.Property(e => e.PlayerId).HasComment("Id del player");
    //        entity.Property(e => e.PlayerStoreId).IsFixedLength();
    //        entity.Property(e => e.ProcessorId)
    //            .IsFixedLength()
    //            .HasComment("Identificador del procesador. Ej: NIUBIZ, CULQUI, IZIPAY, ...");
    //        entity.Property(e => e.Status).HasComment("0: init | 1: success | 2: error | 3: rejected | 4: pending | 5: expired");
    //        entity.Property(e => e.SubType)
    //            .HasDefaultValueSql("'NORMAL'")
    //            .IsFixedLength()
    //            .HasComment("BONO_FINALIZE");
    //        entity.Property(e => e.Type)
    //            .IsFixedLength()
    //            .HasComment("DEBIT | CREDIT | DEBIT_RESELLER | CREDIT_RESELLER");
    //        entity.Property(e => e.UserStoreId)
    //            .IsFixedLength()
    //            .HasComment("Id de la tienda");
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
