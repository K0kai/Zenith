using System.Diagnostics;
<<<<<<< Updated upstream
using System.Runtime.InteropServices;
=======
using ArcherTools_0._0._1.boxes;
>>>>>>> Stashed changes
using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.cfg.oldcfg;
using ArcherTools_0._0._1.controllers;
using ArcherTools_0._0._1.excel;


namespace ArcherTools_0._0._1
{
    public partial class ToolHub : UserControl
    {
        private MessageManager messageManager;
        private PageHandler _pageHandler = PageHandler.GetInstance();

        private void CenterControl(Control control)
        {
            int centerX = (this.ClientSize.Width - introlabel.Width) / 2;
            int centerY = control.Location.Y;
            //int centerY = (this.ClientSize.Height - introlabel.Height) / 2;

            control.Location = new System.Drawing.Point(centerX, centerY);
        }
        public ToolHub()
        {
            InitializeComponent();
            messageManager = new MessageManager();
            introlabel.Text = messageManager.RandomizeMessage();
            introlabel.Click += introlabel_Click;
            copyrights.LinkClicked += copyrights_LinkClicked_1;
            CenterControl(introlabel);
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

        private void receiveconfig_btn_Click(object sender, EventArgs e)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Form mainForm = this.FindForm();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (mainForm != null)
            {
                mainForm.ClientSize = new Size(480, 473);
                mainForm.StartPosition = FormStartPosition.Manual; // Enable manual positioning
                Screen currentScreen = Screen.FromControl(mainForm);
                Rectangle currentScreenBounds = currentScreen.WorkingArea;
                mainForm.Location = new Point(
                    (currentScreenBounds.Width - mainForm.Width) / 2 + currentScreenBounds.X,
                    (currentScreenBounds.Height - mainForm.Height) / 2 + currentScreenBounds.Y
                );
            }
            //_pageHandler.LoadUserControl(new ReceiveProperties());

        }

        private void receiveBtn_Click_1(object sender, EventArgs e)
        {
<<<<<<< Updated upstream
            //ScreenImageHandler.DetectImage("C:\\Users\\Archer\\source\\repos\\ArcherTools_0.0.1\\ArcherTools_0.0.1\\img\\find\\item_search.png");

            cfg.oldcfg.ReceivingConfig config = OldConfigData.getInstance();
            String excelPath = config.ExcelFilePath;
            ExcelHandler excelHandler = new ExcelHandler(excelPath);
            MessageBox.Show(excelHandler.GetCell("TEST CHECK", 13, 4));
            Debug.WriteLine(excelHandler.GetColumn("TEST CHECK", 4)[2]);
            Debug.WriteLine(excelHandler.GetRow("TEST CHECK", 13)[2]);
        }

        private async void vpnConnect_btn_Click(object sender, EventArgs e)
        {
            // Sys Methods
            [DllImport("user32.dll")]
            static extern bool SetForegroundWindow(IntPtr hWnd);

            
            // Sys Methods

 

            String vpnPath = "C:\\Program Files\\SonicWall\\Global VPN Client";
            String vpnShortcutName = "SWGVC.exe";
            String vpnShortcutPath = Path.Combine(vpnPath, vpnShortcutName);
            try
            {
                Process[] processes = Process.GetProcessesByName(vpnShortcutName);
                if (processes.Length <= 0)
                {
                    Process.Start(vpnShortcutPath);
                    Thread.Sleep(500);
                    processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(vpnShortcutName));

                }
                IntPtr hWnd = new IntPtr(IntPtr.Zero);
                while (hWnd == IntPtr.Zero && !processes[0].HasExited)
                {
                    Thread.Sleep(100);
                    hWnd = processes[0].MainWindowHandle;
                }
                Thread.Sleep(500);
                SetForegroundWindow(hWnd);
                Rectangle rect = WindowHandler.GetWindowRectFromHandle(hWnd);
                Debug.WriteLine(rect.Y);
                Thread.Sleep(1000);
                Point findIp = ScreenImageHandler.SearchImageOnRegion("C:\\Users\\Archer\\source\\repos\\ArcherTools_0.0.1\\ArcherTools_0.0.1\\img\\find\\vpn_ip.png", rect, 0.95);
                if (findIp != new Point(0,0))
                {
                    MouseHandler.SetCursorPos(findIp.X + 20, findIp.Y + 5);
                    MouseHandler.MouseClick();
                    MouseHandler.MouseClick();
                }
                else
                {
                    findIp = ScreenImageHandler.SearchImageOnRegion("C:\\Users\\Archer\\source\\repos\\ArcherTools_0.0.1\\ArcherTools_0.0.1\\img\\find\\vpn_ipActivated.png", rect, 0.95);
                    MouseHandler.SetCursorPos(findIp.X + 20, findIp.Y + 5);
                    MouseHandler.MouseClick();
                    MouseHandler.MouseClick();
                }
                



            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally { 
                            
            }
            
=======
            ScreenImageHandler.SearchImageOnScreen("C:\\Users\\Archer\\source\\repos\\ArcherTools_0.0.1\\ArcherTools_0.0.1\\img\\find\\item_search.png", 0.95);

            ReceivingConfig? config = ConfigData._receivingConfig;
            if (config != null)
            {
                String excelPath = config.ExcelFilePath;
                ExcelHandler excelHandler = new ExcelHandler(excelPath);
                MessageBox.Show(excelHandler.GetCell("TEST CHECK", 13, 4));
            }
            else
            {
                Debug.WriteLine("Receiving config is empty.");
            }

        }

        private void vpnConnect_btn_Click(object sender, EventArgs e)
        {
            if (ConfigData._userConfig == null)
            {
                MessageBox.Show("Seems like you haven't set up your VPN Configurations yet, so we'll begin with that");
                Form mainForm = this.FindForm();
                mainForm.Enabled = false;
                List<string> boxNames = new List<string>
                {
                    "VPN Username",
                    "VPN Password"
                };
                var UserInputs = DynamicInputBoxForm.Show("Enter these values:", boxNames);
                mainForm.Enabled = true;
            }
>>>>>>> Stashed changes
        }
    }
}
