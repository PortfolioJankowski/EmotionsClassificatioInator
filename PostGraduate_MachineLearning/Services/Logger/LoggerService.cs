using EmotionClassifier.Models.Attributes;
using System.ComponentModel;
using System.Configuration;

namespace Services
{
    public interface ILoggerService
    {
        bool CanLog();
        void Log(LogModel message);
    }

    [ServiceRegistration(typeof(ILoggerService),Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class FileLoggerService : ILoggerService
    {
        private  string _folderPath;
        public bool CanLog()
        {
            if (string.IsNullOrEmpty(_folderPath))
            {
                _folderPath = ConfigurationManager.AppSettings["LoggerDirectory"]!;
            }
            bool logFolderExists = Directory.Exists(_folderPath);
            if (!logFolderExists)
            {
                Directory.CreateDirectory(_folderPath);
            }
            return true;
        }

        public void Log(LogModel model)
        {
            if (CanLog())
            {
                string filePath = Path.Combine(_folderPath, $"{DateTime.Now:yyyy-MM-dd}.log");
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{model.Level}] {model.Message} (User: {model.User}){Environment.NewLine}";
                File.AppendAllText(filePath, logMessage);
            }
        }
    }

    public class LogModel
    {
        public string Message { get; set; }
        public LogType Level{ get; set; }
        public UserType User { get; set; }  

    }

    public enum LogType
    {
        [Description("Info")] Info     = 1,
        [Description("Debug")] Warning = 2,
        [Description("Error")] Error   = 3,
    }

    public enum UserType
    {
        [Description("Admin")] Admin  = 1,
        [Description("User")]  User   = 2,
    }   
}
