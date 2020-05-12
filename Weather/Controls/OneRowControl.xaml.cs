using System.Windows.Input;
using Xamarin.Forms;

namespace Weather.Controls
{
    public partial class OneRowControl : ContentView
    {
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(OneRowControl));

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(OneRowControl), default(ICommand));

        public static readonly BindableProperty PrimaryTextProperty = BindableProperty.Create(nameof(PrimaryText), typeof(string), typeof(OneRowControl), default(string));

        public OneRowControl()
        {
            InitializeComponent();
            var recognizer = new TapGestureRecognizer();

            recognizer.Tapped += (sender, e) =>
            {
              if (Command?.CanExecute(CommandParameter) ?? false)
                Command?.Execute(CommandParameter);
            };

            CellLayout.GestureRecognizers.Clear();
            CellLayout.GestureRecognizers.Add(recognizer);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public string PrimaryText
        {
            get => (string)GetValue(PrimaryTextProperty);
            set
      {
         SetValue(PrimaryTextProperty, value);
      }
        }
    }
}
