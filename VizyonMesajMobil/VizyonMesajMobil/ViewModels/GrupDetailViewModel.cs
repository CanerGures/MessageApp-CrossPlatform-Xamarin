using System;
using System.Collections.Generic;
using System.Text;
using VizyonMesajMobil.Models;

namespace VizyonMesajMobil.ViewModels
{
    public class GrupDetailViewModel : BaseViewModel
    {
        public Grup Grup { get; set; }
        public GrupDetailViewModel(Grup item = null)
        {
            Title = item?.GrupAdi;
            Grup = item;
        }
    }
}
