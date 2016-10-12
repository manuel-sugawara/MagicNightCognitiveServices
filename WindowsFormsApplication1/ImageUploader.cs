using System.IO;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Face;

namespace WindowsFormsApplication1 {

    public class ImageMetaData {
        public readonly string Gender;
        public readonly string Emotion;

        public ImageMetaData(string gender, string emotion) {
            Gender = gender;
            Emotion = emotion;
        }
    }

    public class ImageUploader {

        //--- Fields ---
        private readonly IFaceServiceClient _faceServiceClient;
        private readonly EmotionServiceClient _emotionServiceClient;

        //--- Constructors ---
        public ImageUploader(string faceKey, string emotionKey) {
            _faceServiceClient = new FaceServiceClient(faceKey);
            _emotionServiceClient = new EmotionServiceClient(emotionKey);
        }

        public async Task<ImageMetaData> AnalyzeImage(string imageFilePath) {
            using(Stream imageFileStream = File.OpenRead(imageFilePath)) {
                var faces = await _faceServiceClient.DetectAsync(imageFileStream);
                var theFace = faces[0];
                var gender = theFace.FaceAttributes.Gender;
                imageFileStream.Seek(0, SeekOrigin.Begin);
                var emotions = await _emotionServiceClient.RecognizeAsync(imageFileStream);
                var score = emotions[0].Scores;
                var emotion = FindEmotion(score);
                return new ImageMetaData(gender, emotion);
            }
        }

        private string FindEmotion(Scores score) {
            var max = 0f;
            var res = "??";
            if(score.Anger > max) {
                res = "Anger";
            }
            if(score.Contempt > max) {
                res = "Contempt";
            }
            if(score.Disgust > max) {
                res = "Disgust";
            }
            if(score.Fear > max) {
                res = "Fear";
            }
            return res;
        }
    }
}
