using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ILoggerService
    {
        bool CanLog();
        void Log(string message);
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
            return true;
        }

        public void Log(string message)
        {
            // Implement your logging logic here
            Console.WriteLine(message);
        }
    }
}
