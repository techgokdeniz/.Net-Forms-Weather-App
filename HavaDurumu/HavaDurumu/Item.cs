using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HavaDurumu
{
    public partial class Item : UserControl
    {
        public Item()
        {
            InitializeComponent();
        }

        private string _baslik;
        private Image _resim;

        [Category("Custom Props")]
        public string Baslik
        {
            get { return _baslik; }
            set { _baslik = value; label1.Text = value; }
        }

        [Category("Custom Props")]
        public Image Resim
        {
            get { return _resim; }
            set { _resim = value; bunifuShadowPanel1.BackgroundImage = value; }
        }

        private void Item_Load(object sender, EventArgs e)
        {

        }
    }
}
