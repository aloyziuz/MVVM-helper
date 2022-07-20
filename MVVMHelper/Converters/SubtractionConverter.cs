using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MVVMHelper.Converters
{
    public class SubtractionConverter : IMultiValueConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double size = System.Convert.ToDouble(value);
                double reduction = System.Convert.ToDouble(parameter);
                return size - reduction;
            }
            catch (Exception)
            {
                return value;
            }
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var acc = System.Convert.ToDecimal(values[0]);
                for (int i = 1; i < values.Length; i++)
                {
                    acc -= System.Convert.ToDecimal(values[i]);
                }
                return acc;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
