using Com.IT.DiPaSport.Model.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Com.IT.DiPaSport.Model
{
    public class AddToCartConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isVisible = (bool)value;
            bool g0 = LoginModel.Group == CustomerGroup.NOT_LOGGED_IN;
            bool g16 = LoginModel.Group == CustomerGroup.NOPREZZI;
            bool g17 = LoginModel.Group == CustomerGroup.BANNATI;
            if (!isVisible || (g0 || g16 || g17))
            {
                isVisible = false;
            }
            else
            {
                isVisible = true;
            }
            return isVisible ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return System.Windows.Visibility.Visible;
        }
    }
}
