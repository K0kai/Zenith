using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ArcherTools_0._0._1.excel;
using NPOI.OpenXmlFormats.Dml.Chart;

namespace ArcherTools_0._0._1.forms
{
    public partial class PreReceivingGUI : Form
    {
        private string _title;
        private string _desc;
        public static PreReceivingGUI _instance;
        private Point mouseDownLocation;
        internal static string imaFilePath;
        public PreReceivingGUI(string title, string desc)
        {

            InitializeComponent();
            _title = title;
            _desc = desc;
            this.Load += onLoad;
        }

        private void close_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void onLoad(object sender, EventArgs e)
        {
            title_Label.Text = _title;
            description_Label.Text = _desc;
            imaFile_lbl.Text = imaFile_lbl.Text + " Not loaded";
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.GetType() != typeof(Button) && (ctrl != imaFile_lbl))
                {
                    ctrl.MouseDown += new MouseEventHandler(PreReceivingGUI_MouseDown);
                    ctrl.MouseMove += new MouseEventHandler(PreReceivingGUI_MouseMove);
                }
                if (ctrl.GetType() == typeof(Panel))
                {
                    foreach (Control ctrlInCtrl in ctrl.Controls)
                    {
                        if (ctrlInCtrl.GetType() != typeof(Button))
                        {
                            ctrlInCtrl.MouseDown += new MouseEventHandler(PreReceivingGUI_MouseDown);
                            ctrlInCtrl.MouseMove += new MouseEventHandler(PreReceivingGUI_MouseMove);
                        }
                    }

                }
            }
        }

        private void PreReceivingGUI_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Capture the mouse position when the left button is pressed
                mouseDownLocation = e.Location;
            }
        }

        private void PreReceivingGUI_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Adjust the form's position based on the mouse movement
                var deltaX = e.X - mouseDownLocation.X;
                var deltaY = e.Y - mouseDownLocation.Y;

                this.Location = new Point(
                    this.Location.X + deltaX,
                    this.Location.Y + deltaY
                );
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Sheet (*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.Multiselect = false;
            string filePath;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                string status = imaFile_lbl.Text.Split(':')[0] + $": {Path.GetFileName(filePath)}";
                imaFilePath = filePath;
                imaFile_lbl.Text = status;
            }
        }

        internal string CheckForIMAPL(string FilePath)
        {
            if (!string.IsNullOrEmpty(FilePath))
            {
                ExcelHandler exHandler = new ExcelHandler(FilePath);
                var worksheets = exHandler.GetWorksheets();
                if (worksheets != null)
                {
                    if (worksheets.Count > 0)
                    {
                        foreach (var worksheet in worksheets)
                        {
                            if (methods.strings.StringMatching.CosineSimilarity(worksheet.Name.ToLower(), "detail pl") >= 70)
                            {
                                Debug.WriteLine($"there is detail pl or similar.\nsimilarity: {methods.strings.StringMatching.CosineSimilarity(worksheet.Name.ToLower(), "detail pl")}");
                                return worksheet.Name;
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }
        private void imaprercv_Btn_Click(object sender, EventArgs e)
        {
            if (imaFilePath != null)
            {
                ExcelHandler exHandler = new ExcelHandler(imaFilePath);
                var worksheetName = CheckForIMAPL(imaFilePath);
                if (!string.IsNullOrEmpty(worksheetName))
                {
                    ConcurrentBag<string> listToLook = ["Style", "P.O. No", "Description", "Color", "Quantity", "Total PC"];
                    ConcurrentDictionary<string, int> listToLookV2 = new ConcurrentDictionary<string, int>
                    {
                        ["Style"] = 50,
                        ["P.O. No"] = 40,
                        ["Description"] = 40,
                        ["Color"] = 40,
                        ["Quantity"] = 40,
                        ["Total PC"] = 95
                    };
                    var help = exHandler.SearchWorksheetFor(worksheetName, listToLookV2 );
                    var rowList = new List<int>();
                    foreach (var helpless in help)
                    {
                        Debug.WriteLine(helpless.Key +" "+ helpless.Value["row"].ToString());
                        rowList.Add(helpless.Value["row"]);
                    }
                    int minRow = rowList.Count == 0 ? 1 : rowList.Min();
                    Debug.WriteLine(minRow);
                    PreReceivingGUI_prestart preReceivingGUI_Prestart = new PreReceivingGUI_prestart(help);
                    preReceivingGUI_Prestart.ShowDialog(this);
                }
            }
        }
    }
}

