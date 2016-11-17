using NetCoreEx.Geometry;
using System.Collections.Generic;
using WinApi.Core;
using WinApi.User32;

namespace WinApi.Windows.Controls.Layouts
{
    public class HorizontalStretchLayoutWithInnerMargin
    {
        public List<WindowCore> Children;
        public Rectangle ClientArea;
        public Rectangle InnerMargin;
        public Rectangle Margin;

        public HorizontalStretchLayoutWithInnerMargin(int capacity = 0)
        {
            this.Children = new List<WindowCore>(capacity);
        }

        public void SetSize(ref Size size)
        {
            if (this.ClientArea.Size == size) return;
            this.ClientArea.Width = size.Width;
            this.ClientArea.Height = size.Height;
            this.PerformLayout();
        }

        public void PerformLayout()
        {
            var clientArea = this.ClientArea;
            var margin = this.Margin;
            var innerMargin = this.InnerMargin;

            var clientRect = clientArea;
            Rectangle.Deflate(ref clientRect, ref margin);
            var len = this.Children.Count;

            var childSize = clientRect.Size;
            childSize.Width = childSize.Width/len;

            var cx = clientRect.Left + innerMargin.Left;
            var cy = clientRect.Top + innerMargin.Top;

            foreach (var windowCore in this.Children)
            {
                windowCore.SetPosition(cx, cy, childSize.Width - (innerMargin.Left + innerMargin.Right),
                    childSize.Height - (innerMargin.Top + innerMargin.Bottom));
                cx += childSize.Width;
            }
        }
    }
}