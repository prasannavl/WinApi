using System.Collections.Generic;
using WinApi.Core;
using WinApi.Helpers;
using WinApi.User32;

namespace WinApi.Windows.Controls.Layouts
{
    public class HorizontalStretchLayoutWithInnerMargin
    {
        public List<WindowCore> Children;
        public Rectangle ClientArea;
        public Rectangle Margin;
        public Rectangle InnerMargin;

        public HorizontalStretchLayoutWithInnerMargin(int capacity = 0)
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
            var innerMargin = InnerMargin;

            var clientRect = clientArea;
            RectangleHelpers.PadInside(ref clientRect, ref margin);
            var len = Children.Count;

            var childSize = clientRect.GetSize();
            childSize.Width = childSize.Width / len;

            var cx = clientRect.Left + innerMargin.Left;
            var cy = clientRect.Top + innerMargin.Top;

            foreach (var windowCore in Children)
            {
                windowCore.SetPosition(cx, cy, childSize.Width - (innerMargin.Left + innerMargin.Right),
                    childSize.Height - (innerMargin.Top + innerMargin.Bottom));
                cx += childSize.Width;
            }
        }
    }
}
