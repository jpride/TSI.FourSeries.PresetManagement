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
        public string band { get; set; }
    }

    public class Root
    {
        public List<Preset> presets { get; set; }
    }


    public class PresetListLoadedEventArgs : EventArgs
    {
        public ushort presetindex { get; set; }
        public string presetname { get; set; }
        public string presetnumber  { get; set; }
        public string presetband { get; set; }
    }

}
