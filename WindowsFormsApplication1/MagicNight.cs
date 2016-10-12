using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApplication1;

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
            public readonly GenderEnum Gender;
            public readonly EmotionEnum Emotion;

            //--- Constructors ---
            public AnalyzedImage(string filename, GenderEnum gender, EmotionEnum emotion) {
                this.Image = Image.FromFile(filename);
                this.Gender = gender;
                this.Emotion = emotion;
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
            var analyzer = new ImageUploader(key1, emotionApiKey);
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

        private void DownloadAndClassifyImages(Action doneLoading) {

            var dummy = 0;
            foreach(var filename in Directory.EnumerateFiles("C:\\Users\\Admin\\magicnight\\MagicNightCognitiveServices\\images")) {

                // TODO: determine gender & emotion for image
                var gender = (GenderEnum)(dummy % 2) + 1;
                var emotion = (EmotionEnum)(dummy % 8) + 1;
                _images.Add(new AnalyzedImage(filename, gender, emotion));
                ++dummy;
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