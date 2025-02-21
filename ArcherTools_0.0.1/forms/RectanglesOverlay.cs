using ArcherTools_0._0._1.cfg;
using ArcherTools_0._0._1.forms;
using InputSimulatorEx;
using InputSimulatorEx.Native;
using System.Diagnostics;

namespace ArcherTools_0._0._1.boxes
{
    internal class RectanglesOverlay : Form
    {
        public static Form? thisForm;
        private const int ResizeMargin = 10;
        private static List<PowerHouseRectangles> pwhRects = new List<PowerHouseRectangles>();
        private PowerHouseRectangles? selectedPwhRect = null;
        private Point lastMousePos;
        private byte pwhMonitorAccessible;
        private ResizeDirection currentResizeDirection = ResizeDirection.None;
        private static MouseTrackLabel? mouseTrackLabel;


        internal RectanglesOverlay(List<PowerHouseRectangles> importedPwhRects, byte PwhMonitor = 1)
        {
            pwhMonitorAccessible = PwhMonitor;
            this.StartPosition = FormStartPosition.Manual;
            this.Bounds = Screen.AllScreens[PwhMonitor - 1].Bounds;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;
            this.Opacity = 0.6;
            this.TopMost = true;
            this.DoubleBuffered = true;
            this.ShowInTaskbar = false;

            pwhRects = importedPwhRects;

            this.MouseDown += GreatOverlay_MouseDown;
            this.MouseUp += GreatOverlay_MouseUp;
            this.MouseMove += GreatOverlay_MouseMove;
            this.MouseLeave += GreatOverlay_MouseLeave;

            this.Paint += GreatOverlay_Paint;

            thisForm = this;
           
            Task.Run(() =>
            {
                MonitorKeyPress(new InputSimulator());
            });
        }

        private static async Task MonitorKeyPress(InputSimulator inputSimulator)
        {
            while (true)
            {
                if (inputSimulator.InputDeviceState.IsKeyDown(VirtualKeyCode.END))
                {
                    Debug.WriteLine("'End' key pressed. Exiting...");
                    if (thisForm.InvokeRequired)
                    {
                        thisForm.Invoke((MethodInvoker)delegate
                        {
                            thisForm.Close();
                        });
                    }
                    else
                    {
                        thisForm.Close();
                    }
                    break;
                    
                }
                await Task.Delay(50);
            }
       }
        private void GreatOverlay_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var rect in pwhRects)
            {
                using (Brush brush = new SolidBrush(Color.FromArgb(128, Color.Red)))
                {
                    g.FillRectangle(brush, rect.getRectangle());
                }
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    g.DrawRectangle(pen, rect.getRectangle());
                }

