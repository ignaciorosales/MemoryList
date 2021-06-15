using System;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using memoryList.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(Entry), typeof(MyEntryRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace memoryList.Droid.CustomRenderers
{
    [Obsolete]
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Gray);
            else
                Control.Background.SetColorFilter(Android.Graphics.Color.Gray, PorterDuff.Mode.SrcAtop);
        }
    }
}
