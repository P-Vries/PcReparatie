namespace PcReparatie_MK_2.Models
{
    using System.Data.Entity;

    public partial class DataBase : DbContext
    {
        public DataBase()
            : base("name=DataBase")
        {
        }


        public virtual DbSet<Gebruikt> Gebruikts { get; set; }
        public virtual DbSet<Klanten> Klantens { get; set; }
        public virtual DbSet<Medewerker> Medewerkers { get; set; }
        public virtual DbSet<Reparaty> Reparaties { get; set; }
        public virtual DbSet<Rollen> Rollens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gebruikt>()
                .Property(e => e.PrijsOnderdeel)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Klanten>()
                .HasMany(e => e.Reparaties)
                .WithRequired(e => e.Klanten)
                .HasForeignKey(e => e.KlantID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medewerker>()
                .HasMany(e => e.Reparaties)
                .WithRequired(e => e.Medewerker)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reparaty>()
                .Property(e => e.Arbeidsloon)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Reparaty>()
                .HasMany(e => e.Gebruikts)
                .WithRequired(e => e.Reparaty)
                .HasForeignKey(e => e.ReparatieId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rollen>()
                .HasMany(e => e.Klantens)
                .WithRequired(e => e.Rollen)
                .HasForeignKey(e => e.RolId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rollen>()
                .HasMany(e => e.Medewerkers)
                .WithRequired(e => e.Rollen)
                .HasForeignKey(e => e.RolId)
                .WillCascadeOnDelete(false);
        }
    }
}
