using ArcherTools_0._0._1.cfg;
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

        public async Task<Point> TrackMouse(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {

                Point currentPosition = Cursor.Position;

                if (Control.MouseButtons != MouseButtons.None)
                {
                    return currentPosition;
                }
                await Task.Delay(50);
            }
            return Point.Empty;           
        }
    }
}





        

