using System;
using System.Collections.Generic;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronIO;

namespace TSI.FourSeries.PresetManagement
{
    public class FileOperations
    {

        public static string fileContents;
        private static readonly object lockingVar = new object();

        public static Boolean CheckFileExists(string filePath)
        {
            lock (lockingVar)
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
        }

        public static string ReadFile(string filePath)
        {
            string fileContents;


            lock (lockingVar)
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

        }

        public static void WriteFile(string filePath, string payload)
        {
            FileStream fs = new FileStream(filePath, FileMode.Create);

            lock (lockingVar)
            {
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
                }
            }

        }

    }
}