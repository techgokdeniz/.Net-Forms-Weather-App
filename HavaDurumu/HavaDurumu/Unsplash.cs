using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavaDurumu
{
    class Unsplash
    {
        /* Bu Sınıfın Kullanılmasının Asıl Amacı JSON üzerinden okunan verilerin sınıfa transfer edilmesini
      sağlamak amacıyla oluşturulmuştur.

         */

        public class Urls //Urls sınıfı ve propertyleri 
        {
            public string raw { get; set; }
            public string full { get; set; }
            public string regular { get; set; }
            public string small { get; set; }
            public string thumb { get; set; }
        }

        public class Result //Result sınıfı ve propertyleri
        {
            public string id { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public DateTime? promoted_at { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string color { get; set; }
            public string blur_hash { get; set; }
            public string description { get; set; }
            public string alt_description { get; set; }
            public Urls urls { get; set; }
            public List<object> categories { get; set; }
            public int likes { get; set; }
            public bool liked_by_user { get; set; }
            public List<object> current_user_collections { get; set; }
            public object sponsorship { get; set; }

        }

        // Kullanmaız gereken diğer propertylerimiz

        public int total { get; set; } 
        public int total_pages { get; set; }
        public List<Result> results { get; set; }

        /* JSON ÇIKTISINI GÖRMEK İÇİN AŞAĞIDAKİ LİNKİ KULLANABİLİRSİNİZ 
     https://api.unsplash.com/search/photos?query=Bursa&client_id=iSq7jKzat5VoisBHvLlC61ZLNYg-5kdU0WXs17w5JWs

    JSON içerisinde birden fazla property bulunmaktadır ekstra olarak fakat ben sadece işime yarayacakları almak istedim.


     */

    }
}
