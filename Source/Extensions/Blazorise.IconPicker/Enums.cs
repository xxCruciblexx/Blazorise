#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Blazorise.IconPicker
{
    public enum IconRotation
    {
        None,
        FlipHorizontal,
        FlipVertical,
        FlipBoth,
        Rotate90,
        Rotate180,
        Rotate270,
        Spin,
        Pulse
    }

    public enum IconSize
    {
        ExtraSmall, //0.75em
        Small, //0.875em
        Default, //1em
        Large, //1.33em
        x2, //2em
        x3, //3em
        x4, //4em
        x5, //5em
        x6, //6em
        x7, //7em
        x8, //8em
        x9, //9em
        x10 //10em
    }
}
