using System.ComponentModel;

namespace Services
{
    public interface ILoggerService
    {
        bool CanLog();
        void Log(LogModel message);
    }

    public class FileLoggerService : ILoggerService
    {
        private readonly string _folderPath;
        public FileLoggerService(string folderPath)
        {
             _folderPath = folderPath;
        }
        public bool CanLog()
        {
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
        public int User { get; set; }  

    }

    public enum LogType
    {
        [Description("Info")] Info     = 1,
        [Description("Debug")] Warning = 2,
        [Description("Error")] Error   = 3,
    }
}
