using System.ComponentModel.DataAnnotations;
namespace Ticaret.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Urunler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urunler()
        {
            this.Siparisdetay = new HashSet<Siparisdetay>();
            this.Sepet = new HashSet<Sepet>();
            this.Favoriler = new HashSet<Favoriler>();
        }
    
        public int urun_id { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Ürün Adý")]
		public string urun_adi { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Açýklamasý")]
		public string aciklamasi { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Fiyat")]
		public Nullable<decimal> fiyat { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Stok Miktarý")]
		public Nullable<int> stok { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Satici ID")]
		public Nullable<int> satici_id { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Kategori ID")]
		public Nullable<int> kategori_id { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Resim")]
		public string picture { get; set; }
    
        public virtual kategoriler kategoriler { get; set; }
        public virtual Saticilar Saticilar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparisdetay> Siparisdetay { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sepet> Sepet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favoriler> Favoriler { get; set; }
    }
}
