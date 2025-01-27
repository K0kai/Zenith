using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.cfg
{
    [Serializable]
    internal class ColorPresets
    {
        public static ColorPresets _instance;
        public static ColorConfig SelectedPreset { get; set; }
        public List<ColorConfig> Presets { get; internal set; }

        public ColorPresets() { }

        public ColorPresets(List<ColorConfig> presets)
        {
            Presets = presets;
            if (_instance == null)
            {
                _instance = this;
            }
        }

        public void SetPreset(string PresetName)
        {
            try
            {
                if (Presets.Count != 0)
                {
                    foreach (var preset in Presets)
                    {
                        if (preset.PresetName == PresetName)
                        {
                            SelectedPreset = preset;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to set preset.\nReason:{ex.Message}, {ex.StackTrace}");
            }
        }


    }
}
