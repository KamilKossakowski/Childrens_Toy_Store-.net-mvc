using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Childrens_Toy_Store.Models
{
    public class Uprawnieniawyswietl
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string Id_uzytkownik { get; set; }
        public string imie { get; set; }
        public string email { get; set; }
        public string nazwisko { get; set; }
        public string ulica { get; set; }
        public string numer_domu { get; set; }
        public string kod_pocztowy { get; set; }
        public string poczta { get; set; }
    }
}