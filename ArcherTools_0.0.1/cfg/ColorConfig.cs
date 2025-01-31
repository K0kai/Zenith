using System.Xml.Serialization;

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
        [XmlIgnore]
        public Color DetailsColor {  get; set; }

        [XmlElement("BackgroundColor")]
        public int BackgroundColorAsARGB {
            get { return BackgroundColor.ToArgb(); }
            set { BackgroundColor = Color.FromArgb(value); }
        }
        [XmlElement("PrimaryLabelColor")]
        public int PrimaryLabelColorAsARGB {
            get { return PrimaryLabelColor.ToArgb(); }
            set { PrimaryLabelColor = Color.FromArgb(value); } }
        [XmlElement("SecondaryLabelColor")]
        public int SecondaryLabelColorAsARGB {
            get { return SecondaryLabelColor.ToArgb(); }
            set { SecondaryLabelColor = Color.FromArgb(value); }
        }
        [XmlElement("ButtonColor")]
        public int ButtonColorAsARGB {
            get { return ButtonColor.ToArgb(); }
            set { ButtonColor = Color.FromArgb(value); }
        }
        [XmlElement("InputBox")]
        public int InputBoxColorAsARGB {
            get { return InputBoxColor.ToArgb(); }
            set { InputBoxColor = Color.FromArgb(value); }
        }
        [XmlElement("TextColor")]
        public int TextColorAsARGB {
            get { return TextColor.ToArgb(); }
            set { TextColor = Color.FromArgb(value); }
        }
        [XmlElement("DetailsColor")]
        public int DetailsColorAsARGB
        {
            get { return DetailsColor.ToArgb(); }
            set { DetailsColor = Color.FromArgb(value); }
        }

        public ColorConfig(string presetName, Color backgroundColor, Color primaryLabelColor, Color secondaryLabelColor, Color buttonColor, Color inputBoxColor, Color textColor, Color detailsColor)
        {
            PresetName = presetName;
            BackgroundColor = backgroundColor;
            PrimaryLabelColor = primaryLabelColor;
            SecondaryLabelColor = secondaryLabelColor;
            ButtonColor = buttonColor;
            InputBoxColor = inputBoxColor;
            TextColor = textColor;
            DetailsColor = detailsColor;
        }

        internal static void GenerateDefaults()
        {
            Color bgColor = Color.FromArgb(64, 64, 64);
            Color primaryLblColor = Color.Lavender;
            Color secondaryLblColor = Color.WhiteSmoke;
            Color btnColor = Color.DimGray;
            Color inputBox = Color.FromArgb(40, 40, 40);
            Color textColor = Color.WhiteSmoke;
            Color detailsColor = Color.FromArgb(184, 44, 95);
            string presetName = "Dark";
            ColorConfig defaultDark = new ColorConfig(presetName, bgColor, primaryLblColor, secondaryLblColor, btnColor, inputBox, textColor, detailsColor);
            ColorPresets initPresetList = new ColorPresets(new List<ColorConfig> {defaultDark});
        }

        public override string ToString()
        {
            return PresetName;
        }



    }
}
