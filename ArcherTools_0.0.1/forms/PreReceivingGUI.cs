using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
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
                imaFile_lbl.Text = status ;
            }
        }
    }
}
