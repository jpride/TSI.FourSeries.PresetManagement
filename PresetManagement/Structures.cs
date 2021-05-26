using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TSI.FourSeries.PresetManagement
{

    public class Preset
    {
        public string name { get; set; }
        public string number { get; set; }
    }

    public class Root
    {
        public List<Preset> presets { get; set; }
    }


    public class PresetListLoadedEventArgs : EventArgs
    {
        public string[] names { get; set; }
        public string[] numbers  { get; set; }
        public ushort listCount { get; set; }
    }

}
