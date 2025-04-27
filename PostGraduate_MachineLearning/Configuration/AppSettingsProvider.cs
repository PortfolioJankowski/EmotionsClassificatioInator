using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionClassifier.Configuration
{
    public class AppSettingsProvider
    {
        public int InitialFormWidth { get; set; }
        public int InitialFormHeight { get; set; }
        public int FinalFormHeight { get; set; }
        public string APIKey { get; set; }
        public string APIKeySecret { get; set; }
        public string BearerToken { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string LoggerDirectory { get; set; }
    }
}
