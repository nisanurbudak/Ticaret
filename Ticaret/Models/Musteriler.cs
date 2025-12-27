
using System.ComponentModel.DataAnnotations;

namespace Ticaret.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Musteriler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Musteriler()
        {
            this.Saticilar = new HashSet<Saticilar>();
            this.Siparisler = new HashSet<Siparisler>();
            this.Sepet = new HashSet<Sepet>();
            this.Favoriler = new HashSet<Favoriler>();
            this.KartBilgileri = new HashSet<KartBilgileri>();
        }
    
        public int musteri_id { get; set; }
[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
[Display(Name = "Ad Soyad")]
public string ad_soyad { get; set; }

[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
[Display(Name = "Kullanýcý Adý")]
[StringLength(100, ErrorMessage = "Kullanýcý adý en az 8 karakter olmalý.", MinimumLength = 8)]
[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Kullanýcý adý sadece harf ve rakamlardan oluþmalýdýr.")]
public string kullanici_adi { get; set; }
[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
[Display(Name = "Þifre")]

[StringLength(100, ErrorMessage = "Þifre en az 8 karakter olmalý.", MinimumLength = 8)]
[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Þifre sadece harf ve rakamlardan oluþmalýdýr.")]
public string sifre { get; set; }
[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
[Display(Name = "E-Mail")]
public string email { get; set; }
[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
[Display(Name = "Telefon")]

[StringLength(11, ErrorMessage = "Telefon numarasý 11 karakter olmalýdýr.", MinimumLength = 11)]
[RegularExpression("^[0-9]*$", ErrorMessage = "Telefon numarasý sadece rakamlardan oluþmalýdýr.")]
public string telefon { get; set; }
[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
[Display(Name = "Adres")]
public string adres { get; set; }
[Display(Name = "Ülke")]
[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
public string ulke { get; set; }
[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
[Display(Name = "Þehir")]
public string sehir { get; set; }
[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
[Display(Name = "Ýlçe")]
public string ilce { get; set; }
public string LoginErrorMessage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Saticilar> Saticilar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparisler> Siparisler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sepet> Sepet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favoriler> Favoriler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KartBilgileri> KartBilgileri { get; set; }
    }
}
