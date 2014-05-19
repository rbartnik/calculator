using System;
using System.Globalization;
using System.Windows.Data;
using Calculator.ViewModel;

namespace Calculator
{
   public class OperatorToStringConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         var toConvert = (Operator) value;
         switch (toConvert)
         {
            case Operator.Divide:
               {
                  return "÷";
               }
            case Operator.Multiply:
               {
                  return "×";
               }
            case Operator.Add:
               {
                  return "+";
               }
            case Operator.Subtract:
               {
                  return "-";
               }
            default:
               {
                  return string.Empty;
               }
         }
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}
