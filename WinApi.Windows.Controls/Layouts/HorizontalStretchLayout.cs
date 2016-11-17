using NetCoreEx.Geometry;
using System.Collections.Generic;
using WinApi.Core;
using WinApi.User32;

namespace WinApi.Windows.Controls.Layouts
{
    public class HorizontalStretchLayout
    {
        public List<NativeWindow> Children;
        public Rectangle ClientArea;
        public Rectangle Margin;

        public HorizontalStretchLayout(int capacity = 0)
        {
            this.Children = new List<NativeWindow>(capacity);
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
            var clientRect = clientArea;
            Rectangle.Deflate(ref clientRect, ref margin);
            var len = this.Children.Count;

            var childSize = clientRect.Size;
            childSize.Width = childSize.Width/len;

            var cx = clientRect.Left;
            var cy = clientRect.Top;

            foreach (var windowCore in this.Children)
            {
                windowCore.SetPosition(cx, cy, childSize.Width, childSize.Height);
                cx += childSize.Width;
            }
        }
    }
}