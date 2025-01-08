using ArcherTools_0._0._1.boxes;
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
            Control thisLabel = fln_Label;
            setAndTrackPosition(thisLabel);

        }

        private void itemSearch_btn_Click(object sender, EventArgs e)
        {            
            Control thisLabel = itsearch_Label;
            setAndTrackPosition(thisLabel);
        }

        private void itemMaint_btn_Click(object sender, EventArgs e)
        {
            Control thisLabel = itmtn_Label;
            setAndTrackPosition(thisLabel);
        }

        private void itemConfig_btn_Click(object sender, EventArgs e)
        {            
            Control thisLabel = itcfg_Label;
            setAndTrackPosition(thisLabel);
        }

        private void receiveSaveCfg_btn_Click(object sender, EventArgs e)
        {
            
        }

        private void UpdateLabels()
        {
            
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

        private void itsSearchInq_btn_Click(object sender, EventArgs e)
        {            
            Control thisLabel = itSearchInq_Label;
            setAndTrackPosition(thisLabel);
        }

        private void setAndTrackPosition(Control thisLabel)
        {
            Point lastClickedPosition = Point.Empty;

            var tracker = new MouseTracker(this.FindForm());

            Thread trackingThread = new Thread(() =>
                tracker.TrackMouse(coords =>
                {
                    lastClickedPosition = coords;

                    Debug.WriteLine($"Coordinates saved for {thisLabel.Name}: {lastClickedPosition}");

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

        private void editProperties_btn_Click(object sender, EventArgs e)
        {
            Rectangle itSearchRect = new Rectangle();
            Rectangle itSearchInqRect = new Rectangle();

            Task ItemSearchRect = Task.Run(() =>
            {
                itSearchRect = OverlayForm.Show(new Rectangle(0, 0, 300, 300), "Item Search Rect");
                
            });
            Task ItemSearchInqRect = Task.Run(() =>
            {
                itSearchInqRect = OverlayForm.Show(new Rectangle(0, 0, 300, 300), "Item Search Inq Rect");
            });
            Task.WaitAll(ItemSearchInqRect, ItemSearchRect);
            Debug.WriteLine(itSearchInqRect.X);
            Debug.WriteLine(itSearchRect.X);


        }
    }
}
