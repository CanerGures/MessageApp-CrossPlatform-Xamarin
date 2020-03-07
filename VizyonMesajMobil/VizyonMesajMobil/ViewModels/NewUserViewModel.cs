using System;
using System.Collections.Generic;
using System.Text;
using VizyonMesajMobil.Models;

namespace VizyonMesajMobil.ViewModels
{
    public class NewUserViewModel
    {
        public List<Grup> GroupList { get; set; }
        public List<Grup> SelectedGroup { get; set; }
    }
}
