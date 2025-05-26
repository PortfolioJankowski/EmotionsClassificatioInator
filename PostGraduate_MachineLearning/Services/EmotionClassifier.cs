using Microsoft.ML.Data;
using Microsoft.ML;
using Models;
using EmotionClassifier.Models.FormModels;
using EmotionClassifier.Models.Attributes;

namespace EmotionClassifier.Services
{
    [ServiceRegistration(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class EmotionModel
    {
        private readonly string _dataPath;
        private readonly string _modelPath;
        public EmotionModel()
        {
            _dataPath = Path.Combine(AppContext.BaseDirectory, "Model", "tweets_dataset.csv");
            _modelPath = Path.Combine(AppContext.BaseDirectory, "Model", "emotionModel.zip");
        }

        public void TrainModel()
        {
            var mlContext = new MLContext();
            var data = mlContext.Data.LoadFromTextFile<ClassificationResult>(_dataPath, separatorChar: ',', hasHeader: true);


            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(ClassificationResult.TweetText))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(ClassificationResult.Emotion)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var model = pipeline.Fit(data);

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
