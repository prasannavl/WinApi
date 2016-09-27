using System.Collections.Generic;
using WinApi.Core;
using WinApi.User32;
using WinApi.XWin.Helpers;

namespace WinApi.XWin.Controls.Layouts
{
    public class HorizontalStretchLayout
    {
        public List<WindowCore> Children;
        public Rectangle ClientArea;
        public Rectangle Margin;

        public HorizontalStretchLayout(int capacity = 0)
        {
            this.Children = new List<WindowCore>(capacity);
        }

        public void SetSize(ref Size size)
        {
            if (ClientArea.GetSize() == size) return;
            ClientArea.Width = size.Width;
            ClientArea.Height = size.Height;
            PerformLayout();
        }

        public void PerformLayout()
        {
            var clientArea = ClientArea;
            var margin = Margin;
            var clientRect = clientArea;
            RectangleHelpers.PadInside(ref clientRect, ref margin);
            var len = Children.Count;

            var childSize = clientRect.GetSize();
            childSize.Width = childSize.Width / len;

            var cx = clientRect.Left;
            var cy = clientRect.Top;

            foreach (var windowCore in Children)
            {
                windowCore.SetPosition(cx, cy, childSize.Width, childSize.Height);
                cx += childSize.Width;
            }
        }
    }
}