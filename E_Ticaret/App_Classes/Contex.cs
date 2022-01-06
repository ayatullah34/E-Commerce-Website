using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Ticaret.Models;

namespace E_Ticaret.App_Classes
{
    public class Contex
    {
        private static Model1 baglanti;

        public static Model1 Baglanti
        {
            get {
                if (baglanti == null)
                    baglanti = new Model1();
                return baglanti; 
            }
            set { baglanti = value; }
        }

    }
}