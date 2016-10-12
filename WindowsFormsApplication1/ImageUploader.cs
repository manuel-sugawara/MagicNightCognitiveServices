using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicNightAzureApplication;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

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

        //public static void Main(string[] args) {
        //    var analyzer = new ImageUploader(args[0], args[1]);
        //    foreach(var file in Directory.GetFiles(args[3])) {
        //        var info = await analyzer.AnalyzeImage(file);
        //        Console.WriteLine("gender:{0}\temotion:{1}");
        //    }
        //}

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
