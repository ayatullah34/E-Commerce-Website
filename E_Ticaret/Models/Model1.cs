using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace E_Ticaret.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

       
        public virtual DbSet<Kargo> Kargoes { get; set; }
        public virtual DbSet<Kategori> Kategoris { get; set; }
        public virtual DbSet<Marka> Markas { get; set; }
        public virtual DbSet<Musteri> Musteris { get; set; }
        public virtual DbSet<MusteriAdres> MusteriAdres { get; set; }
        public virtual DbSet<OzellikDeger> OzellikDegers { get; set; }
        public virtual DbSet<OzellikTip> OzellikTips { get; set; }
        public virtual DbSet<Resim> Resims { get; set; }
        public virtual DbSet<Satis> Satis { get; set; }
        public virtual DbSet<SatisDetay> SatisDetays { get; set; }
        public virtual DbSet<SiparisDurum> SiparisDurums { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Urun> Uruns { get; set; }
        public virtual DbSet<UrunOzellik> UrunOzelliks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Tedarikci> Tedarikcis { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
              .Property(e => e.KullaniciAdi)
              .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Parola)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Rol)
                .IsUnicode(false);

            modelBuilder.Entity<Kargo>()
                .Property(e => e.Telefon)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Kategori>()
                .HasMany(e => e.OzellikTips)
                .WithRequired(e => e.Kategori)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Musteri>()
                .Property(e => e.Telefon)
                .IsFixedLength()
                .IsUnicode(false);

           

            modelBuilder.Entity<OzellikDeger>()
                .HasMany(e => e.UrunOzelliks)
                .WithRequired(e => e.OzellikDeger)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OzellikTip>()
                .HasMany(e => e.OzellikDegers)
                .WithRequired(e => e.OzellikTip)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OzellikTip>()
                .HasMany(e => e.UrunOzelliks)
                .WithRequired(e => e.OzellikTip)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Satis>()
                .Property(e => e.ToplamTutar)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Satis>()
                .HasMany(e => e.SatisDetays)
                .WithRequired(e => e.Satis)
                .HasForeignKey(e => e.SatisID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SatisDetay>()
                .Property(e => e.Fiyat)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Urun>()
                .Property(e => e.AlisFiyat)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Urun>()
                .Property(e => e.SatisFiyat)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.SatisDetays)
                .WithRequired(e => e.Urun)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.UrunOzelliks)
                .WithRequired(e => e.Urun)
                .WillCascadeOnDelete(false);
        }
    }
}
