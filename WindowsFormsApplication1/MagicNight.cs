
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MagicNightAzureApplication {
    public class MagicNight {
        public enum GenderEnum {
            All,
            Male,
            Female
        }

        public enum EmotionEnum {
            All,
            Anger,
            Contempt,
            Disgust,
            Fear,
            Happiness,
            Neutral,
            Sadness,
            Surprise
        }

        private readonly PictureBox[] _pictureBoxes;

        public MagicNight(PictureBox[] pictureBoxes, Action doneLoading) {
            _pictureBoxes = pictureBoxes;
            DownloadAndClassifyImages(doneLoading);
        }

        public void ChangeFilters(GenderEnum gender, EmotionEnum emotion) {
            var images = new List<Image>();

            ///
            ///  Filter Images
            /// 

            // Display Images
            DisplayImages(images);
        }

        // Display up to 3 images in application
        private void DisplayImages(IEnumerable<Image> images) {
            var index = 0;
            foreach(var image in images.Take(_pictureBoxes.Length)) {
                _pictureBoxes[index++].Image = image;
            }
        }

        private async void DownloadAndClassifyImages(Action doneLoading) {
            
            ///
            ///  Classify images via Cognitive Services and cache images locally
            ///  Waiting cursor will display until loading is complete
            /// 

            // Display normal cursor
            doneLoading();
        }
    }
}