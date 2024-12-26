using System.Diagnostics;

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

        private void receiveBtn_Click(object sender, EventArgs e)
        {
            Receiving.MainCall(["uau"]);
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
            _pageHandler.LoadUserControl(new ReceiveProperties());
            
        }
    }
}
