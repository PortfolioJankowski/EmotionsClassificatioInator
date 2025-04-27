using Microsoft.ML.Data;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using EmotionClassifier.Models.FormModels;

namespace EmotionClassifier.Services
{
    public class EmotionModel
    {
        private readonly MenuFormModel _request;
        private readonly string _dataPath;
        private readonly string _modelPath;
        public EmotionModel(MenuFormModel request)
        {
            _request = request;
            _dataPath = Path.Combine(AppContext.BaseDirectory, "Model", "tweets_dataset.csv");
            _modelPath = Path.Combine(AppContext.BaseDirectory, "Model", "emotionModel.zip");
        }

        public void TrainModel()
        {
            var mlContext = new MLContext();

            // 1. Wczytanie danych
            var data = mlContext.Data.LoadFromTextFile<ClassificationResult>(_dataPath, separatorChar: ',', hasHeader: true);

            // 2. Tworzenie pipeline
            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(ClassificationResult.TweetText))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(ClassificationResult.Emotion)))
           .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // 3. Trenowanie modelu
            var model = pipeline.Fit(data);

            // 4. Zapis modelu
            mlContext.Model.Save(model, data.Schema, _modelPath);
            Console.WriteLine("Model zapisany!");
        }
    }

    // Klasa predykcji
    public class EmotionPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedEmotion { get; set; }
    }
}
