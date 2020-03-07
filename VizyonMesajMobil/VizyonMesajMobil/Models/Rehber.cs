using System;
using System.Collections.Generic;
using System.Text;

namespace VizyonMesajMobil.Models
{
    public class Rehber
    {
        public int ID { get; set; }
        public string GsmNo1 { get; set; }
        public string GsmNo2 { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        
        public Nullable<int>AboneID { get; set; }
    }
}
