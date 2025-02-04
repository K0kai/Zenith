using System.Diagnostics;
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.controllers;
using ArcherTools_0._0._1.forms;
using ArcherTools_0._0._1.methods;

namespace ArcherTools_0._0._1
{
    public partial class ToolHub : UserControl
    {
        private MessageManager messageManager;
        public static string programVersion = "0.1.0";
        private PageHandler _pageHandler = PageHandler.GetInstance();
        internal ToolHub instance;
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
            copyrights.LinkClicked += copyrights_LinkClicked_1;
            this.Load += userControlLoad;
            this.Invalidated += OnInvalidated;
            this.instance = this;



        }

        private void userControlLoad(object sender, EventArgs e)
        {
            _mainForm = this.FindForm();
            _mainForm.FormBorderStyle = FormBorderStyle.Sizable;
            _mainForm.FormClosing += OnFormClosed;
            this.pagelabel.Text = $"Zenith v{programVersion}";
            if (Properties.Settings.Default.ToolHubLocation != new Point(0, 0))
            {
                _mainForm.Location = Properties.Settings.Default.ToolHubLocation;
            }
            if (Properties.Settings.Default.ToolHubSize != new Size(0, 0))
            {
                _mainForm.Size = Properties.Settings.Default.ToolHubSize;
            }
            
            ucSize = _mainForm.Size;
            this.Cursor = Cursors.Default;
            connectToVpn_Btn.SetToolTip(vpnConnect_btn, "Obsolete function as of December 2024.");
            preRcv_tooltip.SetToolTip(prercv_Btn, "Session under construction and currently unusable.");
            try
            {
                if (!File.Exists(ConfigData.filePath))
                {
                    ConfigData.createNewCfgFile();
                }
                ConfigData? config = ConfigData.DeserializeConfigData();
                config?.PostDeserialization();


                introlabel.Text = messageManager.RandomizeMessage();
                introlabel.Click += introlabel_Click;
                CenterControl(introlabel);
                CenterControl(pagelabel);
                ColorConfig.GenerateDefaults();
                if (!string.IsNullOrEmpty(Properties.Settings.Default.SelectedPreset))
                {
                    ColorPresets._instance.SetPreset(Properties.Settings.Default.SelectedPreset);
                }
                try
                {
                    classes.Container.DeserializeAllContainers();
                }
                catch (Exception ex) {
                    Debug.WriteLine(ex.StackTrace);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to deserialize ConfigData: {ex.ToString()}");
            }
        }

        private void OnFormClosed(object sender, EventArgs e)
        {
            Properties.Settings.Default.ToolHubSize = _mainForm.Size;
            Properties.Settings.Default.ToolHubLocation = _mainForm.Location;            
            Properties.Settings.Default.Save();
        }
        private void OnInvalidated(object sender, EventArgs e)
        {
            if (instance != null) { return; }
            introlabel.Text = messageManager.RandomizeMessage();
            CenterControl(introlabel);
            copyrights.LinkVisited = false;
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
            if (ReceivingGUI._instance == null)
            {
                ReceivingGUI rcvGUI = new ReceivingGUI("Receiving Main GUI", "Available Options:");
                rcvGUI.Show();
            }
            else
            {
                ReceivingGUI._instance.Focus();
            }
            


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

        private void testbutton_Click(object sender, EventArgs e)
        {
            Point mouseto = new Point();
            Task findDefault = Task.Run(() =>
            {
                mouseto = ScreenImageHandler.SearchImageOnScreen("C:\\Users\\Archer\\source\\repos\\ArcherTools_0.0.1\\ArcherTools_0.0.1\\img\\find\\defaultcfg.png", 0.99);
            });
            Task.WaitAll(findDefault);

            MouseHandler.MouseMoveTo(mouseto);
        }

        private void prercv_Btn_Click(object sender, EventArgs e)
        {
            PreReceivingGUI rcvGUI = new PreReceivingGUI("Pre-Receiving Main GUI", "Available Options:");
            this.FindForm().Visible = false;
            rcvGUI.ShowDialog(this);
            this.FindForm().Visible = true;
        }
    }
}
            
        
    

