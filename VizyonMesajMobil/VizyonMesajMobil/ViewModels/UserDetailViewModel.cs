using System;
using System.Collections.Generic;
using VizyonMesajMobil.Models;

namespace VizyonMesajMobil.ViewModels
{
    public class UserDetailViewModel : BaseViewModel
    {
        public Rehber Item { get; set; }
        public List<Grup> RehberGrupList{ get; set; }

        public List<Grup> GroupList { get; set; }
        public List<Grup> SelectedGroup { get; set; }
        public UserDetailViewModel(Rehber item = null)
        {
            Title = item?.Adi;
            Item = item;
        }
    }
}
