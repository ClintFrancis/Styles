using System;
namespace Styles
{
    public class ColorSet
    {
        public string Id { get; set; }
        public ColorRGB BackgroundColor { get; set; }
        public ColorRGB PrimaryColor { get; set; }
        public ColorRGB SecondaryColor { get; set; }
        public ColorRGB DetailColor { get; set; }
        public IRgb[] Gradient { get; set; }
    }

    public struct ColorCount
    {
        public ColorRGB Color { get; set; }
        public int Count { get; set; }

        public ColorCount(ColorRGB color, int count)
        {
            Count = count;
            Color = color;
        }
    }
}
