using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanosCanvas
{
    public static class CanosSizeUtility
    {
        private const double CanvasMargin = 10;

        public static CanvasDraw GetCanvasSize(double controlActualWidth, double controlActualHeight, int canvasWidth, int canvasHeight)
        {
            CanvasDraw cc = new CanvasDraw();

            double availableWidth = controlActualWidth - CanvasMargin * 2;
            double availableHeight = controlActualHeight - CanvasMargin * 2;

            if (canvasWidth <= availableWidth && canvasHeight <= availableHeight)//不需要缩放
            {
                cc.CanvasWidth = canvasWidth;
                cc.CanvasHeight = canvasHeight;

            }
            else//需要缩放
            {
                double finalWidth = Math.Min(canvasWidth, availableWidth);
                var finalHeight = finalWidth * canvasHeight / canvasWidth;
                if (finalHeight > availableHeight)
                {
                    finalHeight = availableHeight;
                    finalWidth = finalHeight * canvasWidth / canvasHeight;
                }

                cc.CanvasWidth = finalWidth;
                cc.CanvasHeight = finalHeight;
            }

            cc.Left = (controlActualWidth - cc.CanvasWidth) / 2;
            cc.Top = (controlActualHeight - cc.CanvasHeight) / 2;
            cc.Ratio = cc.CanvasWidth / canvasWidth;

            return cc;
        }
    }
}
