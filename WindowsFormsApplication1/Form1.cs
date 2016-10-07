using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicNightAzureApplication {
    public partial class Form1 : System.Windows.Forms.Form {
        public Form1() {
            InitializeComponent();
        }

        private void SendChangeEvent(object sender, EventArgs e) {
            var comboBox = (ComboBox)sender;
            MagicNight.ChangeFilters(new [] {this.pictureBox1, this.pictureBox2, this.pictureBox3}, comboBox.Name, comboBox.Text);
        }

        private void ResizePictureBoxes(object sender = null, EventArgs e = null) {
            int width = (this.Width - 54) / 3;
            var hieght = this.Height - 108;
            var y = 57;
            pictureBox1.Width = pictureBox2.Width = pictureBox3.Width = width;
            pictureBox1.Height = pictureBox2.Height = pictureBox3.Height = hieght;
            pictureBox1.Location = new Point(12, y);
            pictureBox2.Location = new Point(12 + 7 + width, y);
            pictureBox3.Location = new Point(12 + 7 + 7 + width * 2, y);
        }
    }
}
