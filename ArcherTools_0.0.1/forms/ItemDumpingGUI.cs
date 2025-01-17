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
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.excel;
using ArcherTools_0._0._1.methods;

namespace ArcherTools_0._0._1.forms
{
    public partial class ItemDumpingGUI : Form
    {
        private string _title;
        private string _description;

        public ItemDumpingGUI(string title, string desc)
        {
            InitializeComponent();
            this.BackColor = ToolHub._mainForm.BackColor;
            title_Label.TextChanged += labelTextChanged;
            description_Label.TextChanged -= labelTextChanged;
            _title = title;
            _description = desc;
            this.Load += onLoad;
            this.itemValues_Box.TextChanged += textBoxChanged;
            centerLabels();


        }

        private void onLoad(object sender, EventArgs e)
        {
            title_Label.Text = _title;
            description_Label.Text = _description;
        }

        private void labelTextChanged(object sender, EventArgs e)
        {
            centerLabels();
        }
        private void centerLabels()
        {
            title_Label.Location = new Point(this.Width / 2, title_Label.Location.Y);
            description_Label.Location = new Point(this.Width / 2, description_Label.Location.Y);

        }

        private void close_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void textBoxChanged(object sender, EventArgs e)
        {
            int count = 0;            
            TextBox textBox = sender as TextBox;
            string[] lines = textBox.Lines;
            List<String> cleanLines = lines.Where(s => !string.IsNullOrEmpty(s)).ToList();
            foreach (string line in cleanLines)
            {
              count++;              
            }
        this.lines_label.Text = $"Lines: {count}";          
            
        }

        private List<String> itemSeparation()
        {
            string[] lines = this.itemValues_Box.Lines;
            List<String> cleanLines = lines.Where(s => !string.IsNullOrEmpty(s)).ToList();
            return cleanLines;
        }    

        private void sendItems_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("begin");
                List<String> values = itemSeparation();
                var configDataValidation = Receiving.validateConfigData();
                var excelDataValidation = Receiving.validateExcel();
                if (configDataValidation && excelDataValidation)
                {
                    Debug.WriteLine("check success");
                    ReceivingConfig rcvCfg = ConfigData._receivingConfig;
                    ExcelHandler excelHandler = new ExcelHandler(rcvCfg.ExcelFilePath);
                    if (rcvCfg.ExcelSheetNames.Contains("DUMP"))
                    {
                        var workSheetName = "DUMP";
                        excelHandler.SetColumn(workSheetName, 4, values, 2);
                        Debug.WriteLine("done");
                    }
                }
                else { throw new Exception($"Failed at checks: {nameof(configDataValidation)}: {configDataValidation}, {nameof(excelDataValidation)}: {excelDataValidation} "); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something went wrong at item translocation\n{ex.StackTrace}\n{ex.Message}");
            }
        }
    }
}
