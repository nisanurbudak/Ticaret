
using System.ComponentModel.DataAnnotations;
namespace Ticaret.Models
{
    using System;
    using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public partial class kategoriler
	{
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public kategoriler()
        {
            this.Urunler = new HashSet<Urunler>();
        }
    
        public int kategori_id { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Kategori Adý")]
		public string kategori_ad { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Resim")]
		public string kategori_resim { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urunler> Urunler { get; set; }
    }
}
