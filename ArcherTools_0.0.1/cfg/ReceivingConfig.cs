using ArcherTools_0._0._1.excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArcherTools_0._0._1.cfg
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
            if (type == this.ControlType)
            {
                return this.Coordinates;
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
        [XmlElement("ReceiveCfgVersion")]
        public required string configVersion { get; set; } = ConfigData.ConfigVersion;
        [XmlElement("ItemCfgExcelPath")]
        public string ExcelFilePath { get; set; }
        [XmlArray("ItemCfgSheetList")]
        [XmlArrayItem("WorksheetName")]
        public List<string> ExcelSheetNames { get; set; }

        [XmlArray("MousePositionList")]
        [XmlArrayItem("MousePosition")]
        public List<MousePosition> MousePositionList { get; set; } = new List<MousePosition>();

        public ReceivingConfig() { }

        [System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
        public ReceivingConfig(string excelFilePath, List<MousePosition> mousePositionList)
        {
            ExcelFilePath = excelFilePath;
            if (excelFilePath != null && File.Exists(excelFilePath))
            {
                setExcelSheetNames(excelFilePath);
            }
            MousePositionList = mousePositionList;
            configVersion = ConfigData.ConfigVersion;
        }

        public List<MousePosition> getMousePositions() { return this.MousePositionList; }
        public string? getExcelFilePath() { try { return this.ExcelFilePath; } catch (Exception ex) { Debug.WriteLine($"Excel file path is empty.\n Error Message: {ex.Message}"); return null; } }

        public void setExcelFilePath(string filePath) { this.ExcelFilePath = filePath; }

        public void setExcelSheetNames(string filePath)
        {
            ExcelHandler excelHandler = new ExcelHandler(filePath);
            this.ExcelSheetNames = ExcelHandler.GetWorksheetNames(filePath);
        }

        public void setMousePositions(List<MousePosition> mousePositions) { this.MousePositionList = mousePositions; }

        public void addMousePosition(MousePosition valueToAdd) { this.MousePositionList.Add(valueToAdd); }
    }
}

        

      
       

 

