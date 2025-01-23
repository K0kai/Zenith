using ArcherTools_0._0._1.classes;
using ArcherTools_0._0._1.excel;
using NPOI.SS.Formula.Functions;
using System.Diagnostics;
using System.Xml.Serialization;

namespace ArcherTools_0._0._1.cfg
{
    public enum ControlType
    {
        ReceiptLineWindow,
        ReceiptLineFirstLine,
        ItemSearchWindow,
        ItemSearchInquiry,
        ItemMaintenanceWindow,
        ItemConfigurationWindow,
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

        
        public PowerHouseRectangles getPwhRectByType(ControlType type)
        {
            if (type == this.ControlType)
            {
                return this;
            }
            else
            {
                return new PowerHouseRectangles();
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

        public void setRectangle(Rectangle newRect)
        {
            this.rect = new SerializableRectangle(newRect);
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

        [XmlArray("RectanglePositionList")]
        [XmlArrayItem("PowerHouseRectangles")]
        public List<PowerHouseRectangles> RectanglePositionList { get; set; }

        public ReceivingConfig() { }

        [System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
        public ReceivingConfig(string excelFilePath = "", List<PowerHouseRectangles> rectPosList = null, ReceivingConfig rcvCfgOverride = null)
        {
            ExcelFilePath = excelFilePath;
            RectanglePositionList = rectPosList;
            configVersion = ConfigData.ConfigVersion;
            if (rcvCfgOverride != null)
            {
                ExcelFilePath = rcvCfgOverride.ExcelFilePath;
                RectanglePositionList = rcvCfgOverride.RectanglePositionList;
                configVersion = rcvCfgOverride.configVersion;
            }

            if (excelFilePath != null && File.Exists(excelFilePath))
            {
                setExcelSheetNames(excelFilePath);
            }
            
        }

        public List<PowerHouseRectangles> getRectangles() { return this.RectanglePositionList; }

        public PowerHouseRectangles getRectByType(ControlType ctrlType)
        {
            var rectReturn = new PowerHouseRectangles(ctrlType, new SerializableRectangle(new Rectangle(0, 0, 150, 150)));
            foreach (var rect in this.RectanglePositionList)
            {
                if (rect.ControlType == ctrlType)
                {
                    rectReturn = rect.getPwhRectByType(ctrlType);
                }

            }
            return rectReturn;
        }

        public int getRectIndexByType(ControlType ctrlType)
        {
            var rectReturn = new PowerHouseRectangles(ctrlType, new SerializableRectangle(new Rectangle(0, 0, 150, 150)));
            for (var i = 0; i < this.RectanglePositionList.Count; i++)
            {
                if (!RectanglePositionList[i].getPwhRectByType(ctrlType).rect.toRectangle().IsEmpty)
                {
                    return i;
                }                
            }
            return -1;
        }

        public bool containsCtrlType(ControlType ctrlType)
        {
            if ( this.RectanglePositionList.Count == 0 || this.RectanglePositionList == null)
            {
                Debug.WriteLine("null list");
                return false;
            }
            foreach (var rect in this.RectanglePositionList)
            {
                if (rect.ControlType == ctrlType)
                {
                    Debug.WriteLine("conflict");
                    return true;
                }
            }
            Debug.WriteLine("nothing");
            return false;
        }

        public bool configExcel()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel Sheet (*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.Multiselect = false;
            string filePath;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
                setExcelFilePath(filePath);
                ConfigData cfgData = new ConfigData(ConfigData._userConfig, ConfigData._receivingConfig, ConfigData._toolConfig);
                cfgData.PrepareForSerialization();
                ConfigData.SerializeConfigData();
                return true;
            }
            else
            {
                return false;
            }
        }

        public string? getExcelFilePath() { try { return this.ExcelFilePath; } catch (Exception ex) { Debug.WriteLine($"Excel file path is empty.\n Error Message: {ex.Message}"); return null; } }

        public void setExcelFilePath(string filePath) { this.ExcelFilePath = filePath; setExcelSheetNames(filePath); }

        public void setExcelSheetNames(string filePath)
        {
            ExcelHandler excelHandler = new ExcelHandler(filePath);
            this.ExcelSheetNames = ExcelHandler.GetWorksheetNames(filePath);
        }

        public bool ConfigIsDifferent(ReceivingConfig config)
        {
            if (config != null)
            {
                if (config.configVersion != this.configVersion)
                {
                    return true;
                }
                if (config.ExcelFilePath != this.ExcelFilePath)
                {
                    return true;
                }
                if (config.ExcelSheetNames.Count != this.ExcelSheetNames.Count)
                {
                    return true;
                }
                foreach (var sheetName in this.ExcelSheetNames)
                {
                    if (!config.ExcelSheetNames.Contains(sheetName))
                    {
                        return true;
                    }
                }

                if (config.RectanglePositionList.Count != this.RectanglePositionList.Count)
                {
                    return true;
                }
                else
                {
                    foreach (var rect in config.RectanglePositionList)
                    {
                        if (!rect.getRectangle().Equals(this.getRectByType(rect.ControlType)))
                        {
                            return true;
                        }
                    }
                    return false;
                }
                
            }
            else
            {
                return true;
            }
        }

        public void setMousePositions(List<PowerHouseRectangles> mousePositions) { this.RectanglePositionList = mousePositions; }


        public void addMousePosition(PowerHouseRectangles valueToAdd)
        {
            if (!this.containsCtrlType(valueToAdd.ControlType))
            {
                Debug.WriteLine("doesnt contain");
                this.RectanglePositionList.Add(valueToAdd);
                Debug.WriteLine("adding item");
            }
            else
            {
                var conflictingRect = getRectByType(valueToAdd.ControlType);
                if (RectanglePositionList.Contains(conflictingRect))
                {
                    RectanglePositionList[getRectIndexByType(valueToAdd.ControlType)] = valueToAdd;
                }
            }
        }
    }
}

        

      
       

 

