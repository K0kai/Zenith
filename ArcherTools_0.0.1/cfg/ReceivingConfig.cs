using ArcherTools_0._0._1.classes;
using ArcherTools_0._0._1.excel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ArcherTools_0._0._1.cfg
{
    public enum ControlType
    {
        ReceiptLineLeftSide,
        ReceiptLineFirstLine,
        ItemSearchWindow,
        ItemSearchInquiry,
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
        CasesPerTierInputBox,
        PowerHouseIcons
    }

    [Serializable]
    public class PowerHouseRectangles
    {
        [XmlElement("ControlType")]
        public ControlType ControlType { get; set; }

        
        [XmlElement("PositionRectangle")]
        public SerializableRectangle rect { get; set; } = new SerializableRectangle();

        public PowerHouseRectangles() { }

        public PowerHouseRectangles(ControlType controlType, SerializableRectangle rectangle)
        {
            ControlType = controlType;
            rect = rectangle;
        }

        public Rectangle getRectangleByType(ControlType type)
        {
            if (type == this.ControlType)
            {
                return this.rect.toRectangle();
            }
            else
            {
                return new Rectangle();
            }
        }

        public Rectangle getRectangle()
        {
            try
            {
                return this.rect.toRectangle();
            }
            catch (Exception e) { Debug.WriteLine(e.Message); return new Rectangle(0, 0, 100, 100); }
        }



        public Rectangle getPosition() { return this.rect.toRectangle(); }
    }

    [Serializable]
    public class ReceivingConfig
    {
        [XmlElement("ReceiveCfgVersion")]
        public required string configVersion { get; set; } = ConfigData.ConfigVersion;
        [XmlElement("ItemCfgExcelPath")]
        public string ExcelFilePath { get; set; }
        [XmlArray("ItemCfgSheetList")]
        [XmlArrayItem("WorksheetName")]
        public List<string> ExcelSheetNames { get; set; }

        [XmlArray("MousePositionList")]
        [XmlArrayItem("PowerHouseRectangles")]
        public List<PowerHouseRectangles> MousePositionList { get; set; } = new List<PowerHouseRectangles>();

        public ReceivingConfig() { }

        [System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
        public ReceivingConfig(string excelFilePath, List<PowerHouseRectangles> mousePositionList)
        {
            ExcelFilePath = excelFilePath;
            if (excelFilePath != null && File.Exists(excelFilePath))
            {
                setExcelSheetNames(excelFilePath);
            }
            MousePositionList = mousePositionList;
            configVersion = ConfigData.ConfigVersion;
        }

        public List<PowerHouseRectangles> getMousePositions() { return this.MousePositionList; }

        public PowerHouseRectangles? getMousePosByType(ControlType ctrlType)
        {
            List<PowerHouseRectangles> mousePositions = this.getMousePositions();
            PowerHouseRectangles? foundPos = null;
            foreach (var mousePos in mousePositions) {
                if (mousePos.ControlType == ctrlType) { foundPos = mousePos; }
                
            }
            return foundPos;
        }

        public string? getExcelFilePath() { try { return this.ExcelFilePath; } catch (Exception ex) { Debug.WriteLine($"Excel file path is empty.\n Error Message: {ex.Message}"); return null; } }

        public void setExcelFilePath(string filePath) { this.ExcelFilePath = filePath; }

        public void setExcelSheetNames(string filePath)
        {
            ExcelHandler excelHandler = new ExcelHandler(filePath);
            this.ExcelSheetNames = ExcelHandler.GetWorksheetNames(filePath);
        }

        public void setMousePositions(List<PowerHouseRectangles> mousePositions) { this.MousePositionList = mousePositions; }

        public void addMousePosition(PowerHouseRectangles valueToAdd) { this.MousePositionList.Add(valueToAdd); }
    }
}

        

      
       

 

