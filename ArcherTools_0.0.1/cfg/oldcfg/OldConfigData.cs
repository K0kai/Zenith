using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace ArcherTools_0._0._1.cfg.oldcfg
{
    public enum ControlType
    {
        ReceiptLineLeftSide,
        ReceiptLineFirstLine,
        ItemSearchInputBox,
        ItemMaintenanceBox,
        ItemConfigurationBox,
        NumPiecesInputBox,
        PalletWeightInputBox,
        PalletHeightInputBox,
        PalletWidthInputBox,
        PalletDepthInputBox,
        CasesWeightInputBox,
        CasesHeightInputBox,
        CasesWidthInputBox,
        CasesDepthInputBox,
        CasesPerPalletInputBox,
        CasesPerTierInputBox
    }

    [Serializable]
    public class MousePosition
    {
        [XmlElement("ControlType")]
        public ControlType ControlType { get; set; }

        [XmlArray("Coordinates")]
        [XmlArrayItem("Coordinate")]
        public List<int> Coordinates { get; set; } = new List<int>();

        public MousePosition() { }

        public MousePosition(ControlType controlType, List<int> coordinates)
        {
            ControlType = controlType;
            Coordinates = coordinates;
        }

        public List<int> getPositionByType(ControlType type)
        {
            if (type == ControlType)
            {
                return Coordinates;
            }
            else
            {
                return new List<int> { 0, 0 };
            }
        }
    }

    [Serializable]
    public class ReceivingConfig
    {
        public required string ExcelFilePath { get; set; }
        public required string ExcelSheetName { get; set; }

        [XmlArray("MousePositionList")]
        [XmlArrayItem("MousePosition")]
        public List<MousePosition> MousePositionList { get; set; } = new List<MousePosition>();

        public ReceivingConfig() { }

        [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
        public ReceivingConfig(string excelFilePath, string excelSheetName, List<MousePosition> mousePositionList)
        {
            ExcelFilePath = excelFilePath;
            ExcelSheetName = excelSheetName;
            MousePositionList = mousePositionList;
        }

        public List<MousePosition> getMousePositions() { return MousePositionList; }
        public string getExcelFilePath() { return ExcelFilePath; }

        public void setExcelFilePath(string filePath) { ExcelFilePath = filePath; }

        public void setMousePositions(List<MousePosition> mousePositions) { MousePositionList = mousePositions; }

        public void addMousePosition(MousePosition valueToAdd) { MousePositionList.Add(valueToAdd); }

    }

    public class OldConfigData
    {
        public static ReceivingConfig _receivingConfig;

        public OldConfigData(ReceivingConfig receivingConfig)
        {
            _receivingConfig = receivingConfig;
        }

        public static void SerializeConfig()
        {
            var configData = _receivingConfig;
            if (configData != null)
            {
                string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
                string fileName = "Config.xml";
                string filePath = Path.Combine(directoryPath, fileName);
                try
                {
                    if (Directory.Exists(directoryPath))
                    {
                        Process.Start("explorer.exe", directoryPath);
                    }
                }
                catch (Exception ex)
                {
                }

                var serializer = new XmlSerializer(typeof(ReceivingConfig));
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(fileStream, configData);
                    Console.WriteLine($"Config file {(File.Exists(filePath) ? "updated" : "created")} at {filePath}");
                }

            }
            else
            {
#if NOTDEBUG
                Console.WriteLine($"Configuration data is empty, doing nothing instead.");
#else
                Debug.WriteLine($"Configuration data is empty, doing nothing instead.");
#endif
            }


        }

        public static ReceivingConfig getInstance() { return _receivingConfig; }

        public static void UnserializeConfig()
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "Config.xml";
            string filePath = Path.Combine(directoryPath, fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(ReceivingConfig));
                    using (var fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        _receivingConfig = (ReceivingConfig)serializer.Deserialize(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while deserializing: {ex}");
                }
            }

        }
    }
}