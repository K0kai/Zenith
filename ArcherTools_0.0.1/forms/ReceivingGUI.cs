using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArcherTools_0._0._1.boxes;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.excel;
using ArcherTools_0._0._1.methods;

namespace ArcherTools_0._0._1.forms
{
    public partial class ReceivingGUI : Form
    {
        private string _title;
        private string _desc;
        private string statusDefaultText;
        public static ReceivingGUI _instance;
        public ReceivingGUI(string title, string desc)
        {
            InitializeComponent();
            _title = title;
            _desc = desc;
            statusDefaultText = status_Label.Text;
            _instance = this;
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
        }

        private void startscript_Btn_Click(object sender, EventArgs e)
        {
            bool cfgdata = Receiving.validateConfigData();
            bool excel = Receiving.validateExcel();
            bool rects = Receiving.validateRectanglePositions();
            if (cfgdata && excel && rects)
            {
                List<String> dibf =  DynamicInputBoxForm.Show("Please enter the container code, release and owner", new List<string> { "Container", "Release", "Owner" });
                if (dibf.Count > 0)
                {
                    foreach (var streng in dibf)
                    {
                        Debug.WriteLine(streng);
                    }
                }
                Receiving.MainCall();
            }
            else
            {
                updateStatusLabel("Status: Failed at data validation,\nTry reconfiguring the program and trying again.");
                Debug.WriteLine("Something's not right with your receiving config.");
            }
        }

        private void itemToExcel_Btn_Click(object sender, EventArgs e)
        {
            ItemDumpingGUI itDmpGui = new ItemDumpingGUI("Item Dumping GUI", "Follow the placeholder formatting pcs/case_depth/case_width/case_height/case_weight");
            itDmpGui.Show(this);
        }

        public void updateStatusLabel(string text, int delay = 0)
        {
            if (delay > 0)
            {
                Thread.Sleep(delay);
            }
            status_Label.Text = text;
        }

        private async void cleanExcel_Btn_Click(object sender, EventArgs e)
        {
            updateStatusLabel("Status: Starting cleaning process");
            var configDataValidation = Receiving.validateConfigData();
            var excelDataValidation = Receiving.validateExcel();
            try
            {
                if (configDataValidation && excelDataValidation)
                {
                    ReceivingConfig rcvConfig = ConfigData._receivingConfig;
                    ExcelHandler exHandler = new ExcelHandler(rcvConfig.ExcelFilePath);
                    var list = exHandler.GetColumn("DUMP", 4, 2);
                    updateStatusLabel("Status: Cleaning items 1/2");
                    Task cleanItems = Task.Run(() => { exHandler.SetColumn("DUMP", 4, list, 2, true); });
                    Task.WaitAll(cleanItems);
                    updateStatusLabel("Status: Cleaning items 2/2");
                    Task cleanLines = Task.Run(() => { exHandler.SetColumn("DUMP", 3, list, 2, true); });
                    Task.WaitAll(cleanLines);
                    updateStatusLabel($"Status: Cleaned {list.Count} items successfully.");


                }
                else { updateStatusLabel("Status: Failure at data validation."); throw new DataException($"Failed at validations:\n{nameof(configDataValidation)}: {configDataValidation}, {nameof(excelDataValidation)}: {excelDataValidation} "); }
            } catch (Exception ex)
            {

            }
        }
    }
}
