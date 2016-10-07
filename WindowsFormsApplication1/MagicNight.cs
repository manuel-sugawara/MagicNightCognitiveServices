
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MagicNightAzureApplication {
    public class MagicNight {
        public static void ChangeFilters(PictureBox[] pictureBoxes, string filterName, string filterValue) {
            var images = new List<Image>();

            // Ask asure about our images

            DisplayImages(pictureBoxes, images);
        }

        private static void DisplayImages(PictureBox[] pictureBoxes, IEnumerable<Image> images) {
            var index = 0;
            foreach(var image in images.Take(3)) {
                pictureBoxes[index++].Image = image;
            }
        }
    }
}