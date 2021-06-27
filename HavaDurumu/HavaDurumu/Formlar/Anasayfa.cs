using HavaDurumu.Formlar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using static Bunifu.Dataviz.WinForms.BunifuDatavizAdvanced;
using System.Net;
using System.Globalization;
using System.Diagnostics;

namespace HavaDurumu
{
    public partial class Form1 : Form
    {
        /* KENDİ YAZDIĞIM METOTLAR İÇİN SUMMARY PARAM AÇIKLAMALARI YAPILMIŞTIR DİĞERLERİ İÇİN METOT İÇİNDE
         AÇIKLAMA YAPILMIŞTIR. */ 

        
        public static string sehirismi; //tüm sınıf içersinden çağrılabilecek bir string değişken tanımlıyoruz.
        Template cs = new Template(); //template sınıfından veri akışını sağlayabilmek için bir nesne oluşturuyoruz.

        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        ///  Almış olduğu şehir ismine göre Grafik tablosunda grafik oluşturmaya yarayan metot
        /// </summary>
        /// <param name="sehir"> Grafiğin işlem yapacağı şehir ismi zorunludur. </param>
        void RenderColumn(string sehir)
        {
            baslik.Text = sehir.ToString() + " Şehri için Grafiksel Gösterim"; //Başlık değerini değiştiriyoruz
            string[] text = new string[5]; //gün isimlerini tutmak için bir array
            string[] hava = new string[5]; //hava durumu değerlerini tutmak için bir array

            const string appidgunluk = "542ffd081e67f4512b705f89d2a611b2"; //kullandığımız api'nin key bilgsi
            string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&lang=tr&cnt=6&APPID={1}", sehir, appidgunluk);
            //Json URL yapımızı formatlıyoruz.

            using (WebClient web = new WebClient()) //yeni bir webclient başlatıyoruz.
            {
                web.Encoding = Encoding.UTF8; //Türkçe karakter desteği sağlıyoruz 
                var json = web.DownloadString(url); //json dosyamızı string olarak alıyoruz.
                var Object = JsonConvert.DeserializeObject<Gunluk>(json); //Json dosyamızı sınıf yapımıza göre deserialize ediyoruz.

                Gunluk output = Object; //çıkış değişkenimiz

                for (int i = 0; i < 5; i++)
                {
                    string a = string.Format("{0}", CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)(getDate(output.list[i].dt).DayOfWeek)]);
                    string b = string.Format("{0}", (int)output.list[i].temp.day);
                    hava[i] = b;
                    text[i] = a;
                }

                //for döngüsü yardımıyla 5 güne ait gün bilgileri ve hava durumu değerlerini array yapımıza aktarıyoruz.
            }
            

            var canvas = new Canvas(); //grafik çizebilmek için bir canvas oluşturuyoruz. 
            var datapoint1 = new DataPoint(_type.Bunifu_stepArea); //yeni bir data objesi oluşturuyourz ve grafik türümüzü belirliyoruz.

            datapoint1.addLabely(text[0], hava[0]); //array yapılarımız sayesinde x ve y kordinatında değerlerimizi ekliyoruz.
            datapoint1.addLabely(text[1], hava[1]);
            datapoint1.addLabely(text[2], hava[2]);
            datapoint1.addLabely(text[3], hava[3]);
            datapoint1.addLabely(text[4], hava[4]);

