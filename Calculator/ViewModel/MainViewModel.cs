using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Calculator.ViewModel
{
   public enum Operator
   {
      None = 0,
      Add,
      Subtract,
      Multiply,
      Divide,
      Equals
   }

   public class MainViewModel : ViewModelBase
   {
      private double _result;
      public double Result
      {
         get
         {
            return _result;
         }
         set
         {
            _result = value;
            RaisePropertyChanged( "Result" );
         }
      }

      private string _display;
      public string Display
      {
         get
         {
            return _display;
         }
         set
         {
            _display = value;
            RaisePropertyChanged( "Display" );
         }
      }

      private string _input;
      public string Input
      {
         get
         {
            return _input;
         }
         set
         {
            _input = value;
            RaisePropertyChanged( "Input" );
         }
      }

      private Operator _operator;
      public Operator Operator
      {
         get
         {
            return _operator;
         }
         set
         {
            _operator = value;
            RaisePropertyChanged( "Operator" );
         }
      }

      private bool _inputChanged;
      public bool InputChanged
      {
         get
         {
            return _inputChanged;
         }
         set
         {
            _inputChanged = value;
            RaisePropertyChanged( "InputChanged" );
         }
      }

      public MainViewModel()
      {
         InputValueCommand = new RelayCommand<string>( HandleInputCommand );
         SetOperatorCommand = new RelayCommand<Operator>( HandleSetOperatorCommand );
         ClearCommand = new RelayCommand( HandleClearCommand, () => true );
         PlusMinusCommand = new RelayCommand( HandlePlusMinusCommand );
         HandleClearCommand();
      }

      public RelayCommand<string> InputValueCommand
      {
         get;
         private set;
      }

      public RelayCommand<Operator> SetOperatorCommand
      {
         get;
         private set;
      }

      public RelayCommand ClearCommand
      {
         get;
         private set;
      }

      public RelayCommand PlusMinusCommand
      {
         get;
         private set;
      }

      private void HandleInputCommand( string value )
      {
         // To recycle previous sums
         if ( !InputChanged )
         {
            Input = "0";
         }

         // Don't want leading zeroes
         if ( Input == "0" && value == "0" )
         {
            return;
         }

         // Only one decimal allowed
         if ( Input.Contains( "." ) && value == "." )
         {
            return;
         }

         // No inputs longer than ten characters
         if ( Input.Length > 9 )
         {
            return;
         }
         
         // Again, no leading zeroes, unless it's a decimal
         if ( Input == "0" && value != "." )
         {
            Input = value;
         }
         else
         {
            Input = Input + value;
         }

         InputChanged = true;
         Display = Input;
      }

      private void HandleSetOperatorCommand(Operator newOp)
      {
         if ( Operator == newOp && newOp == Operator.Equals )
         {
            return;
         }

         if ( InputChanged )
         {
            ProcessInput( Operator );
         }

         Operator = newOp;  
      }

      private void HandlePlusMinusCommand()
      {
         var inputValue = double.Parse( Input );
         inputValue *= -1;
         Input = inputValue.ToString();
         InputChanged = true;
         Display = Input;
      }

      private void HandleClearCommand()
      {
         Result = 0;
         Display = "0";
         Input = "0";
         Operator = Operator.None;
         InputChanged = false;
      }


      private void ProcessInput( Operator opToUse )
      {
         var inputValue = double.Parse( Input );

         if ( !HadPreviousOperator() )
         {
            Result = inputValue;
         }
         else
         {
            switch ( opToUse )
            {
               case Operator.Multiply:
               {
                  Result = Result * inputValue;
                  break;
               }
               case Operator.Divide:
               {
                  Result = Result / inputValue;
                  break;
               }
               case Operator.Add:
               {
                  Result = Result + inputValue;
                  break;
               }
               case Operator.Subtract:
               {
                  Result = Result - inputValue;
                  break;
               }
            }
         }

         Display = Result.ToString();
         Input = Display;
         InputChanged = false;
      }

      private bool HadPreviousOperator()
      {
         return Operator != Operator.None && Operator != Operator.Equals;
      }
   }
}