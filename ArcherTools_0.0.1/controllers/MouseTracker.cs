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

        public MouseTracker(Form form)
        {
            _form = form;
        }

        public void TrackMouse(Action<Point> onMouseClick) {

            _form.Invoke(new Action(() => _form.Enabled = false));

            while (true) {

                Point currentPosition = Cursor.Position;

                
                
                if (Control.MouseButtons != MouseButtons.None) {
                    onMouseClick?.Invoke(currentPosition);
                    break;
                }

                Thread.Sleep(50);
            }

            _form.Invoke(new Action(() => _form.Enabled = true));




        }
        
    }
}
