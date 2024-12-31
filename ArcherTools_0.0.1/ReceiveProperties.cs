/*
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.controllers;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ArcherTools_0._0._1
{
    public partial class ReceiveProperties : UserControl
    {
        public ReceiveProperties()
        {

            InitializeComponent();
            UpdateLabels();

        }

        private void goBackRCV_btn_Click(object sender, EventArgs e)
        {
            PageHandler pagehandler = PageHandler.GetInstance();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Form mainForm = this.FindForm();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (mainForm != null)
            {
                mainForm.ClientSize = new Size(646, 662);
                mainForm.StartPosition = FormStartPosition.Manual; // Enable manual positioning
                Screen currentScreen = Screen.FromControl(mainForm);
                Rectangle currentScreenBounds = currentScreen.WorkingArea;
                mainForm.Location = new Point(
                    (currentScreenBounds.Width - mainForm.Width) / 2 + currentScreenBounds.X,
                    (currentScreenBounds.Height - mainForm.Height) / 2 + currentScreenBounds.Y
                );
            }
            pagehandler.LoadUserControl(new ToolHub());
        }

        private void firstLine_btn_Click(object sender, EventArgs e)
        {
            Point lastClickedPosition = Point.Empty;
            Control thisLabel = fln_Label;

            var tracker = new MouseTracker(this.FindForm());

            Thread trackingThread = new Thread(() =>
                tracker.TrackMouse(coords =>
                {
                    lastClickedPosition = coords;

#if DEBUG
                    Debug.WriteLine($"Coordinates saved for First Line: {lastClickedPosition}");
#else
                    Console.WriteLine($"Coordinates saved for First Line: {lastClickedPosition}")
#endif

                    if (thisLabel.InvokeRequired)
                    {
                        thisLabel.Invoke(new Action(() =>
                        {
                            thisLabel.Text = $"X: {lastClickedPosition.X} ; Y: {lastClickedPosition.Y}";
                        }));
                    }
                    else
                    {
                        thisLabel.Text = $"X: {lastClickedPosition.X} ; Y: {lastClickedPosition.Y}";
                    }


                })
            );
            trackingThread.IsBackground = true;
            trackingThread.Start();

        }

        private void itemSearch_btn_Click(object sender, EventArgs e)
        {
            Point lastClickedPosition = Point.Empty;
            Control thisLabel = itsearch_Label;

            var tracker = new MouseTracker(this.FindForm());

            Thread trackingThread = new Thread(() =>
                tracker.TrackMouse(coords =>
                {
                    lastClickedPosition = coords;

#if DEBUG
                    Debug.WriteLine($"Coordinates saved for Item Search: {lastClickedPosition}");
#else
                    Console.WriteLine($"Coordinates saved for Item Search: {lastClickedPosition}")
#endif

                    if (thisLabel.InvokeRequired)
                    {
                        thisLabel.Invoke(new Action(() =>
                        {
                            thisLabel.Text = $"X: {lastClickedPosition.X} ; Y: {lastClickedPosition.Y}";
                        }));
                    }
                    else
                    {
                        thisLabel.Text = $"X: {lastClickedPosition.X} ;  Y: {lastClickedPosition.Y}";
                    }


                })
            );
            trackingThread.IsBackground = true;
            trackingThread.Start();
        }

        private void itemMaint_btn_Click(object sender, EventArgs e)
        {
            Point lastClickedPosition = Point.Empty;
            Control thisLabel = itmtn_Label;

            var tracker = new MouseTracker(this.FindForm());

            Thread trackingThread = new Thread(() =>
                tracker.TrackMouse(coords =>
                {
                    lastClickedPosition = coords;

#if DEBUG
                    Debug.WriteLine($"Coordinates saved for Item Maintenance: {lastClickedPosition}");
#else
                    Console.WriteLine($"Coordinates saved for Item Maintenance: {lastClickedPosition}")
#endif

                    if (thisLabel.InvokeRequired)
                    {
                        thisLabel.Invoke(new Action(() =>
                        {
                            thisLabel.Text = $"X: {lastClickedPosition.X} ; Y: {lastClickedPosition.Y}";
                        }));
                    }
                    else
                    {
                        thisLabel.Text = $"X: {lastClickedPosition.X} ; Y: {lastClickedPosition.Y}";
                    }


                })
            );
            trackingThread.IsBackground = true;
            trackingThread.Start();
        }

        private void itemConfig_btn_Click(object sender, EventArgs e)
        {
            Point lastClickedPosition = Point.Empty;
            Control thisLabel = itcfg_Label;

            var tracker = new MouseTracker(this.FindForm());

            Thread trackingThread = new Thread(() =>
                tracker.TrackMouse(coords =>
                {
                    lastClickedPosition = coords;

#if DEBUG
                    Debug.WriteLine($"Coordinates saved for Item Config: {lastClickedPosition}");
#else
                    Console.WriteLine($"Coordinates saved for Item Config: {lastClickedPosition}")
#endif

                    if (thisLabel.InvokeRequired)
                    {
                        thisLabel.Invoke(new Action(() =>
                        {
                            thisLabel.Text = $"X: {lastClickedPosition.X} ; Y: {lastClickedPosition.Y}";
                        }));
                    }
                    else
                    {
                        thisLabel.Text = $"X: {lastClickedPosition.X} ; Y: {lastClickedPosition.Y}";
                    }


                })
            );
            trackingThread.IsBackground = true;
            trackingThread.Start();
        }

        private void receiveSaveCfg_btn_Click(object sender, EventArgs e)
        {
            List<int> itmtnCoordinates = new List<int>();
            List<int> itcfgCoordinates = new List<int>();
            List<int> itsearchCoordinates = new List<int>();
            List<int> flnCoordinates = new List<int>();
            string[] splitLabel = itmtn_Label.Text.Split(';');
            itmtnCoordinates.Add(((int.Parse(string.Concat(splitLabel[0].Where(Char.IsDigit))))));
            itmtnCoordinates.Add(((int.Parse(string.Concat(splitLabel[1].Where(Char.IsDigit))))));
            splitLabel = itcfg_Label.Text.Split(";");
            itcfgCoordinates.Add(((int.Parse(string.Concat(splitLabel[0].Where(Char.IsDigit))))));
            itcfgCoordinates.Add(((int.Parse(string.Concat(splitLabel[1].Where(Char.IsDigit))))));
            splitLabel = itsearch_Label.Text.Split(";");
            itsearchCoordinates.Add(((int.Parse(string.Concat(splitLabel[0].Where(Char.IsDigit))))));
            itsearchCoordinates.Add(((int.Parse(string.Concat(splitLabel[1].Where(Char.IsDigit))))));
            splitLabel = fln_Label.Text.Split(";");
            flnCoordinates.Add(((int.Parse(string.Concat(splitLabel[0].Where(Char.IsDigit))))));
            flnCoordinates.Add(((int.Parse(string.Concat(splitLabel[1].Where(Char.IsDigit))))));

            List<MousePosition> mousePositions = new List<MousePosition> {
                new MousePosition(ControlType.ItemMaintenanceBox, itmtnCoordinates),
                new MousePosition(ControlType.ItemSearchInputBox, itsearchCoordinates),
                new MousePosition(ControlType.ItemConfigurationBox, itcfgCoordinates),
                new MousePosition(ControlType.ReceiptLineFirstLine, flnCoordinates)
            };
            ConfigData newCfg = new ConfigData();
            if (ConfigData._receivingConfig == null)
            {
                ReceivingConfig receiveConfig = new ReceivingConfig("nullPath", mousePositions);
                newCfg = new ConfigData(ConfigData._userConfig, receiveConfig);
                newCfg.PrepareForSerialization();
                Debug.WriteLine("creating new config");               
            }
            else
            {
                ReceivingConfig thisInstance = ConfigData._receivingConfig;
                thisInstance.setMousePositions(mousePositions);
                thisInstance.setExcelSheetNames(thisInstance.ExcelFilePath);
            }
            ConfigData.SerializeConfigData();

            /*itmtnCoordinates.Add(int.Parse(itmtn_Label.Text.Split(";"));
            
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
            
        } */
