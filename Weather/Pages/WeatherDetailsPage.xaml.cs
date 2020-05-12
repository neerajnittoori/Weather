using Weather.ViewModels.Implementations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Weather.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class WeatherDetailsPage : ContentPage
  {
    public WeatherDetailsPage()
    {
      InitializeComponent();
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();
      if (BindingContext is WeatherDetailsPageViewModel viewModel)
      {
        viewModel.LoadData.Execute(null);
      }
    }
  }
}