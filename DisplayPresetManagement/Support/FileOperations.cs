namespace TSI.FourSeries.DisplayPresetManagement
{
    using System;
    using System.Text;
    using Crestron.SimplSharp;
    using Crestron.SimplSharp.CrestronIO;


    public class FileOperations
    {
        private static CCriticalSection _csection = new CCriticalSection();

        public static Boolean CheckFileExists(string filePath)
        {
            //lock (_lockingVar)
            _csection.Enter();

            try
            {
                if (File.Exists(filePath))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                _csection.Leave();
            }
    
            
        }

        public static string ReadFile(string filePath)
        {
            string fileContents;

            _csection.Enter();
            try
            {
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
                _csection.Leave();
            }
        }

        public static void WriteFile(string filePath, string payload)
        {
            FileStream fs = new FileStream(filePath, FileMode.Create);

            _csection.Enter();

            try
            {
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
                _csection.Leave();
            }
        }

    }
}