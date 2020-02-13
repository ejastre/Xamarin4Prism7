using Xamarin.Forms;
using XamarinPrism7.Models;
using XamarinPrism7.ViewModels;

namespace XamarinPrism7.Views
{
    public partial class Test : ContentPage
    {
        private readonly TestViewModel _viewModel;

        public Test()
        {
            InitializeComponent();

            _viewModel = new TestViewModel();
            BindingContext = _viewModel;            
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _viewModel.PedidoSelecionado = e.SelectedItem as Pedido;
        }
    }
}
