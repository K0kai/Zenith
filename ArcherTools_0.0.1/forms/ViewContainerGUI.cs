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
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace ArcherTools_0._0._1.forms
{
    public partial class ViewContainerGUI : Form
    {
        private classes.Container _container;
        private int _releaseAsNum;
        private string _releaseAsString;
        
        public ViewContainerGUI(classes.Container container)
        {
            InitializeComponent();
            _container = container;
            _releaseAsNum = classes.Container.SelectedRelease;
            this.Load += ViewContainerGUI_Load;
            this.Text = $"{container.ContainerId}-{classes.Container.ReleaseToString(classes.Container.SelectedRelease)}-{container.Owner}";

        }

        private void ViewContainerGUI_Load(object? sender, EventArgs e)
        {
            switch (_releaseAsNum)
            {
                case 100:
                    _releaseAsString = "ND";
                    break;
                case 101:
                    _releaseAsString = "TEF";
                    break;
                case 102:
                    _releaseAsString = "BYD";
                    break;
                case 103:
                    _releaseAsString = "IMA";
                    break;
                default:
                    _releaseAsString = _releaseAsNum.ToString();
                    break;
            }

            this.title_Label.Text = title_Label.Text.Split(':')[0] + $":\n{_container.ToString()}-{_releaseAsString}";
            lineCfg_DtGridView.AllowUserToAddRows = false;
            var lineList = GetLines();
            var configList = GetConfigurations();
            var lineColumn = new DataColumn("Lines");
            lineColumn.ColumnName = "Lines";
            var configColumn = new DataColumn("Configs");
            configColumn.ColumnName = "Configs";
            var dataTable = new DataTable("Lines_Configs_tbl");
            dataTable.Columns.Add(lineColumn);
            dataTable.Columns.Add(configColumn);
            Debug.WriteLine(configList.Count);
            for (int i = 0; i < lineList.Count; i++)
            {
                int line = i < lineList.Count ? lineList[i] : -999;
                string config = i < configList.Count ? configList[i] : string.Empty;

                dataTable.Rows.Add(line, config);
            }

            BindingSource lineCfgBs = new BindingSource();
            lineCfgBs.DataSource = dataTable;
            lineCfg_DtGridView.DataSource = lineCfgBs;
        }

        private static List<int>? GetLines()
        {
            if (classes.Container.ValidateSelectedContainerAndRelease() == 0)
            {
                List<int> Lines = classes.Container.SelectedContainer.AttachedConfigurations[classes.Container.SelectedRelease].Keys.ToList();
                return Lines;
            }
            else
            {
                return null;
            }
        }

        private static List<string>? GetConfigurations()
        {
            if (classes.Container.ValidateSelectedContainerAndRelease() == 0)
            {
                List<string> Configs = classes.Container.SelectedContainer.AttachedConfigurations[classes.Container.SelectedRelease].Values.ToList();
                return Configs;
            }
            else
            {
                return null;
            }
        }
    }
}
