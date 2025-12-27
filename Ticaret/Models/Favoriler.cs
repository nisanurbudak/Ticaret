

namespace Ticaret.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Favoriler
    {
        public int Id { get; set; }
        public int musteri_id { get; set; }
        public int urun_id { get; set; }
    
        public virtual Musteriler Musteriler { get; set; }
        public virtual Urunler Urunler { get; set; }
    }
}
