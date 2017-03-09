using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanosCanvas
{
    public class CanvasDraw
    {
        public double CanvasWidth { get; set; }

        public double CanvasHeight { get; set; }

        public double Top { get; set; }

        public double Left { get; set; }

        /// <summary>
        /// 缩放比例
        /// </summary>
        public double Ratio { get; set; }
    }
}
