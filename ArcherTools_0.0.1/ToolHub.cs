using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.controllers;
using ArcherTools_0._0._1.enums;
using ArcherTools_0._0._1.forms;
using ArcherTools_0._0._1.methods;
using System.Diagnostics;

namespace ArcherTools_0._0._1
{
    public partial class ToolHub : UserControl
    {
        private MessageManager messageManager;
        private PageHandler _pageHandler = PageHandler.GetInstance();
        public static Form _mainForm;
        public static Size ucSize;

        private void CenterControl(Control control)
        {
            int centerX = (this.ClientSize.Width - control.Width) / 2;
            int centerY = control.Location.Y;
            //int centerY = (this.ClientSize.Height - introlabel.Height) / 2;

            control.Location = new Point(centerX, centerY);
        }
        public ToolHub()
        {
            InitializeComponent();
            messageManager = new MessageManager();
            introlabel.Text = messageManager.RandomizeMessage();
            introlabel.Click += introlabel_Click;
            copyrights.LinkClicked += copyrights_LinkClicked_1;
            this.Load += userControlLoad;
            
            CenterControl(introlabel);


        }

        private void userControlLoad(object sender, EventArgs e)
        {
            _mainForm = this.FindForm();
            ucSize = _mainForm.Size;
            try
            {
                ConfigData? config = ConfigData.DeserializeConfigData();
                config?.PostDeserialization();

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to deserialize ConfigData: {ex.ToString()}");
            }
        }

        private void copyrights_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open the link " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void VisitLink()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "https://github.com/K0kai";
            psi.UseShellExecute = true;
            Process.Start(psi);
            copyrights.LinkVisited = true;

        }

        /*
         private void MouseEnterBtn(object sender, EventArgs e)
         {
             if (sender is Button button)
             {
                 button.ForeColor = Color.Black;
             }
         }
        */

        /*
         private void MouseLeaveBtn(object sender, EventArgs e)
         {
             if (sender is Button button)
             {
                 button.ForeColor = Color.White;
             }
         }
        */

        private void introlabel_Click(object sender, EventArgs e)
        {
            if (introlabel != null)
            {
                introlabel.Text = messageManager.RandomizeMessage();
                CenterControl((Control)sender);
            }
        }

        private void receiveBtn_Click_1(object sender, EventArgs e)
        {
            ReceivingGUI rcvGUI = new ReceivingGUI("Receiving Main GUI", "Available Options:");
            this.FindForm().Visible = false;
            rcvGUI.ShowDialog(this);
            this.FindForm().Visible = true;

        }

        private void vpnConnect_btn_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            VPNConnect.ConnectToVPN();
            this.Enabled = true;
        }

        private void settings_Btn_Click(object sender, EventArgs e)
        {
            this.FindForm().Size = new Size(396, 444);
            _pageHandler.LoadUserControl(new SettingsUserControl());
        }
    }
}
            
        
    

