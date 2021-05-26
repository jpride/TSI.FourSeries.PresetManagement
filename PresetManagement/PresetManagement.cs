﻿using System;
using System.Collections.Generic;
using Crestron.SimplSharp;
using Newtonsoft.Json;


namespace TSI.FourSeries.PresetManagement
{
    public class PresetManagement
    {
        Root root = new Root();
        FileOperations fileOps = new FileOperations();
        string filecontents;
        string _filelocation;

        public string fileLocation
        {
            get { return _filelocation; }
            set { _filelocation = value; }
        }


        public PresetManagement()
        {
            //emtpy initializer
        }

        public void Initialize()
        {
            if (FileOperations.CheckFileExists(_filelocation))
            {
                filecontents = FileOperations.ReadFile(_filelocation);
                CrestronConsole.PrintLine("fileContents: {0}", filecontents.ToString());
            }
            else
            {
                if (Debug.debugEnable) CrestronConsole.PrintLine(Constants.FileNotFoundCreatingNewFileMessage, _filelocation);

                //create a default json string
                try
                {
                    string jsonTemplate = String.Format(Constants.DefaultFileContents);
                    if (Debug.debugEnable) CrestronConsole.PrintLine(jsonTemplate);

                    //Write default string to file
                    FileOperations.WriteFile(_filelocation, jsonTemplate);

                    //Read file
                    filecontents = FileOperations.ReadFile(_filelocation);

                }
                catch (Exception ex)
                {
                    CrestronConsole.PrintLine(Constants.PasswordListObjectInitializeExceptionMessage, ex.Message);
                }
            }

            DeserializeJSON();
            

        }

        //deserialize the json file into the root class
        private void DeserializeJSON()
        {
            try
            {
                if (!filecontents.Equals(String.Empty) && !filecontents.Equals(null))
                {
                    root = JsonConvert.DeserializeObject<Root>(filecontents);
                    GetPresetListFromFile();
                }
                else
                {
                    CrestronConsole.PrintLine(Constants.NothingToDeserializeMessage);
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error(Constants.ErrorConvertingFileContentsMessage, filecontents, e.Message);
                CrestronConsole.PrintLine(Constants.ErrorConvertingFileContentsMessage, filecontents, e.Message);
            }
        }

        //loop thru presets and create events for each one. each event triggers 
        private void GetPresetListFromFile()
        {
            if (!root.presets.Count.Equals(0))
            {
                
                try
                {
                    //for loop to iterate through passwords
                    for (int i = 0; i < root.presets.Count; i++)
                    {
                        //create empty event args of type pwListCodeEventArgs
                        PresetListLoadedEventArgs args = new PresetListLoadedEventArgs()
                        {
                            presetindex = (ushort)(i + 1),
                            presetname = root.presets[i].name,
                            presetnumber = root.presets[i].number,
                        };

                        if (!PresetListLoadedEventToCall.Equals(null))
                        {
                            PresetListLoadedEventToCall(this, args);
                        }
                    }
                }
                catch (Exception e)
                {
                    CrestronConsole.PrintLine(Constants.GetPresetListFromFileExceptionStackTrace, e.Message);
                }
            }
        }

        public event EventHandler<PresetListLoadedEventArgs> PresetListLoadedEventToCall;
    }





}
