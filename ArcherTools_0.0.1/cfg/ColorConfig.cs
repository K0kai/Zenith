using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Emgu.CV.Structure;

namespace ArcherTools_0._0._1.cfg
{
    [Serializable]
    public class ColorConfig
    {
        public static ColorConfig currentInstance;
        public string PresetName { get; internal set; }
        [XmlIgnore]
        public Color BackgroundColor { get; set; }
        [XmlIgnore]
        public Color PrimaryLabelColor { get; set; }
        [XmlIgnore]
        public Color SecondaryLabelColor { get; set;}
        [XmlIgnore]
        public Color ButtonColor { get; set; }
        [XmlIgnore]
        public Color InputBoxColor { get; set; }
        [XmlIgnore]
        public Color TextColor { get; set; }

        [XmlElement("BackgroundColor")]
        public int BackgroundColorAsARGB {
            get { return BackgroundColor.ToArgb(); }
            set { BackgroundColor = Color.FromArgb(value); }
        }
        [XmlElement("PrimaryLabelColor")]
        public int PrimaryLabelColorAsARGB { get; set; }
        [XmlElement("SecondaryLabelColor")]
        public int SecondaryLabelColorAsARGB { get; set; }
        [XmlElement("ButtonColor")]
        public int ButtonColorAsARGB { get; set; }
        [XmlElement("InputBox")]
        public int InputBoxColorAsARGB { get; set; }
        [XmlElement("TextColor")]
        public int TextColorAsARGB { get; set; }

        public ColorConfig(string presetName, Color backgroundColor, Color primaryLabelColor, Color secondaryLabelColor, Color buttonColor, Color inputBoxColor, Color textColor)
        {
            PresetName = presetName;
            BackgroundColor = backgroundColor;
            PrimaryLabelColor = primaryLabelColor;
            SecondaryLabelColor = secondaryLabelColor;
            ButtonColor = buttonColor;
            InputBoxColor = inputBoxColor;
            TextColor = textColor;
        }

        internal static void GenerateDefaults()
        {
            Color bgColor = Color.FromArgb(64, 64, 64);
            Color primaryLblColor = Color.Lavender;
            Color secondaryLblColor = Color.WhiteSmoke;
            Color btnColor = Color.DimGray;
            Color inputBox = Color.FromArgb(40, 40, 40);
            Color textColor = Color.WhiteSmoke;
            string presetName = "Dark";
            ColorConfig defaultDark = new ColorConfig(presetName, bgColor, primaryLblColor, secondaryLblColor, btnColor, inputBox, textColor);
            ColorPresets initPresetList = new ColorPresets(new List<ColorConfig> {defaultDark});
        }

        public override string ToString()
        {
            return PresetName;
        }



    }
}
