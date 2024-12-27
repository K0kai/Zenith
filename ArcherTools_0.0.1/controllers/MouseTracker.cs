using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.controllers
{
    
    internal class MouseTracker
    {
        Form _form;
        Label _labelCoordinates;

        public MouseTracker(Form form)
        {
            _form = form;
            _labelCoordinates = new Label
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                ForeColor = System.Drawing.Color.Red,
                Padding = new Padding(5),
                Text = "X:0, Y : 0",
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(0, 0),
                Visible = true
            };
            _form.Controls.Add(_labelCoordinates);
            _labelCoordinates.BringToFront();
        }

        public void TrackMouse(Action<Point> onMouseClick) {

            _form.Invoke(new Action(() => _form.Enabled = false));

           

            while (true) {

                Point currentPosition = Cursor.Position;

                if (_labelCoordinates.InvokeRequired)
                {
                    _labelCoordinates.Invoke(new Action(() =>
                    {
                        _labelCoordinates.Location = new System.Drawing.Point(currentPosition.X + 10, currentPosition.Y + 10);

                        _labelCoordinates.Text = $"X: {currentPosition.X}, Y: {currentPosition.Y}";
                    }));
                }



                if (Control.MouseButtons != MouseButtons.None) {
                    onMouseClick?.Invoke(currentPosition);
                    break;
                }

                Thread.Sleep(50);
            }

            _form.Invoke(new Action(() => _form.Enabled = true));
            _labelCoordinates.Dispose();
            _labelCoordinates = null;




        }
        
    }
}
