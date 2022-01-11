namespace TSI.FourSeries.DisplayPresetManagement
{
    using System;
    using System.Collections.Generic;

    public class Preset
    {
        public string input { get; set; }
        public ushort stream { get; set; }
        public string channel { get; set; }
    }

    public class Root
    {
        public List<Preset> presets { get; set; }
    }


    public class PresetListLoadedEventArgs : EventArgs
    {
        public ushort presetindex { get; set; }
        public string presetinput { get; set; }
        public ushort presetstream  { get; set; }
        public string presetchannel { get; set; }
    }



}
