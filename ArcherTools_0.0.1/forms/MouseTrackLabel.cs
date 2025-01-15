﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.forms
{
    internal class MouseTrackLabel : Form
    {
        public Form mouseTrackForm;
        public Label mouseLabel;

        internal MouseTrackLabel()
        {
            mouseTrackForm = this;

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.TopMost = true;
            this.TopLevel = true;
            this.ShowInTaskbar = false;
            this.BackColor = Color.White;
            this.TransparencyKey = BackColor;
            this.Opacity = 1;
            this.Width = 100;
            this.Height = 25;
            this.AutoSize = true;

            mouseLabel = new Label {
                AutoSize = true,
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Padding = new Padding(5)
            };
            this.Controls.Add(mouseLabel);
            this.Show();
            this.BringToFront();

        }

        public async void UpdateAsync(Point mousePos)
        {
            mouseLabel.Location = new Point(Cursor.Position.X + 10, Cursor.Position.Y + 5);
            mouseLabel.Text = $"X: {mousePos.X}, Y: {mousePos.Y}";
            await Task.Delay(50);
        }

        public void Destroy()
        {
            this.Dispose();
        }
    }
}

/*
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
        Form _labelForm;
        Label _labelCoordinates;

        public MouseTracker(Form form)
        {
            _form = form;
            _labelForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                TopMost = true,
                ShowInTaskbar = false,
                BackColor = Color.Black,
                TransparencyKey = Color.Black,
                Opacity = 0.8,
                Width = 100,
                Height = 25,                
                AutoSize = true,
            };
            _labelCoordinates = new Label
            {
                AutoSize = true,
                ForeColor = Color.Red,
                BackColor = Color.Transparent,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Padding = new Padding(5)
            };
            _labelForm.Controls.Add(_labelCoordinates);
            _labelForm.Show();
            _labelCoordinates.BringToFront();
        }

        public void TrackMouse(Action<Point> onMouseClick) {

            _form.Invoke(new Action(() => _form.Enabled = false));

           

            while (true) {

                Point currentPosition = Cursor.Position;

                if (Control.MouseButtons != MouseButtons.None)
                {
                    onMouseClick?.Invoke(currentPosition);
                    break;
                }

                if (_labelForm.InvokeRequired)
                {
                    _labelForm.Invoke(new Action(() =>
                    {
                        _labelForm.Location = new Point(currentPosition.X + 10, currentPosition.Y + 10);
                        _labelCoordinates.Text = $"X: {currentPosition.X}, Y: {currentPosition.Y}";
                    }));
                }
                else
                {
                    _labelForm.Location = new Point(currentPosition.X + 10, currentPosition.Y + 10);
                    _labelCoordinates.Text = $"X: {currentPosition.X}, Y: {currentPosition.Y}";
                }



                if (Control.MouseButtons != MouseButtons.None) {
                    onMouseClick?.Invoke(currentPosition);
                    break;
                }

                Thread.Sleep(50);
            }

            _form.Invoke(new Action(() => _form.Enabled = true));
            _labelForm.Dispose();
            _labelForm = null;
            _labelCoordinates = null;




        }
        
    }
}
*/