/*
        }

        private void UpdateLabels()
        {
            if (ConfigData._receivingConfig != null)
            {
                ReceivingConfig configInstance = ConfigData._receivingConfig;
                List<MousePosition> mousePositions = configInstance.getMousePositions();

                List<int> flnCoordinates = new List<int> { 0, 0 };
                List<int> itmtnCoordinates = new List<int> { 0, 0 };
                List<int> itcfgCoordinates = new List<int> { 0, 0 };
                List<int> itsearchCoordinates = new List<int> { 0, 0 };

                foreach (MousePosition mousePosition in mousePositions)
                {
                    if (mousePosition != null)
                    {
                        if (mousePosition.ControlType == ControlType.ReceiptLineFirstLine) { flnCoordinates = mousePosition.getPositionByType(ControlType.ReceiptLineFirstLine); }
                        if (mousePosition.ControlType == ControlType.ItemMaintenanceBox) { itmtnCoordinates = mousePosition.getPositionByType(ControlType.ItemMaintenanceBox); }
                        if (mousePosition.ControlType == ControlType.ItemConfigurationBox) { itcfgCoordinates = mousePosition.getPositionByType(ControlType.ItemConfigurationBox); }
                        if (mousePosition.ControlType == ControlType.ItemSearchInputBox) { itsearchCoordinates = mousePosition.getPositionByType(ControlType.ItemSearchInputBox); }

                    }
                }

                if (flnCoordinates != null || flnCoordinates.Count > 0)
                {
                    fln_Label.Text = ($"X: {flnCoordinates[0]} ; Y: {flnCoordinates[1]}");
                }
                if (itmtnCoordinates != null || itmtnCoordinates.Count > 0)
                {
                    itmtn_Label.Text = ($"X: {itmtnCoordinates[0]} ; Y: {itmtnCoordinates[1]}");
                }
                if (itcfgCoordinates != null || itcfgCoordinates.Count > 0)
                {
                    itcfg_Label.Text = ($"X: {itcfgCoordinates[0]} ; Y: {itcfgCoordinates[1]}");
                }
                if (itsearchCoordinates != null || itsearchCoordinates.Count > 0)
                {
                    itsearch_Label.Text = ($"X: {itsearchCoordinates[0]} ; Y: {itsearchCoordinates[1]}");
                }

            }
        }

        private void cfgexcel_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ConfigData._receivingConfig != null)
            {
                ReceivingConfig configInstance = ConfigData._receivingConfig;
                string excelDirectory = configInstance.getExcelFilePath();
                Process.Start("explorer.exe", excelDirectory);
            }
        }

        private void excelPath_RCV_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel files (*.xlsx,*.xls)|*.xlsx;*.xls|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK) { filePath = openFileDialog.FileName; }
                if (ConfigData._receivingConfig != null)
                {
                    ReceivingConfig configInstance = ConfigData._receivingConfig;
                    if (configInstance.getExcelFilePath() != filePath)
                    {
                        configInstance.setExcelFilePath(filePath);
                    }
                }
            }

        }

        private void nextPageRCV_btn_Click(object sender, EventArgs e)
        {
           
           
        }
    }
}
*/
