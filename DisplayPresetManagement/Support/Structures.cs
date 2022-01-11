namespace TSI.FourSeries.DisplayPresetManagement
{
    using System;
    using System.Collections.Generic;

    public class Preset
    {
        public string input { get; set; }
        public ushort stream { get; set; }
        public string channelNumber { get; set; }
        public string channelName { get; set; }
    }

    public class DisplayPresetListObject
    {
        public List<Preset> PresetList { get; set; }
    }


    public class PresetListLoadedEventArgs : EventArgs
    {
        public ushort presetindex { get; set; }
        public string presetinput { get; set; }
        public ushort presetstream  { get; set; }
        public string presetchannelnumber { get; set; }
        public string presetchannelname { get; set; }
    }



}
