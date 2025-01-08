using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTools_0._0._1.classes
{
    [Serializable]
    public class SerializableRectangle
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public SerializableRectangle() { }

        public SerializableRectangle(Rectangle rect)
        {
            this.X = rect.X;
            this.Y = rect.Y;
            this.Width = rect.Width;
            this.Height = rect.Height;
        }

        public Rectangle toRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }
    }
}
