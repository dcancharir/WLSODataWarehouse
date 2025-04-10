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
        modelBuilder.Entity<DWAssociate>(entity =>
        {
            entity.HasKey(e => e.Ruc).HasName("PK_Associate");

            entity.ToTable("Associate", tb => tb.HasComment("Asociados"));

            entity.Property(e => e.Ruc)
                .IsFixedLength()
                .HasComment("Ruc");

            entity.Property(e => e.Name)
                .HasComment("Nombre del asociado");

            entity.Property(e => e.Status)
                .HasDefaultValue(Convert.ToByte(1))
                .HasComment("Estado del asociado");
        });

        modelBuilder.Entity<DWBrand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK_Brand");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandId)
                .IsFixedLength()
                .HasComment("ID de la marca");

            entity.Property(e => e.Active)
                .HasComment("1 -> Activo | 0 -> Inactivo");

            entity.Property(e => e.Name)
                .HasComment("Nombre Brand");

            entity.Property(e => e.ProviderId)
                .HasComment("ID del provider");
        });

        modelBuilder.Entity<DWCustomer>(entity =>
        {
            entity.ToTable("Customer", tb => tb.HasComment("Informacion de Customer"));

            entity.Property(e => e.Active).HasDefaultValue(Convert.ToSByte(1));
            entity.Property(e => e.AssociateId).IsFixedLength();
            entity.Property(e => e.Email).IsFixedLength();
            entity.Property(e => e.Excluded).HasDefaultValue(Convert.ToSByte(0));
            entity.Property(e => e.Identification).IsFixedLength();
            entity.Property(e => e.Phone).IsFixedLength();
            entity.Property(e => e.StoreId).IsFixedLength();
            entity.Property(e => e.Username).IsFixedLength();
            entity.Property(e => e.Verified).HasDefaultValue(Convert.ToSByte(0));
        });

        modelBuilder.Entity<DWCustomersGroup>(entity =>
        {
            entity.HasKey(e => new { e.PlayerId, e.GroupId }).HasName("PK_CustomersGroup");
            entity.ToTable("CustomersGroup");
        });

        modelBuilder.Entity<DWGame>(entity =>
        {
            entity.HasKey(e => new { e.GameId, e.ProviderId }).HasName("PK_Game");

            entity.ToTable("Game", tb => tb.HasComment("Informacion de los Games"));

            entity.Property(e => e.GameId).IsFixedLength().HasComment("ID del servicio a traves del que se enviaran mensajes");
            entity.Property(e => e.ProviderId).HasComment("ID del provider");
            entity.Property(e => e.Active).HasDefaultValue(Convert.ToSByte(1)).HasComment("1 -> Activo | 0 -> Inactivo");
            entity.Property(e => e.BrandId).IsFixedLength().HasComment("ID de brand");
            entity.Property(e => e.NameBack).IsFixedLength().HasComment("Descripcion del game");
        });

        modelBuilder.Entity<DWGroupsx>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK_Groupsx");

            entity.ToTable("Groupsx");

            entity.Property(e => e.GroupId)
                .ValueGeneratedNever()
                .HasComment("Id del Tag");

            entity.Property(e => e.Status).HasComment("1: Active, 2: Delete");
            entity.Property(e => e.Type).HasComment("1 -> Tag, 2 -> Segment");
        });

        modelBuilder.Entity<DWPaymentMethod>(entity =>
        {
            entity.HasKey(e => new { e.MethodId, e.Type }).HasName("PK_PaymentMethod");

            entity.ToTable("PaymentMethod");

            entity.Property(e => e.MethodId).IsFixedLength().HasComment("ID del metodo del pago. Ej: NIUBIZ_CARDS");
            entity.Property(e => e.Type).HasDefaultValue("PAYIN").IsFixedLength().HasComment("Tipo: PAYIN, PAYOUT");
            entity.Property(e => e.Active).HasDefaultValue(1).HasComment("1 -> True | 0 -> False");
            entity.Property(e => e.Name).HasComment("Nombre del metodo de pago");
        });

        modelBuilder.Entity<DWPlayer>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK_Player");

            entity.ToTable("Player", tb => tb.HasComment("Cuentas de Players de dinero real, estas se generan cuando el cliente realiza un registro"));

            entity.Property(e => e.PlayerId).ValueGeneratedNever().HasComment("ID autoincremental para el player");
            entity.Property(e => e.Coins).HasDefaultValue(Convert.ToUInt64(0)).HasComment("Coins reales");
            entity.Property(e => e.CoinsPayOut).HasDefaultValue(Convert.ToUInt64(0)).HasComment("Balance retirable del usuario");
        });

        modelBuilder.Entity<DWProcessor>(entity =>
        {
            entity.HasKey(e => e.ProcessorId).HasName("PK_Processor");

            entity.ToTable("Processor", tb => tb.HasComment("Tabla de registros del balance los procesadores de pago"));

            entity.Property(e => e.ProcessorId).HasComment("Identificador del procesador. Ej: NIUBIZ, CULQUI, IZIPAY, ...");
            entity.Property(e => e.Name).HasComment("Nombre del proveedor");
            entity.Property(e => e.State).HasDefaultValue(Convert.ToSByte(1)).HasComment("Estado actual del procesador 0 <- DESACTIVADO | 1 <- ACTIVADO");
        });

        modelBuilder.Entity<DWProvider>(entity =>
        {
            entity.HasKey(e => e.ProviderId).HasName("PK_Provider");

            entity.ToTable("Provider", tb => tb.HasComment("Informacion de Proveedores"));

            entity.Property(e => e.ProviderId).ValueGeneratedNever().HasComment("ID autoincremental del provider");
            entity.Property(e => e.Active).HasDefaultValue(Convert.ToSByte(1)).HasComment("1 -> Activo | 0 -> Inactivo");
            entity.Property(e => e.Name).HasComment("El nombre del provider");
        });

        modelBuilder.Entity<DWRealGameEvent>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK_RealGameEvent");

            entity.ToTable("RealGameEvent");

            entity.Property(e => e.EventId).ValueGeneratedNever().HasComment("ID de la transaccion");
            entity.Property(e => e.Amount).HasComment("Monto de la apuesta");
            entity.Property(e => e.AssociateId).IsFixedLength().HasComment("ID del asociado");
            entity.Property(e => e.CoinsType).HasDefaultValue("REAL").IsFixedLength().HasComment("\"REAL\": dinero real | \"BONUS\": bono");
            entity.Property(e => e.GameId).IsFixedLength().HasComment("ID del juego");
            entity.Property(e => e.InsDatetime).HasComment("Fecha Hora registro");
            entity.Property(e => e.InsTimestamp).HasComment("Epoch registro");
            entity.Property(e => e.PlayerId).HasComment("Id del player");
            entity.Property(e => e.ProviderId).HasComment("ID del proveedor");
            entity.Property(e => e.Status).HasDefaultValue(Convert.ToByte(0)).HasComment("Estado del evento: 0 -> Iniciado...");
            entity.Property(e => e.StoreId).IsFixedLength().HasComment("ID del store del player");
            entity.Property(e => e.Type).IsFixedLength().HasComment("DEBIT | CREDIT");
        });

        modelBuilder.Entity<DWStore>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK_Store");

            entity.ToTable("Store", tb => tb.HasComment("Tiendas"));

            entity.Property(e => e.StoreId).IsFixedLength();
            entity.Property(e => e.AssociateId).IsFixedLength();
            entity.Property(e => e.Status).HasDefaultValue(Convert.ToByte(1));
        });

        modelBuilder.Entity<DWStoreTx>(entity =>
        {
            entity.HasKey(e => e.TxId).HasName("PK_StoreTx");

            entity.ToTable("StoreTx");

            entity.Property(e => e.TxId).ValueGeneratedNever().HasComment("ID de la transaccion");
            entity.Property(e => e.Amount).HasComment("monto");
            entity.Property(e => e.AssociateId).IsFixedLength();
            entity.Property(e => e.Coins).HasDefaultValue(Convert.ToInt64(-1)).HasComment("Coins reales después");
            entity.Property(e => e.CoinsBefore).HasDefaultValue(Convert.ToInt64(-1)).HasComment("Coins reales antes");
            entity.Property(e => e.EndDatetime).HasComment("Fecha y hora de finalización");
            entity.Property(e => e.EndTimestamp).HasComment("Epoch final");
            entity.Property(e => e.InsUserId).HasComment("Usuario que hizo la operación");
            entity.Property(e => e.PaymentMethodId).HasDefaultValue("MANUAL").IsFixedLength().HasComment("Método de pago");
            entity.Property(e => e.PlayerId).HasComment("Id del player");
            entity.Property(e => e.PlayerStoreId).IsFixedLength();
            entity.Property(e => e.ProcessorId).IsFixedLength().HasComment("Procesador");
            entity.Property(e => e.Status).HasComment("0: init | 1: success | ...");
            entity.Property(e => e.SubType).HasDefaultValue("NORMAL").IsFixedLength().HasComment("BONO_FINALIZE");
            entity.Property(e => e.Type).IsFixedLength().HasComment("DEBIT | CREDIT | ...");
            entity.Property(e => e.UserStoreId).IsFixedLength().HasComment("Id de la tienda");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    //protected override void OnModelCreating(ModelBuilder modelBuilder) {
    //    modelBuilder.Entity<DWAssociate>(entity => {
    //        entity.HasKey(e => e.Ruc);
    //        entity.Property(e => e.Ruc).ValueGeneratedNever();
    //    });
    //    modelBuilder.Entity<DWBrand>(entity => {
    //        entity.HasKey(e => e.BrandId);
    //        entity.Property(e => e.BrandId).ValueGeneratedNever();
    //    });
    //    modelBuilder.Entity<DWCustomer>(entity => {
    //    });
    //    modelBuilder.Entity<DWCustomersGroup>(entity => {
    //        entity.HasKey(e => new { e.PlayerId, e.GroupId });
    //    });
    //    modelBuilder.Entity<DWGame>(entity => {
    //        entity.HasKey(e => new { e.GameId, e.ProviderId });
    //    });
    //    modelBuilder.Entity<DWGroupsx>(entity => {
    //        entity.HasKey(e => e.GroupId);
    //        entity.Property(e => e.GroupId).ValueGeneratedNever();
    //    });
    //    modelBuilder.Entity<DWPaymentMethod>(entity => {
    //        entity.HasKey(e => new { e.MethodId, e.Type });
    //    });
    //    modelBuilder.Entity<DWPlayer>(entity => {
    //        entity.HasKey(e => e.PlayerId);
    //        entity.Property(e => e.PlayerId).ValueGeneratedNever();
    //    });
    //    modelBuilder.Entity<DWProcessor>(entity => {
    //        entity.HasKey(e => e.ProcessorId);
    //        entity.Property(e => e.ProcessorId).ValueGeneratedNever();
    //    });
    //    modelBuilder.Entity<DWProvider>(entity => {
    //        entity.HasKey(e => e.ProviderId);
    //        entity.Property(e => e.ProviderId).ValueGeneratedNever();
    //    });
    //    modelBuilder.Entity<DWRealGameEvent>(entity => {
    //        entity.HasKey(e => e.EventId);
    //        entity.Property(e => e.EventId).ValueGeneratedNever();
    //    });
    //    modelBuilder.Entity<DWStore>(entity => {
    //        entity.HasKey(e => e.StoreId);
    //        entity.Property(e => e.StoreId).ValueGeneratedNever();
    //    });
    //    modelBuilder.Entity<DWStoreTx>(entity => {
    //        entity.HasKey(e => e.TxId);
    //        entity.Property(e => e.TxId).ValueGeneratedNever();
    //    });
    //    OnModelCreatingPartial(modelBuilder);
    //}
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
