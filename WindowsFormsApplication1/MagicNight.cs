
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MagicNightAzureApplication {
    public class MagicNight {
        public enum GenderEnum {
            Undefined,
            Male,
            Female
        }

        public enum EmotionEnum {
            Undefined,
            Anger,
            Contempt,
            Disgust,
            Fear,
            Happiness,
            Neutral,
            Sadness,
            Surprise
        }

        public static void ChangeFilters(PictureBox[] pictureBoxes, GenderEnum gender, EmotionEnum emotion) {
            var images = new List<Image>();

            // Filter Images

            DisplayImages(pictureBoxes, images);
        }

        // Display up to 3 images in application
        private static void DisplayImages(PictureBox[] pictureBoxes, IEnumerable<Image> images) {
            var index = 0;
            foreach(var image in images.Take(3)) {
                pictureBoxes[index++].Image = image;
            }
        }
    }
}