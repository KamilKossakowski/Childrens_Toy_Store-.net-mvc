using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Childrens_Toy_Store.Models
{
   
        public partial class Produktycreateedit
        {
            public int Id_produktu { get; set; }
            public string nazwa { get; set; }
            public string opis { get; set; }
            public double cena { get; set; }
            [DisplayName("Upload file")]
            public string obrazek { get; set; }
            public Nullable<int> dozw_wiek { get; set; }
            public Nullable<int> rok_produkcji { get; set; }
            public HttpPostedFileBase obrazeknazwa { get; set; }
        }
    


}