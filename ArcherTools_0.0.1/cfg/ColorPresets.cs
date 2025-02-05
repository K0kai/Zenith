using System.ComponentModel;
using System.Diagnostics;

namespace ArcherTools_0._0._1.cfg
{
    [Serializable]
    internal class ColorPresets : INotifyPropertyChanged
    {
        public static ColorPresets _instance;
        private ColorConfig selectedPreset;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ColorConfig SelectedPreset
        {
            get
            {
                return selectedPreset;
            }
            set
            {
                lock (_instance)
                {
                    if (value != selectedPreset)
                    {
                        selectedPreset = value;
                        NotifyPropertyChanged("SelectedPreset");
                    }
                }
                }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
            if (SettingsUserControl._instance != null)
            {
                SettingsUserControl._instance.Invalidate();
            }
        }

        public List<ColorConfig> Presets { get; internal set; } = new List<ColorConfig>();

        public ColorPresets() { }

        public ColorPresets(List<ColorConfig> presets)
        {
            Presets = presets;
            if (_instance == null)
            {
                _instance = this;
            }
        }

    

        public void SetPreset(object selPreset)
        {
            try
            {
                if (selPreset.GetType() == typeof(ColorConfig))
                {
                    if (Presets.Count != 0)
                    {
                        foreach (var preset in Presets)
                        {
                            if (preset == selPreset)
                            {
                                SelectedPreset = preset;
                            }
                        }
                    }
                }
                else if (selPreset.GetType() == typeof(string))
                {
                    if (Presets.Count != 0)
                    {
                        foreach (var preset in Presets)
                        {
                            if (preset.PresetName == (string) selPreset)
                            {
                                SelectedPreset = preset;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to set preset.\nReason:{ex.Message}, {ex.StackTrace}");
            }
        }

        public ColorConfig GetCurrentPreset()
        {
            return SelectedPreset;
        }


    }
}
