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
        PowerHouseUpperTab
    }

    [Serializable]
    public class PowerHouseRectangles
    {
        [XmlElement("ControlType")]
        public ControlType ControlType { get; set; }


        [XmlElement("PositionRectangle")]
        public SerializableRectangle rect { get; set; } = new SerializableRectangle();

        public PowerHouseRectangles() { }

        public PowerHouseRectangles(PowerHouseRectangles rectOverride) {
            ControlType = rectOverride.ControlType;
            rect = rectOverride.rect;
        }

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
        public string configVersion { get; set; } = ConfigData.ConfigVersion;
        [XmlElement("ItemCfgExcelPath")]
        public string ExcelFilePath { get; set; }
        [XmlArray("ItemCfgSheetList")]
        [XmlArrayItem("WorksheetName")]
        public List<string> ExcelSheetNames { get; set; }

        [XmlArray("RectanglePositionList")]
        [XmlArrayItem("PowerHouseRectangles")]
        public List<PowerHouseRectangles> RectanglePositionList { get; set; }

        public ReceivingConfig() { }

        public ReceivingConfig(ReceivingConfig rcvCfgOverride)
        {        
            
                ExcelFilePath = rcvCfgOverride.ExcelFilePath;
            if (rcvCfgOverride.ExcelFilePath != null)
            {
                RectanglePositionList = rcvCfgOverride.RectanglePositionList
               .Select(rect => new PowerHouseRectangles(rect))
               .ToList();
            }
            else
            {
                RectanglePositionList = new List<PowerHouseRectangles>();
            }
            configVersion = rcvCfgOverride.configVersion;
            if (ExcelFilePath != null && File.Exists(ExcelFilePath))
            {
                setExcelSheetNames(ExcelFilePath);
            }

        }

        [System.Diagnostics.CodeAnalysis.SetsRequiredMembersAttribute]
        public ReceivingConfig(string excelFilePath = "", List<PowerHouseRectangles> rectPosList = null)
        {
            ExcelFilePath = excelFilePath;
            RectanglePositionList = rectPosList;
            configVersion = ConfigData.ConfigVersion;
            

            if (ExcelFilePath != null && File.Exists(ExcelFilePath))
            {
                setExcelSheetNames(ExcelFilePath);
            }
            
        }

        public List<PowerHouseRectangles> getRectangles() { return this.RectanglePositionList; }

        public PowerHouseRectangles getRectByType(ControlType ctrlType)
        {
            var rectReturn = new PowerHouseRectangles(ctrlType, new SerializableRectangle(new Rectangle(0, 0, 150, 150)));
            if (this.RectanglePositionList != null)
            {
                foreach (var rect in this.RectanglePositionList)
                {
                    if (rect.ControlType == ctrlType)
                    {
                        rectReturn = rect.getPwhRectByType(ctrlType);
                    }

                }
            }
            return rectReturn;
        }

        public int getRectIndexByType(ControlType ctrlType)
        {
            var rectReturn = new PowerHouseRectangles(ctrlType, new SerializableRectangle(new Rectangle(0, 0, 150, 150)));
            for (var i = 0; i < this.RectanglePositionList.Count; i++)
            {
                if (!this.RectanglePositionList[i].getPwhRectByType(ctrlType).rect.toRectangle().IsEmpty)
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
                    Debug.WriteLine("configversion is different");
                    return true;
                }
                if (config.ExcelFilePath != this.ExcelFilePath)
                {
                    Debug.WriteLine("filepath is different");
                    return true;
                }
                if (this.ExcelSheetNames != null && config.ExcelSheetNames != null)
                {
                    if (config.ExcelSheetNames.Count != this.ExcelSheetNames.Count)
                    {
                        Debug.WriteLine("sheet count is different");
                        return true;
                    }
                    else
                    {
                        foreach (var sheetName in this.ExcelSheetNames)
                        {
                            if (!config.ExcelSheetNames.Contains(sheetName))
                            {
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("sheets are null");
                    return true;
                }
                foreach (var sheetName in this.ExcelSheetNames)
                {
                    if (!config.ExcelSheetNames.Contains(sheetName))
                    {
                        Debug.WriteLine("sheet name is different");
                        return true;
                    }
                }

                if (config.RectanglePositionList.Count != this.RectanglePositionList.Count)
                {
                    Debug.WriteLine("rect list size is different");
                    return true;
                }
                else
                {
                    foreach (var rect in this.RectanglePositionList)
                    {
                        Debug.WriteLine($"{rect.getRectangle().Location} vs {config.getRectByType(rect.ControlType).getRectangle().Location}\n" +
                            $"{rect.ControlType}, {config.getRectByType(rect.ControlType).ControlType}");
                        if (rect.getRectangle().Location != config.getRectByType(rect.ControlType).getRectangle().Location || rect.getRectangle().Size != config.getRectByType(rect.ControlType).getRectangle().Size)
                        {
                            Debug.WriteLine("rect position is different");
                            return true;
                        }
                    }
                    return false;
                }
                
            }
            else
            {
                Debug.WriteLine("reached the end");
                return true;
            }
        }

        public void setMousePositions(List<PowerHouseRectangles> mousePositions) { this.RectanglePositionList = mousePositions; }


        public void addMousePosition(PowerHouseRectangles valueToAdd)
        {
            if (!this.containsCtrlType(valueToAdd.ControlType))
            {
                this.RectanglePositionList.Add(valueToAdd);
                Debug.WriteLine("doesnt contain, adding item");
            }
            else
            {
                var conflictingRect = getRectByType(valueToAdd.ControlType);
                if (this.RectanglePositionList.Contains(conflictingRect))
                {
                    this.RectanglePositionList[getRectIndexByType(valueToAdd.ControlType)] = valueToAdd;
                    Debug.WriteLine("contains, adding item");
                }
            }
        }
    }
}

        

      
       

 

