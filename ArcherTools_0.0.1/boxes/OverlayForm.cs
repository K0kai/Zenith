using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static ArcherTools_0._0._1.WindowHandler;

namespace ArcherTools_0._0._1.boxes
{
    class OverlayForm : Form
    {
        private Point _mouseDownLocation;

        private static Rectangle _formProperties;

        public OverlayForm(Rectangle rect, string formName = "Rect")
        {
            // Set form properties
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = Color.Red;
            this.Opacity = 0.5; // Semi-transparent
            this.TopMost = true; // Always on top
            this.TopLevel = true;
            this.WindowState = FormWindowState.Maximized;
            
            this.Text = formName;
            this.ShowInTaskbar = false;
            

            // Set position and size
            this.Location = new Point(rect.Left, rect.Top);
            this.Size = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);

            // Enable dragging by capturing mouse events
            this.MouseDown += OverlayForm_MouseDown;
            this.MouseMove += OverlayForm_MouseMove;
            this.FormClosed += OverlayForm_OnClose;

            // Add custom drawing for border
            this.Paint += OverlayForm_Paint;
        }

        private void OverlayForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownLocation = e.Location;
            }
        }

        private void OverlayForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - _mouseDownLocation.X,
                                          this.Location.Y + e.Y - _mouseDownLocation.Y);
            }
        }

        private void OverlayForm_OnClose(object sender, EventArgs e)
        {
            _formProperties = new Rectangle(this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);
        }

        public static Rectangle Show(Rectangle rect, string formName = "")
        {
            using (var ovForm = new OverlayForm(rect, formName))
            {
                ovForm.Load += (sender, e) =>
                {
                    // Set the window as TopMost using native flags
                    IntPtr handle = ovForm.Handle;
                    uint extendedStyle = GetWindowLong(handle, GWL_EXSTYLE);
                    WindowHandler.SetWindowLong(handle, GWL_EXSTYLE, extendedStyle | WS_EX_TOPMOST);

                    // Force focus
                    ovForm.Activate();
                };
                ovForm.ShowDialog();
                
                return _formProperties;
            }
        }
        private void OverlayForm_Paint(object sender, PaintEventArgs e)
        {
            // Draw a border around the rectangle
            using (Pen pen = new Pen(Color.Blue, 3))
            {
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            }
        }
    }
}
