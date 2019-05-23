using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ciceksepeti.Ayarlar
{
    public class Ayarlar
    {
        public static Size UrunBoyut
        {
            get
            {
                Size boyut = new Size();
                boyut.Width = Convert.ToInt32(ConfigurationManager.AppSettings["UrunWidth"]);
                boyut.Height = Convert.ToInt32(ConfigurationManager.AppSettings["UrunHeight"]);
                return boyut;
            }
        }
    }
}