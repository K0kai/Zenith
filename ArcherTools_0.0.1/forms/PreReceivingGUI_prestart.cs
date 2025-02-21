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
using ArcherTools_0._0._1.methods;
using ArcherTools_0._0._1.methods.strings;

namespace ArcherTools_0._0._1.forms
{
    public partial class PreReceivingGUI_prestart : Form
    {
        private Dictionary<string, Dictionary<string, int>> ColumnList { get; set; } = new Dictionary<string, Dictionary<string, int>>();
        public PreReceivingGUI_prestart()
        {
            InitializeComponent();
        }
        public PreReceivingGUI_prestart(Dictionary<string, Dictionary<string, int>> columnList)
        {
            InitializeComponent();
            ColumnList = columnList;
            Task.Run(() =>
            {
                InitializeAutoDetectedValues();
            });

        }

        private void AsyncControlTextUpdate(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                control.Invoke((MethodInvoker)delegate
                {
                    control.Text = text;
                });
            }
            else
            {
                control.Text = text;
            }
        }

        private async void InitializeAutoDetectedValues()
        {
            if (ColumnList.Count > 0)
            {
                foreach (var txtbox in this.textbox_panel.Controls.OfType<TextBox>())
                {
                    foreach (var column in ColumnList)
                    {
                        switch (column.Key)
                        {
                            case "Style":
                                AsyncControlTextUpdate(style_txtbox, column.Value["column"].ToString());
                                break;
                            case "P.O No":
                                AsyncControlTextUpdate(PO_txtBox, column.Value["column"].ToString());
                                break;
                            case "P.O. No":
                                AsyncControlTextUpdate(PO_txtBox, column.Value["column"].ToString());
                                break;
                            case "Color":
                                AsyncControlTextUpdate(color_txtbox, column.Value["column"].ToString());
                                break;
                            case "Quantity":
                                AsyncControlTextUpdate(quantity_txtbox, column.Value["column"].ToString());
                                break;
                            case "Total PC":
                                AsyncControlTextUpdate(totalpc_txtbox, column.Value["column"].ToString());
                                break;
                            case "Description":
                                AsyncControlTextUpdate(desc_txtbox, column.Value["column"].ToString());
                                break;
                            default:
                                AsyncControlTextUpdate(txtbox, string.Empty);
                                break;
                        }

                    }
                }
                List<int> columnValues = ColumnList.Values
    .Where(innerDict => innerDict.ContainsKey("row"))
    .Select(innerDict => innerDict["row"])
    .ToList();
                AsyncControlTextUpdate(row_txtbox, columnValues.Min(x => (x + 1).ToString()));
            }
        }

        private void submit_btn_Click(object sender, EventArgs e)
        {
            bool AllClear = true;
            ErrorProvider.Clear();
            foreach (var txtbox in textbox_panel.Controls.OfType<TextBox>())
            {
                if (string.IsNullOrEmpty(txtbox.Text))
                {
                    ErrorProvider.SetError(txtbox, "Required Field");
                    AllClear = false;
                }
                if (!int.TryParse(txtbox.Text, out int value))
                {
                    ErrorProvider.SetError(txtbox, "Only numbers allowed");
                    AllClear = false;
                }
            }
            if (AllClear)
            {
                ConcurrentDictionary<string, int> ValueColumnPairs = new ConcurrentDictionary<string, int>
                {
                    ["P.O"] = int.Parse(PO_txtBox.Text),
                    ["Style"] = int.Parse(style_txtbox.Text),
                    ["Description"] = int.Parse(desc_txtbox.Text),
                    ["Color"] = int.Parse(color_txtbox.Text),
                    ["Quantity"] = int.Parse(quantity_txtbox.Text),
                    ["Total PC"] = int.Parse(totalpc_txtbox.Text),
                    ["Size"] = int.Parse(size_txtbox.Text),
                    ["Item Code"] = int.Parse(itemcode_txtbox.Text),
                };
                ContainerCreation.MinimumRow = int.Parse(row_txtbox.Text);
                ContainerCreation.ExcelValueColumns = new ConcurrentDictionary<string, int>(ValueColumnPairs);
                ContainerCreation.FilePath = PreReceivingGUI.imaFilePath;
                this.Close();
            }
        }
    }
}
