using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HavaDurumu.Formlar;
using Newtonsoft.Json;

namespace HavaDurumu.Formlar
{
    public partial class Template : Form
    {
        public Template()
        {
            InitializeComponent();
        }


        //Api'lerde çalışabilmek için siteler üzerinden almış olduğum api keyler
        const string appidgunluk = "542ffd081e67f4512b705f89d2a611b2";
        const string unsplashapi = "iSq7jKzat5VoisBHvLlC61ZLNYg-5kdU0WXs17w5JWs";

        //Arayzü üzerinden gelen şehir ismini aldığım prop.
        public static string gelensehir;


        /// <summary>
        ///  Havadurumu değerlerimizin, icon değerimizin çekilebilmesi ve gösterilebilmesi için yazılmış metot
        /// </summary>
        /// <param name="sehir">İşlem yapılacak şehir belirtilmelidir.</param>
        public void GetForcast(string sehir)
        {
            string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&lang=tr&cnt=6&APPID={1}", sehir, appidgunluk);
            //api'nin çalıştığı url ve bizim anasayfadan gönderdiğimiz şehir propertysi bu özelliğie göre şehrin verilerini getirecek


            using (WebClient web = new WebClient()) //çalıştıktan sonra çöp toplayıcının çalışması için using kullandık ve yeni bir webclient nesnesi olutşurduk.
            {
                web.Encoding = Encoding.UTF8; //Webclient ile alacağımız verileri UTF8 formatına almayı sağladık. Türkçe Karakterler için.
                var json = web.DownloadString(url); //gönderdiğim URl üzerindeki yapıyı webclient sayesinde indirdik.
                var Object = JsonConvert.DeserializeObject<Gunluk>(json); /*JsonConvert eklentisini kullanarak aldığımız string yapısını olutşurduğumuz sınıfa göre
                ayrıştırdık Deserialize ettik. */

                Gunluk output = Object; //gunluk sınıfımızdan bir çıkış oluşturduk ve deseialize olmuş objeye eşledik.


                /* Aşağıda form üzerine yerleştirdiğimiz nesnelere veri yerleştirme işlemi yapılmaktadır. 
                    JSON çıksıtı üzerinden sınıfa aktardığımız veirleri ekrana yazdırıyoruz.
                 */ 


                gun.Text = string.Format("{0}", CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)(getDate(output.list[0].dt).DayOfWeek)]);
                /*  Gün değerini milisecond olarak ve günü ingilizce olarak vermektedir bunun için formatlama işlemi yaptık 
                 bu işlemde kullanılan metotları aşağıda anlatacağım CultureInfo sınıfı ile de türkçeleştirme işlemini yaptık */
                havadurumu.Text = string.Format(" Hava Durumu: {0}\u00B0" + "C", (int)output.list[0].temp.day);
                /* Hava durumu değerini ekrana yazdırdık */
                anadurum.Image = setIcon(output.list[0].weather[0].icon);
                /* Iconlarıda json üzerinden almaktayız fakat bunu bir picturebox'a aktarabilmemiz için işlemlerden 
                 geçirmemiz gerekiyor bunun için bir metot yazdım bunuda aşağıda anlatacağım amacı web üzerinden icon çekmek ve yazdırmak diyebiliriz. 
                 */


                /* Diğer ekrana yazdırma işlemleri */ 
                ack.Text = string.Format("{0}", output.list[0].weather[0].description);
                nem.Text = string.Format("Nem Oranı: {0}%", (int)output.list[0].humidity);
                ruzgar.Text = string.Format("Rüzgar Hızı: {0} km/s", (int)output.list[0].speed);

