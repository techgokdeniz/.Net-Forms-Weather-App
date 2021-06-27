using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavaDurumu
{
    /* Bu Sınıfın Kullanılmasının Asıl Amacı JSON üzerinden okunan verilerin sınıfa transfer edilmesini
      sağlamak amacıyla oluşturulmuştur.
         */


    class Gunluk //Kurucu Metot ve Kurucu Metot içerisinde bulunan City ve List türünde 2 adet property
    {
        public List<list> list { get; set; }
        public city city { get; set; }
    }

    public class city //City Sınıfı
    {
        public string name { get; set; }
    }

    public class list //List Sınıfı ve propertyleri
    {
        public double dt { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double speed { get; set; }
        public temp temp { get; set; }
        public List<weather> weather { get; set; }

    }

    public class weather //Weather Sınıfı ve propertyleri
    {
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class temp //Temp sınıfı ve propertyleri
    {
        public double day { get; set; }
    }

    /* JSON ÇIKTISINI GÖRMEK İÇİN AŞAĞIDAKİ LİNKİ KULLANABİLİRSİNİZ 
     http://api.openweathermap.org/data/2.5/forecast/daily?q=Bursa&units=metric&lang=tr&cnt=6&APPID=542ffd081e67f4512b705f89d2a611b2

    JSON içerisinde birden fazla property bulunmaktadır ekstra olarak fakat ben sadece işime yarayacakları almak istedim.


     */
}
