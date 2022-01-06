using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    [Table("Tedarikci")]
    public partial class Tedarikci
    {
        public int TedarikciID { get; set; }

        [Required]
        [StringLength(40)]
        public string SirketAdi { get; set; }

        [StringLength(30)]
        public string MusteriAdi { get; set; }

        [StringLength(30)]
        public string MusteriUnvani { get; set; }

        [StringLength(60)]
        public string Adres { get; set; }

        [StringLength(15)]
        public string Sehir { get; set; }

        [StringLength(15)]
        public string Bolge { get; set; }

        [StringLength(10)]
        public string PostaKodu { get; set; }

        [StringLength(15)]
        public string Ulke { get; set; }

        [StringLength(24)]
        public string Telefon { get; set; }

        [StringLength(24)]
        public string Faks { get; set; }

        [Column(TypeName = "ntext")]
        public string WebSayfasi { get; set; }

        public virtual ICollection<Urun> Urunler { get; set; }
    }
}