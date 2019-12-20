using System;
using System.Collections.Generic;
using System.Text;

namespace Blazorise.IconPicker
{
    public class IconSizeOption : IconOption
    {
        public IconSize Id { get; set; }
        public static List<IconSizeOption> Options { get; } = new List<IconSizeOption>
        {
            new IconSizeOption() { Id = IconSize.ExtraSmall, Text = "Extra Small", Value = " fa-xs" },
            new IconSizeOption() { Id = IconSize.Small, Text = "Small", Value = " fa-sm" },
            new IconSizeOption() { Id = IconSize.Default, Text = "Default", Value = "" },
            new IconSizeOption() { Id = IconSize.Large, Text = "Large", Value = " fa-lg" },
            new IconSizeOption() { Id = IconSize.x2, Text = "x2", Value = " fa-2x" },
            new IconSizeOption() { Id = IconSize.x3, Text = "x3", Value = " fa-3x" },
            new IconSizeOption() { Id = IconSize.x4, Text = "x4", Value = " fa-4x" },
            new IconSizeOption() { Id = IconSize.x5, Text = "x5", Value = " fa-5x" },
            new IconSizeOption() { Id = IconSize.x6, Text = "x6", Value = " fa-6x" },
            new IconSizeOption() { Id = IconSize.x7, Text = "x7", Value = " fa-7x" },
            new IconSizeOption() { Id = IconSize.x8, Text = "x8", Value = " fa-8x" },
            new IconSizeOption() { Id = IconSize.x9, Text = "x9", Value = " fa-9x" },
            new IconSizeOption() { Id = IconSize.x10, Text = "x10", Value = " fa-10x" }
        };
    }

}
