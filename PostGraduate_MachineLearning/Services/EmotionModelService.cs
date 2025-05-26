using EmotionClassifier.Models.Attributes;
using Microsoft.ML;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionClassifier.Services
{
    [ServiceRegistration(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class EmotionModelService
    {
        public PredictionEngine<ClassificationResult, EmotionPrediction> PredictionEngine { get; private set; }
        private readonly MLContext _mlContext = new MLContext();
        private ITransformer _model;
        private readonly ILoggerService _logger;

        public EmotionModelService(ILoggerService logger)
        {
            _logger = logger;
            var modelPath = Path.Combine(AppContext.BaseDirectory, "Model", "emotionModel.zip");
            try
            {
                _model = _mlContext.Model.Load(modelPath, out _);
                PredictionEngine = _mlContext.Model.CreatePredictionEngine<ClassificationResult, EmotionPrediction>(_model);
            }
            catch (Exception ex)
            {
                _logger.Log(new LogModel()
                {
                    Level = LogType.Error,
                    Message = $"Error loading model: {ex.Message}",
                    User = UserType.Admin
                });
            }
        }

        public void ReloadModel()
        {
            var modelPath = Path.Combine(AppContext.BaseDirectory, "Model", "emotionModel.zip");
            _model = _mlContext.Model.Load(modelPath, out _);
            PredictionEngine = _mlContext.Model.CreatePredictionEngine<ClassificationResult, EmotionPrediction>(_model);
        }
    }
}