                using (Brush textBrush = new SolidBrush(Color.White))
                {
                    var text = rect.ControlType.ToString();
                    g.DrawString(text, this.Font, textBrush, rect.getRectangle().Location);
                }
            }
        }

        private void GreatOverlay_MouseDown(object sender, MouseEventArgs e)
        {
            selectedPwhRect = pwhRects.FirstOrDefault(r => r.getRectangle().Contains(e.Location));
            mouseTrackLabel.Visible = false;
            if (selectedPwhRect != null)
            {
                currentResizeDirection = GetResizeDirection(selectedPwhRect.getRectangle(), e.Location);
                lastMousePos = e.Location;
            }
        }

        private void GreatOverlay_MouseMove(object sender, MouseEventArgs e)
        {
            var hoverRectangle = pwhRects.FirstOrDefault(r => r.getRectangle().Contains(e.Location));
            if (hoverRectangle != null)
            {
                if (mouseTrackLabel == null)
                {
                    mouseTrackLabel = new MouseTrackLabel();
                }
                mouseTrackLabel.UpdateAsync(new Point(e.X - hoverRectangle.getRectangle().Left, e.Y - hoverRectangle.getRectangle().Top));
            }
            if (selectedPwhRect != null)
            {
                if (currentResizeDirection == ResizeDirection.None && e.Button == MouseButtons.Left)
                {
                    var deltaX = e.X - lastMousePos.X;
                    var deltaY = e.Y - lastMousePos.Y;

                    deltaX = Math.Max(0, Math.Min(selectedPwhRect.getRectangle().X + deltaX, Screen.AllScreens[pwhMonitorAccessible - 1].Bounds.Width - selectedPwhRect.getRectangle().Width));
                    deltaY = Math.Max(0, Math.Min(selectedPwhRect.getRectangle().Y + deltaY, Screen.AllScreens[pwhMonitorAccessible - 1].Bounds.Height - selectedPwhRect.getRectangle().Height));

                    selectedPwhRect.setRectangle(new Rectangle(deltaX, deltaY, selectedPwhRect.getRectangle().Width, selectedPwhRect.getRectangle().Height));
                }
                else if (currentResizeDirection != ResizeDirection.None && e.Button == MouseButtons.Left)
                {
                    ResizeRectangle(selectedPwhRect, e.Location);
                }

                lastMousePos = e.Location;
                this.Invalidate();
            }
            else
            {
                if (hoverRectangle != null)
                {
                    var direction = GetResizeDirection(hoverRectangle.getRectangle(), e.Location);
                    this.Cursor = GetCursorForDirection(direction);

                }

                else
                {

                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void GreatOverlay_MouseLeave(object sender, EventArgs e)
        {
            if (mouseTrackLabel != null)
            {
                mouseTrackLabel.Destroy();
                mouseTrackLabel = null;
            }
        }


        private void GreatOverlay_MouseUp(object sender, MouseEventArgs e)
        {
            selectedPwhRect = null;
            currentResizeDirection = ResizeDirection.None;
            this.Cursor = Cursors.Default;
            mouseTrackLabel.Focus();
            mouseTrackLabel.Visible = true;
        }

        private ResizeDirection GetResizeDirection(Rectangle rect, Point mousePosition)
        {
            bool left = Math.Abs(mousePosition.X - rect.Left) <= ResizeMargin;
            bool right = Math.Abs(mousePosition.X - rect.Right) <= ResizeMargin;
            bool top = Math.Abs(mousePosition.Y - rect.Top) <= ResizeMargin;
            bool bottom = Math.Abs(mousePosition.Y - rect.Bottom) <= ResizeMargin;

            if (top && left) return ResizeDirection.TopLeft;
            if (top && right) return ResizeDirection.TopRight;
            if (bottom && left) return ResizeDirection.BottomLeft;
            if (bottom && right) return ResizeDirection.BottomRight;
            if (top) return ResizeDirection.Top;
            if (bottom) return ResizeDirection.Bottom;
            if (left) return ResizeDirection.Left;
            if (right) return ResizeDirection.Right;

            return ResizeDirection.None;
        }

        private Cursor GetCursorForDirection(ResizeDirection direction)
        {
            return direction switch
            {
                ResizeDirection.Top => Cursors.SizeNS,
                ResizeDirection.Bottom => Cursors.SizeNS,
                ResizeDirection.Left => Cursors.SizeWE,
                ResizeDirection.Right => Cursors.SizeWE,
                ResizeDirection.TopLeft => Cursors.SizeNWSE,
                ResizeDirection.TopRight => Cursors.SizeNESW,
                ResizeDirection.BottomLeft => Cursors.SizeNESW,
                ResizeDirection.BottomRight => Cursors.SizeNWSE,
                _ => Cursors.Default,
            };
        }

        private void ResizeRectangle(PowerHouseRectangles rect, Point mousePosition)
        {
            var bounds = rect.getRectangle();

            switch (currentResizeDirection)
            {
                case ResizeDirection.Top:
                    bounds = new Rectangle(bounds.X, mousePosition.Y, bounds.Width, bounds.Bottom - mousePosition.Y);
                    break;
                case ResizeDirection.Bottom:
                    bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, mousePosition.Y - bounds.Y);
                    break;
                case ResizeDirection.Left:
                    bounds = new Rectangle(mousePosition.X, bounds.Y, bounds.Right - mousePosition.X, bounds.Height);
                    break;
                case ResizeDirection.Right:
                    bounds = new Rectangle(bounds.X, bounds.Y, mousePosition.X - bounds.X, bounds.Height);
                    break;
                case ResizeDirection.TopLeft:
                    bounds = new Rectangle(mousePosition.X, mousePosition.Y, bounds.Right - mousePosition.X, bounds.Bottom - mousePosition.Y);
                    break;
                case ResizeDirection.TopRight:
                    bounds = new Rectangle(bounds.X, mousePosition.Y, mousePosition.X - bounds.X, bounds.Bottom - mousePosition.Y);
                    break;
                case ResizeDirection.BottomLeft:
                    bounds = new Rectangle(mousePosition.X, bounds.Y, bounds.Right - mousePosition.X, mousePosition.Y - bounds.Y);
                    break;
                case ResizeDirection.BottomRight:
                    bounds = new Rectangle(bounds.X, bounds.Y, mousePosition.X - bounds.X, mousePosition.Y - bounds.Y);
                    break;
            }
            const int MinSize = 20;
            if (bounds.Width >= MinSize && bounds.Height >= MinSize)
            {
                rect.setRectangle(bounds);
            }
        }

        public static List<PowerHouseRectangles> Show(List<PowerHouseRectangles> importedPwhRects, byte PwhMonitor = 1)
        {
            using (var rectOv = new RectanglesOverlay(importedPwhRects, PwhMonitor))
            {
                rectOv.ShowDialog();
                if (mouseTrackLabel != null)
                {
                    mouseTrackLabel.Destroy();
                    mouseTrackLabel = null;
                }
            }

            return pwhRects;
        }

    }
}
