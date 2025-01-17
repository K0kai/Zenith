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
using ArcherTools_0._0._1.methods;

namespace ArcherTools_0._0._1.forms
{
    public partial class ReceivingGUI : Form
    {
        private string _title;
        private string _desc;
        public ReceivingGUI(string title, string desc)
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
        }

        private void startscript_Btn_Click(object sender, EventArgs e)
        {
            bool cfgdata = Receiving.validateConfigData();
            bool excel = Receiving.validateExcel();
            bool rects = Receiving.validateRectanglePositions();
            if (cfgdata && excel && rects)
            {
                Receiving.MainCall();
            }
            else
            {
                Debug.WriteLine("Something's not right with your receiving config.");
            }
        }

        private void itemToExcel_Btn_Click(object sender, EventArgs e)
        {
            ItemDumpingGUI itDmpGui = new ItemDumpingGUI("Item Dumping GUI", "Follow the placeholder formatting pcs/case_depth/case_width/case_height/case_weight");
            itDmpGui.Show(this);
        }
    }
}
