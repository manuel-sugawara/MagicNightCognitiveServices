using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MagicNightAzureApplication {

    public class MagicNight {

        //--- Types ---
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

        public class AnalyzedImage {

            //--- Fields ---
            public readonly Image Image;
            public readonly GenderEnum Gender = GenderEnum.All;
            public readonly EmotionEnum Emotion = EmotionEnum.Happiness;

            //--- Constructors ---
            public AnalyzedImage(string filename) {
                this.Image = Image.FromFile(filename);

                // TODO: load gender and emtion information
            }

            //--- MethoBds ---
            public bool Matches(GenderEnum gender, EmotionEnum emotion) {
                return ((gender == GenderEnum.All) || (gender == Gender)) && ((emotion == EmotionEnum.All) || (emotion == Emotion));
            }
        }

        //--- Fields ---
        private readonly PictureBox[] _pictureBoxes;
        private readonly List<AnalyzedImage> _images = new List<AnalyzedImage>();

        //--- Constructors ---
        public MagicNight(PictureBox[] pictureBoxes, Action doneLoading) {
            _pictureBoxes = pictureBoxes;
            DownloadAndClassifyImages(() => {
                ChangeFilters(GenderEnum.All, EmotionEnum.All);
                doneLoading();
            });
        }

        //--- Methods ---
        public void ChangeFilters(GenderEnum gender, EmotionEnum emotion) {

            //
            //  Filter Images
            //
            var images = _images.Where(image => image.Matches(gender, emotion))
                .Select(image => image.Image)
                .ToArray();

            // Display Images
            DisplayImages(images);
        }

        // Display up to 3 images in application
        private void DisplayImages(IEnumerable<Image> images) {
            var imageArray = images.ToArray();
            for(var index = 0; index < _pictureBoxes.Length; ++index) {
                _pictureBoxes[index].Image = (index < imageArray.Length) ? imageArray[index] : null;
            }
        }

        private async void DownloadAndClassifyImages(Action doneLoading) {
            foreach(var filename in Directory.EnumerateFiles("C:\\MindTouch\\MagicNightCognitiveServices\\images")) {
                _images.Add(new AnalyzedImage(filename));
            }

            ///
            ///  Classify images via Cognitive Services and cache images locally
            ///  Waiting cursor will display until loading is complete
            ///

            // Display normal cursor
            doneLoading();
        }
    }
}