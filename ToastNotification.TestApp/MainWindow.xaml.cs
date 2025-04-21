using System.Windows;

namespace ToastNotification.TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tuna.ToastNotification.Services.ToastService.Instance.ShowToast("Hello World Tuna.ToastNotification.ViewModel.ToastType.Info, Tuna.ToastNotification.ViewModel.ToastStyleMode.OnlyIconColor, 3000, 500, Tuna.ToastNotifiTuna.ToastNotification.ViewModel.ToastType.Info, Tuna.ToastNotification.ViewModel.ToastStyleMode.OnlyIconColor, 3000, 500, Tuna.ToastNotifi", Tuna.ToastNotification.ViewModel.ToastType.Success, Tuna.ToastNotification.ViewModel.ToastStyleMode.OnlyIconColor, 3000, 500, Tuna.ToastNotification.ViewModel.ToastPosition.TopCenter);
        }
    }
}