                Gunismi.Text = string.Format("{0}", CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)(getDate(output.list[1].dt).DayOfWeek)]);
                aciklama.Text = string.Format("{0}", output.list[1].weather[0].description);
                durum.Text = string.Format("{0}\u00B0" + "C", (int)output.list[1].temp.day);
                res.Image = setIcon(output.list[1].weather[0].icon);

                Gunismi1.Text = string.Format("{0}", CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)(getDate(output.list[2].dt).DayOfWeek)]);
                aciklama1.Text = string.Format("{0}", output.list[2].weather[0].description);
                durum1.Text = string.Format("{0}\u00B0" + "C", (int)output.list[2].temp.day);
                res1.Image = setIcon(output.list[2].weather[0].icon);

                Gunismi2.Text = string.Format("{0}", CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)(getDate(output.list[3].dt).DayOfWeek)]);
                aciklama2.Text = string.Format("{0}", output.list[3].weather[0].description);
                durum2.Text = string.Format("{0}\u00B0" + "C", (int)output.list[3].temp.day);
                res2.Image = setIcon(output.list[3].weather[0].icon);

                Gunismi3.Text = string.Format("{0}", CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)(getDate(output.list[4].dt).DayOfWeek)]);
                aciklama3.Text = string.Format("{0}", output.list[4].weather[0].description);
                durum3.Text = string.Format("{0}\u00B0" + "C", (int)output.list[4].temp.day);
                res3.Image = setIcon(output.list[4].weather[0].icon);

                Gunismi4.Text = string.Format("{0}", CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)(getDate(output.list[5].dt).DayOfWeek)]);
                aciklama4.Text = string.Format("{0}", output.list[5].weather[0].description);
                durum4.Text = string.Format("{0}\u00B0" + "C", (int)output.list[5].temp.day);
                res4.Image = setIcon(output.list[5].weather[0].icon);

                UnsplashApi(sehir);
            }
        }



        
        /// <summary>
        ///  Gün Değerimizi Bulmaya yarayan metot
        /// </summary>
        /// <param name="millisecound"> Gün değerinin bulunması için millisecond değeri gereklidir. </param>
        DateTime getDate(double millisecound)
        {
            DateTime day = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(millisecound).ToLocalTime();
            return day;

            /* Yenir bir datatime sınıfı oluşturuyoruz ve dateTimeKind ile Evrensel saate çevirme işlemi yapıyoruz 
             ve local saate çeviriyoruz ardından day değişkenimize ekleme işlemi yapıp local olarak gün değişkenimizi
             buluyoruz.
             */ 
        }


        
        /// <summary>
        ///  İcon resminin görüntülenebilmesi
        /// </summary>
        /// <param name="iconID"> Doğru iconların bulunabilmesi için iconid gereklidir </param>
        Image setIcon(string iconID)
        {
            string iconid = iconID.Substring(0, 2) + "n"; //bu satır gelen değerin night kısmını almak için yani 2 çeşit icon bulunuyor onun için güzel olanını aldım.
            string url = string.Format("http://openweathermap.org/img/wn/{0}@2x.png", iconid); // weather icon servisinin url adresi 
            var request = WebRequest.Create(url); //WebRequest sınıfı ile yukarıdaki url adresinden veri isteme işlemi yapıyoruz.
            using (var response = request.GetResponse()) //aldığımız veri kaynağından yanıt döndürüyoruz
            using (var weatherIcon = response.GetResponseStream()) //veriyi okuyoruz
            {
                Image weatherImg = Bitmap.FromStream(weatherIcon); //image türünde değişken oluşturup veri içerisinden aldığımız resmi bitmap olarak değişkene aktarıyoruz
                return weatherImg; //Image türüne sahip değişkenimizi döndürüyoruz.
            }
        }



        /// <summary>
        ///  Arka planların indirilmesi için gerekli olan metot Unsplash üzerinden arka plan verisi çeker
        /// </summary>
        /// <param name="sehir"> İşlem yapılacak şehir belirtilmelidir. </param>
        public void UnsplashApi(string sehir) 
        {
            // baseUrl
            string baseUrl = string.Format("https://api.unsplash.com/search/photos?query={0}&client_id=iSq7jKzat5VoisBHvLlC61ZLNYg-5kdU0WXs17w5JWs", sehir); //resim biliglerini alacağımız json dosyasının formatlıyoruz

            using (WebClient web = new WebClient()) //yeni bir webclient oluşturuyoruz.
            {
                web.Encoding = Encoding.UTF8; //Türkçe karakterler için türümüzü UTF8 yapıyoruz.
                var json = web.DownloadString(baseUrl); //url içerisindeki jsonu string olarak indiriyoruz
                var Object = JsonConvert.DeserializeObject<Unsplash>(json);
                //json convert eklentsindeki deserialize metodunu kullanarak ayarlamış olduğumzu sınıf sayesinde json dosyamızı formatlıyoruz.

                Unsplash output = Object; //bir çıkış değişkeni oluşturuyoruz.

                string url = output.results[0].urls.regular.ToString(); //oluşturudğumuz çıkış değişkeni ve sınıf yardımıyla json dosyasından veri çekiyoruz
                pictureBox1.Image = ArkaPlan(url); //picturebox'umuzun arka plan resmini ArkaPlan metodundan dönen image türündeki veriye eşitliyoruz.
            }

        }



        /// <summary>
        ///  Arkaplan resminin görüntülenebilmesi
        /// </summary>
        /// <param name="urlimg"> Resmin görüntülenebilmesi ve indirilebilmesi için url gereklidir </param>
        public static Image ArkaPlan(string urlimg)
        {
            string url = urlimg; //url isimli değişkene resim dosyamızın adresini aktarıyoruz.
            var request = WebRequest.Create(url); //yeni bir web isteği oluşturuyoruz.
            using (var response = request.GetResponse()) 
            using (var Back = response.GetResponseStream()) //veri okuma işlemi yapıyoruz.
            {
                Image weatherBack = Bitmap.FromStream(Back); //image türünde değişken oluşturup çektiğimiz resmi aktarıyoruz
                return weatherBack; //resmi geri döndürüyoruz.
            }
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //Bu click olayının asıl amacı üzerine tıklandığında programı sonlandırabilmektir.
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            /*Bu click olayının asıl amacı formlar arasında geçişi sağlamaktır 
             
             Geri dönme olayını menü ile yapmadığım için Ram tüketimi artmaktadır. İlerleyen dönemlerde bu sorunu
             çözmek için çalışacağım.
             
             */
            this.Hide(); //üzerinde çalıştığımız formu gizler.
            Form1 anasayfa = new Form1();
            anasayfa.Show(); //anasayfayı yeniden gösterir.
        }
    }
}
