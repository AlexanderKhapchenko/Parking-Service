// TODO: implement the LogService class from the ILogService interface.
//+       One explicit requirement - for the read method, if the file is not found, an InvalidOperationException should be thrown
//+       Other implementation details are up to you, they just have to match the interface requirements
//+       and tests, for example, in LogServiceTests you can find the necessary constructor format.
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using System;
using System.IO;
using System.Text;

namespace CoolParking.BL.Services
{
    public class LogService : ILogService
    {
        private readonly string logFilePath;

        public LogService(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }

        public LogService()
        {
            this.logFilePath = Settings.logPath;
        }

        public string LogPath
        {
            get { return logFilePath; }
        }

        public string Read()
        {
            string result = "";
            try
            {
                using (StreamReader sr = new StreamReader(logFilePath, System.Text.Encoding.ASCII))
                {
                    result += sr.ReadToEnd();
                }
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException();
            }
            catch (Exception)
            {
                throw new Exception();
            }
            return result;
        }

        public void Write(string logInfo)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true, System.Text.Encoding.ASCII))
                {
                    sw.Write(logInfo + "\n");
                }
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException();
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
    }
}