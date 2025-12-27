using System.ComponentModel.DataAnnotations;

namespace Ticaret.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Siparisler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Siparisler()
        {
            this.Siparisdetay = new HashSet<Siparisdetay>();
            this.Odemeler1 = new HashSet<Odemeler>();
        }
    
        public int siparis_id { get; set; }

		public Nullable<int> musteri_id { get; set; }

		public Nullable<int> odeme_id { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Sipariþ Tarihi")]
		public Nullable<System.DateTime> siparis_tarihi { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Fiyat")]
		public Nullable<decimal> fiyat { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Durum")]
		public string durum { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Ad Soyad")]
		public string ad_soyad { get; set; }
        public string adres_tanimi { get; set; }
        public string Adres { get; set; }

		public virtual Musteriler Musteriler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparisdetay> Siparisdetay { get; set; }
        public virtual Odemeler Odemeler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Odemeler> Odemeler1 { get; set; }

		public virtual KartBilgileri KartBilgileri { get; set; }
	}
}
