using System;
using System.Collections.Generic;
using System.Text;

namespace Blazorise.IconPicker
{
    public class IconRotationOption : IconOption
    {
        public IconRotation Id { get; set; }
        public static List<IconRotationOption> Options { get; } = new List<IconRotationOption>
        {
            new IconRotationOption() { Id = IconRotation.None, Text = "None", Value = "" },
            new IconRotationOption() { Id = IconRotation.FlipHorizontal, Text = "Flip Horizontal", Value = " fa-flip-horizontal" },
            new IconRotationOption() { Id = IconRotation.FlipVertical, Text = "Flip Vertical", Value = " fa-flip-vertical" },
            new IconRotationOption() { Id = IconRotation.FlipBoth, Text = "Flip Both", Value = " fa-flip-both" },
            new IconRotationOption() { Id = IconRotation.Rotate90, Text = "Rotate 90°", Value = " fa-rotate-90" },
            new IconRotationOption() { Id = IconRotation.Rotate180, Text = "Rotate 180°", Value = " fa-rotate-180" },
            new IconRotationOption() { Id = IconRotation.Rotate270, Text = "Rotate 270°", Value = " fa-rotate-270" },
            new IconRotationOption() { Id = IconRotation.Spin, Text = "Spin", Value = " fa-spin" },
            new IconRotationOption() { Id = IconRotation.Pulse, Text = "Pulse", Value = " fa-pulse" }
        };
    }
}
