#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
#endregion

namespace Blazorise.IconPicker
{
    public class BaseIconPicker : ComponentBase
    {
        protected override void OnInitialized()
        {
            if ( !string.IsNullOrEmpty( Icon ) )
            {
                IconName = string.Join( " ", Icon.Split().Take( 3 ) );
                IconMargin = Margin.Is2.FromLeft;
                (RotationOption, SizeOption) = ExtractOptionsFromIcon( Icon );
                if ( RotationOption.Id != IconRotation.None )
                    RotationOptionChanged( RotationOption.Id );
                if ( SizeOption.Id != IconSize.Default )
                    SizeOptionChanged( SizeOption.Id );
            }
            foreach ( IconRotationOption option in IconRotationOption.Options )
            {
                switch ( option.Id )
                {
                    case IconRotation.None:
                        if ( AllowFlip | AllowRotation | AllowAnimation )
                            RotationOptions.Add( option );
                        break;
                    case IconRotation.FlipHorizontal:
                        if ( AllowFlip )
                            RotationOptions.Add( option );
                        break;
                    case IconRotation.FlipVertical:
                        if ( AllowFlip )
                            RotationOptions.Add( option );
                        break;
                    case IconRotation.FlipBoth:
                        if ( AllowFlip )
                            RotationOptions.Add( option );
                        break;
                    case IconRotation.Rotate90:
                        if ( AllowRotation )
                            RotationOptions.Add( option );
                        break;
                    case IconRotation.Rotate180:
                        if ( AllowRotation )
                            RotationOptions.Add( option );
                        break;
                    case IconRotation.Rotate270:
                        if ( AllowRotation )
                            RotationOptions.Add( option );
                        break;
                    case IconRotation.Spin:
                        if ( AllowAnimation )
                            RotationOptions.Add( option );
                        break;
                    case IconRotation.Pulse:
                        if ( AllowAnimation )
                            RotationOptions.Add( option );
                        break;
                    default:
                        break;
                }
            }
            foreach ( IconSizeOption option in IconSizeOption.Options )
            {
                if ( AllowSize & option.Id >= AllowSizeMinimum & option.Id <= AllowSizeMaximum )
                    SizeOptions.Add( option );
            }
        }

        protected void ShowModal()
        {
            IconBackup = Icon;
            Modal.Show();
        }

        protected async Task ModalCancel()
        {
            Icon = IconBackup;
            (RotationOption, SizeOption) = ExtractOptionsFromIcon( Icon );
            await IconChanged.InvokeAsync( Icon );
            Modal.Hide();
        }

        protected async Task ModalSubmit()
        {
            await IconChanged.InvokeAsync( Icon ); //TODO: Submit and Cancel buttons are with daemonite style. Check blazorise for update https://github.com/stsrki/Blazorise/issues/430
            Modal.Hide();
        }

        protected void IconClicked( string icon )
        {
            IconName = icon;
            Icon = Format( IconName, RotationOption.Id, SizeOption.Id );
            IconMargin = Margin.Is2.FromLeft;
        }

        protected void RotationOptionChanged( object option )
        {
            RotationOption = IconRotationOption.Options.Find( x => x.Id == (IconRotation)option );
            Icon = Format( IconName, RotationOption.Id, SizeOption.Id );
        }

        protected void SizeOptionChanged( object option )
        {
            SizeOption = IconSizeOption.Options.Find( x => x.Id == (IconSize)option );
            Icon = Format( IconName, RotationOption.Id, SizeOption.Id );
        }

        private (IconRotationOption rotationOption, IconSizeOption sizeOption) ExtractOptionsFromIcon( string icon )
        {
            IconRotationOption rotationOption = IconRotationOption.Options.Find( x => x.Id == IconRotation.None );
            IconSizeOption sizeOption = IconSizeOption.Options.Find( x => x.Id == IconSize.Default );
            foreach ( string part in icon.Split( " "[0] ) )
            {
                rotationOption = IconRotationOption.Options.Find( x => x.Value.Trim() == part ) ?? rotationOption;
                sizeOption = IconSizeOption.Options.Find( x => x.Value.Trim() == part ) ?? sizeOption;
            }
            return (rotationOption, sizeOption);
        }

        protected static string Format( string icon, IconRotation rotate = IconRotation.None, IconSize size = IconSize.Default )
        {
            return icon + IconRotationOption.Options.Find( x => x.Id == rotate ).Value + IconSizeOption.Options.Find( x => x.Id == size ).Value;
        }

        protected Modal Modal { get; set; }
        protected IFluentSpacingOnBreakpointWithSideAndSize IconMargin { get; set; } = Margin.Is0.OnAll;
        protected List<IconRotationOption> RotationOptions { get; set; } = new List<IconRotationOption>();
        protected List<IconSizeOption> SizeOptions { get; set; } = new List<IconSizeOption>();
        protected IconRotationOption RotationOption { get; set; } = IconRotationOption.Options.Find( x => x.Id == IconRotation.None );
        protected IconSizeOption SizeOption { get; set; } = IconSizeOption.Options.Find( x => x.Id == IconSize.Default );
        protected string IconName { get; set; } = string.Empty;
        private string IconBackup { get; set; }

        [Parameter] public string Icon { get; set; }
        [Parameter] public bool AllowFlip { get; set; } = true;
        [Parameter] public bool AllowRotation { get; set; } = true;
        [Parameter] public bool AllowAnimation { get; set; } = true;
        [Parameter] public bool AllowSize { get; set; } = true;
        [Parameter] public IconSize AllowSizeMinimum { get; set; } = IconSize.ExtraSmall;
        [Parameter] public IconSize AllowSizeMaximum { get; set; } = IconSize.x10;
        [Parameter] public EventCallback<string> IconChanged { get; set; }
        [Parameter] public ButtonSize ButtonSize { get; set; } = ButtonSize.None;
        [Parameter] public Color ButtonColor { get; set; } = Color.None;
        [Parameter] public string ButtonTooltipText { get; set; } = "Select an Icon";
        [Parameter] public Placement ButtonTooltipPlacement { get; set; } = Placement.Bottom;
        [Parameter] public string ModalTitle { get; set; } = "Select an Icon";
        [Parameter] public ButtonSize ModalButtonSize { get; set; } = ButtonSize.None;
        [Parameter] public Placement ModalTooltipPlacement { get; set; } = Placement.Bottom;
    }
}
