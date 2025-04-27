using EmotionClassifier.Configuration;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionClassifier.Presenters
{
    public class BasePresenter
    {
        private readonly AppSettingsProvider _appSettings;
        private readonly ILoggerService _logger;
        public BasePresenter(AppSettingsProvider appSettings, ILoggerService logger)
        {
            this._appSettings = appSettings;
            this._logger = logger;

        }
        protected ILoggerService Logger { get => _logger; }
        protected AppSettingsProvider AppSettings { get => _appSettings; }
    }
}
