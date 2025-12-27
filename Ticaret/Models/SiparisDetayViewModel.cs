using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ticaret.Models
{
	public class SiparisDetayViewModel
	{
		public int SiparisId { get; set; }
		public List<Siparisdetay> SiparisDetaylari { get; set; }
		public KartBilgileri KartBilgileri { get; set; }
	}

}