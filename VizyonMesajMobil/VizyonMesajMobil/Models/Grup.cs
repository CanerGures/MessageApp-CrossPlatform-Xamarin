using System;
using System.Collections.Generic;
using System.Text;

namespace VizyonMesajMobil.Models
{
    public class Grup
    {
        public int ID { get; set; }
        public string GrupAdi { get; set; }
        public Nullable<int> AboneID { get; set; }
    }
}
