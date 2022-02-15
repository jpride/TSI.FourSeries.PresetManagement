namespace TSI.FourSeries.DisplayPresetManagement
{
    using System;
    using System.Text;
    using Crestron.SimplSharp;
    using Crestron.SimplSharp.CrestronIO;


    public class FileOperations
    {
        private static CCriticalSection _fileLock = new CCriticalSection();
        public static Boolean fileExists = false;

        public static bool CheckFileExists(string filePath)
        {
            
            try
            {
                _fileLock.Enter();

                if (File.Exists(filePath))
                {
                    fileExists = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                _fileLock.Leave();
            }
        }

        public static string ReadFile(string filePath)
        {
            string fileContents;

            try
            {
                _fileLock.Enter();
                if (File.Exists(filePath))
                {
                    fileContents = File.ReadToEnd(filePath, Encoding.ASCII);
                    return fileContents;
                }
                else
                {
                    ErrorLog.Error(Constants.FileNotFoundMessage);
                    return string.Empty;
                }
            }
            finally
            {
                _fileLock.Leave();
            }
        }

        public static void WriteFile(string filePath, string payload)
        {
            FileStream fs = new FileStream(filePath, FileMode.Create);

            try
            {
                _fileLock.Enter();
                fs.Write(payload, Encoding.UTF8);
                if (Debug.debugEnable) CrestronConsole.PrintLine(Constants.WriteFilePayloadReport, payload);
            }
            catch (Exception e)
            {
                CrestronConsole.PrintLine(Constants.WriteFileExceptionStackTrace, e.StackTrace);
                ErrorLog.Error(Constants.WriteFileExceptionStackTrace, e.Message);
            }
            finally
            {
                fs.Close();
                _fileLock.Leave();
            }
        }

    }
}