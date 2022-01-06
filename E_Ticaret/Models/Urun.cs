namespace E_Ticaret.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Urun")]
    public partial class Urun
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urun()
        {
            Resims = new HashSet<Resim>();
            SatisDetays = new HashSet<SatisDetay>();
            UrunOzelliks = new HashSet<UrunOzellik>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Adi { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }

        [Column(TypeName = "money")]
        public decimal AlisFiyat { get; set; }

        [Column(TypeName = "money")]
        public decimal SatisFiyat { get; set; }

        public DateTime? EklenmeTarihi { get; set; }

        public DateTime? SonKullanmaTarihi { get; set; }

        public int KategoriID { get; set; }

        public int? MarkaID { get; set; }

        public int? TedarikciID { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Marka Marka { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resim> Resims { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SatisDetay> SatisDetays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunOzellik> UrunOzelliks { get; set; }

        public virtual Tedarikci Tedarikci { get; set; }
    }
}
