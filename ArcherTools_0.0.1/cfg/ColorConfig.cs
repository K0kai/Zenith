using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.cfg
{
    public class ColorConfig
    {
        public static ColorConfig currentInstance;
        public string PresetName { get; internal set; }
        public Color BackgroundColor { get; set; }
        public Color PrimaryLabelColor { get; set; }
        public Color SecondaryLabelColor { get; set;}
        public Color ButtonColor { get; set; }
        public Color InputBoxColor { get; set; }
        public Color TextColor { get; set; }

        public ColorConfig(Color backgroundColor, Color primaryLabelColor, Color secondaryLabelColor, Color buttonColor, Color inputBoxColor, Color textColor)
        {
            BackgroundColor = backgroundColor;
            PrimaryLabelColor = primaryLabelColor;
            SecondaryLabelColor = secondaryLabelColor;
            ButtonColor = buttonColor;
            InputBoxColor = inputBoxColor;
            TextColor = textColor;
        }
    }
}
