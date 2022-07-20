using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MVVMHelper.Converters
{
    public class BitmapToImageSourceConverter : IValueConverter
    {
        public static BitmapSource ConvertToImageSource(Bitmap bmp)
        {
            var conv = new BitmapToImageSourceConverter();
            return (BitmapSource)conv.Convert(bmp, null, null, null);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Bitmap)
            {
                Bitmap bitmap = (Bitmap)value;
                var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

                var bitmapData = bitmap.LockBits(
                    rect,
                    ImageLockMode.ReadWrite,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                try
                {
                    var size = (rect.Width * rect.Height) * 4;

                    return BitmapSource.Create(
                        bitmap.Width,
                        bitmap.Height,
                        bitmap.HorizontalResolution,
                        bitmap.VerticalResolution,
                        PixelFormats.Bgra32,
                        null,
                        bitmapData.Scan0,
                        size,
                        bitmapData.Stride);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
            }
            else
                throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
