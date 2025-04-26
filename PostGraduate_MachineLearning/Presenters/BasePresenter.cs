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
        private readonly AppSettings _appSettings;
        private readonly ILoggerService _logger;
        public BasePresenter(AppSettings appSettings, ILoggerService logger)
        {
            this._appSettings = appSettings;
            this._logger = logger;

        }
        protected ILoggerService Logger { get => _logger; }
        protected AppSettings AppSettings { get => _appSettings; }
    }
}
