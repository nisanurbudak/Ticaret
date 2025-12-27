using System.ComponentModel.DataAnnotations;
namespace Ticaret.Models
{
    using System;
    using System.Collections.Generic;
	public partial class Odemeler
	{
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Odemeler()
        {
            this.Siparisler = new HashSet<Siparisler>();
        }
    
        public int odeme_id { get; set; }
		[Required(ErrorMessage = "Bu kýsým boþ býrakýlamaz")]
		[Display(Name = "Ödeme Tipi")]
		public string odeme_tipi { get; set; }
        public Nullable<int> siparis_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparisler> Siparisler { get; set; }
        public virtual Siparisler Siparisler1 { get; set; }
    }
}