            canvas.addData(datapoint1); //oluşturduğumuz canvas yapısında datapointimizi yerleştiriyoruz.
            BunifuData.Title = sehir; //başlığımızı değiştiriyoruz.
            BunifuData.Render(canvas); //oluşturduğumuz datapoint ve canvas yapımızı render yapıyoruz ve gösnterimini sağlıyoruz.
        }



        // Bu Kısım Sayfa İşlemleri İçin butona tıklandığında sayfaların değişmesini sağlıyor.

        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("Anasayfa");
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("Grafik");
            RenderColumn("Bursa");
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("Ayarlar");
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage("Tarımsal");
        }


        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        // Bu Kısım Şehir İşlemleri İçin

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide(); //üzerinde olduğumuz formu kapatıyor.
            cs.Show(); //en yukarıda oluşturduğumuz nesne yardımıyla o formu gösteriyor.
            sehirismi = "Bursa"; //değişkenimizin değernii değiştiriyor
            cs.GetForcast(sehirismi); //o form sınıfı içerisinde bulunan metota sehrimizi yolluyor
            cs.il.Text = sehirismi; 
            cs.UnsplashApi("Bursa"); //arka plan metoduna sehrimizi yolluyor
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide(); //üzerinde olduğumuz formu kapatıyor.
            cs.Show(); //en yukarıda oluşturduğumuz nesne yardımıyla o formu gösteriyor.
            sehirismi = "Istanbul"; //değişkenimizin değernii değiştiriyor
            cs.GetForcast(sehirismi); //o form sınıfı içerisinde bulunan metota sehrimizi yolluyor
            cs.il.Text = sehirismi;
            cs.UnsplashApi("Istanbul"); //arka plan metoduna sehrimizi yolluyor
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Hide(); //üzerinde olduğumuz formu kapatıyor.
            cs.Show(); //en yukarıda oluşturduğumuz nesne yardımıyla o formu gösteriyor.
            sehirismi = "Izmir"; //değişkenimizin değernii değiştiriyor
            cs.GetForcast(sehirismi); //o form sınıfı içerisinde bulunan metota sehrimizi yolluyor
            cs.il.Text = sehirismi;
            cs.UnsplashApi("Izmir"); //arka plan metoduna sehrimizi yolluyor
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            this.Hide(); //üzerinde olduğumuz formu kapatıyor.
            cs.Show(); //en yukarıda oluşturduğumuz nesne yardımıyla o formu gösteriyor.
            sehirismi = "Adana"; //değişkenimizin değernii değiştiriyor
            cs.GetForcast(sehirismi); //o form sınıfı içerisinde bulunan metota sehrimizi yolluyor
            cs.il.Text = sehirismi;
            cs.UnsplashApi("Adana, türkiye"); //arka plan metoduna sehrimizi yolluyor
            
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Hide(); //üzerinde olduğumuz formu kapatıyor.
            cs.Show(); //en yukarıda oluşturduğumuz nesne yardımıyla o formu gösteriyor.
            sehirismi = "Ankara"; //değişkenimizin değernii değiştiriyor
            cs.GetForcast(sehirismi); //o form sınıfı içerisinde bulunan metota sehrimizi yolluyor
            cs.il.Text = sehirismi;
            cs.UnsplashApi("Ankara"); //arka plan metoduna sehrimizi yolluyor
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            this.Hide(); //üzerinde olduğumuz formu kapatıyor.
            cs.Show(); //en yukarıda oluşturduğumuz nesne yardımıyla o formu gösteriyor.
            sehirismi = "Balıkesir"; //değişkenimizin değernii değiştiriyor
            cs.GetForcast(sehirismi); //o form sınıfı içerisinde bulunan metota sehrimizi yolluyor
            cs.il.Text = sehirismi;
            cs.UnsplashApi("Balıkesir"); //arka plan metoduna sehrimizi yolluyor
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            this.Hide(); //üzerinde olduğumuz formu kapatıyor.
            cs.Show(); //en yukarıda oluşturduğumuz nesne yardımıyla o formu gösteriyor.
            sehirismi = "Eskisehir"; //değişkenimizin değernii değiştiriyor
            cs.GetForcast(sehirismi); //o form sınıfı içerisinde bulunan metota sehrimizi yolluyor
            cs.il.Text = sehirismi;
            cs.UnsplashApi("Eskisehir"); //arka plan metoduna sehrimizi yolluyor
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.Hide(); //üzerinde olduğumuz formu kapatıyor.
            cs.Show(); //en yukarıda oluşturduğumuz nesne yardımıyla o formu gösteriyor.
            sehirismi = "Trabzon"; //değişkenimizin değernii değiştiriyor
            cs.GetForcast(sehirismi); //o form sınıfı içerisinde bulunan metota sehrimizi yolluyor
            cs.il.Text = sehirismi;
            cs.UnsplashApi("Trabzon"); //arka plan metoduna sehrimizi yolluyor
        }


        //Bundan Sonraki Kısım Metotlar


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

        private void sehiryolla_Click(object sender, EventArgs e)
        {
            string sehir = sehiral.Text.ToString(); //sehir ismini alıyoruz
            if (sehir == "") //küçük bir güvenlik işlemi :)
            {
                MessageBox.Show("Lütfen Önce Şehir Giriniz Şehrin Doğru Yazıldığından Emin Olun");
            }
            RenderColumn(sehir); //rendercolumn yani grafik oluşturma metodumuza gönderiyoruz.
        }

        private void sehiral_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar); 

            /* Sehiral isimli textboxt'da bulunan değerlerin sadece metin türünden olmasını sağlıyoruz. 
             */
        }

        private void bunifuTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);

            /* bunifuTextBox1 isimli textboxt'da bulunan değerlerin sadece metin türünden olmasını sağlıyoruz. 
           */
        }

        //Yönlendirmeler

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ProcessStartInfo adres = new ProcessStartInfo("https://github.com/techgokdeniz");
            Process.Start(adres);

            //icon'a tıklanınca bilgisayarımızdaki ana tarayıcı ile github profiline gitmemizi sağlıyor.
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ProcessStartInfo adres = new ProcessStartInfo("https://gokdenizcetin.com.tr");
            Process.Start(adres);

            //icon'a tıklanınca bilgisayarımızdaki ana tarayıcı ile web adresimize gitmemizi sağlıyor
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ProcessStartInfo adres = new ProcessStartInfo("https://www.linkedin.com/in/gokdeniz/");
            Process.Start(adres);

            //icon'a tıklanınca bilgisayarımızdaki ana tarayıcı ile linkedin profiline gitmemizi sağlıyor.
        }

        //Arama işlemi

        private void Ara_Click(object sender, EventArgs e)
        {
            string sehirgir = sehir.Text.ToString(); //arama yapılacak sehri seçiyoruz 
            if (sehirgir == "") //küçük bir kontrol
            {
                MessageBox.Show("Lütfen Önce Şehir Giriniz Şehrin Doğru Yazıldığından Emin Olun");
            }
            this.Hide(); //üzerinde olduğumuz formu kapatıyor.
            cs.Show(); //en yukarıda oluşturduğumuz nesne yardımıyla o formu gösteriyor.
            sehirismi = sehirgir; //değişkenimizin değernii değiştiriyor
            cs.GetForcast(sehirismi); //o form sınıfı içerisinde bulunan metota sehrimizi yolluyor
            cs.il.Text = sehirismi;
            cs.UnsplashApi(sehirismi); //arka plan metoduna sehrimizi yolluyor
        }
    }
}
