using System;
using Android.Content;
using Android.Graphics.Drawables;
using SpeedTest.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(RoundedEntryRenderer))]
namespace SpeedTest.Droid
{
    public class RoundedEntryRenderer : EntryRenderer
    {
        public RoundedEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(20f);
                gradientDrawable.SetStroke(5, Android.Graphics.Color.White);
                gradientDrawable.SetColor(Android.Graphics.Color.Rgb(239, 239, 239));
                Control.SetBackground(gradientDrawable);

                Control.SetPadding(10, 10, 10, 10);
            }
        }
    }
}
