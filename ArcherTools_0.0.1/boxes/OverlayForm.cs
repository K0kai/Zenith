using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ArcherTools_0._0._1.WindowHandler;

namespace ArcherTools_0._0._1.boxes
{
    class OverlayForm : Form
    {
        private Point _mouseDownLocation;

        public OverlayForm(Rectangle rect)
        {
            // Set form properties
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = Color.Red;
            this.Opacity = 0.5; // Semi-transparent
            this.TopMost = true; // Always on top
            this.ShowInTaskbar = false;

            // Set position and size
            this.Location = new Point(rect.Left, rect.Top);
            this.Size = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);

            // Add close button
            Button closeButton = new Button
            {
                Text = "X",
                ForeColor = Color.White,
                BackColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(30, 30),
                Location = new Point(this.Width - 40, 10)
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (sender, e) => this.Close();
            this.Controls.Add(closeButton);

            // Enable dragging by capturing mouse events
            this.MouseDown += OverlayForm_MouseDown;
            this.MouseMove += OverlayForm_MouseMove;

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
