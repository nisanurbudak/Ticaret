
using System.ComponentModel.DataAnnotations;
namespace Ticaret.Models
{
    using System;
    using System.Collections.Generic;
	public partial class KartBilgileri
	{
        public int kart_id { get; set; }
		[Display(Name = "Kart Numarasý")]

		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]

		public string kart_numarasi { get; set; }

		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]

		public Nullable<int> Cvv { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Son Kullanma Tarihi")]
		public Nullable<System.DateTime> SonKullanmaTarihi { get; set; }
        public Nullable<int> musteri_id { get; set; }
        public string odeme_tipi { get; set; }
    
        public virtual Musteriler Musteriler { get; set; }
    }
}
