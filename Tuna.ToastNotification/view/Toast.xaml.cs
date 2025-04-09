using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using Tuna.ToastNotification.ViewModel;

namespace Tuna.ToastNotification.View
{
    /// <summary>
    /// Interaction logic for Toast.xaml
    /// </summary>
    public partial class Toast : Window
    {
        public int _duration { get; set; } = 2500; // Thời gian hiển thị Toast (mặc định là 3 giây)
        private ToastPosition _position;

        public Toast(string message, ToastType type = ToastType.Info, ToastStyleMode styleMode = ToastStyleMode.OnlyIconColor, int duration = 2500, double maxWidth = 400, ToastPosition position = ToastPosition.TopCenter)
        {
            InitializeComponent();
            _duration = duration;
            _position = position;
            DataContext = new ToastViewModel(message, type, styleMode, duration, maxWidth);
        }



        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Ẩn cửa sổ hoàn toàn ban đầu
            this.Opacity = 0;
            this.SizeToContent = SizeToContent.WidthAndHeight;

            // Cho phép WPF hoàn tất việc binding và layout
            await Dispatcher.InvokeAsync(() =>
            {
                this.UpdateLayout(); // Rất quan trọng để có ActualWidth chính xác

                // Tính lại vị trí căn giữa sau khi layout xong
                var workingArea = SystemParameters.WorkArea;
                //this.Left = workingArea.Left + (workingArea.Width - this.ActualWidth) / 2;
                //this.Top = workingArea.Bottom - workingArea.Height * 0.90;

                SetPosition();

                // Bây giờ mới hiển thị
                this.Opacity = 1;
            }, System.Windows.Threading.DispatcherPriority.Render);

            // Animation xuất hiện
            var fadeIn = (Storyboard)this.Resources["FadeInSlideDown"];
            fadeIn?.Begin();

            await Task.Delay(_duration);

            // Animation biến mất
            var fadeOut = (Storyboard)this.Resources["FadeOutSlideDown"];
            fadeOut.Begin();
        }

        private void SetPosition()
        {
            switch (_position)
            {
                case ToastPosition.TopLeft: SetTopLeftPosition(); break;
                case ToastPosition.TopCenter: SetTopCenterPosition(); break;
                case ToastPosition.TopRight: SetTopRightPosition(); break;
                case ToastPosition.BottomLeft: SetBottomLeftPosition(); break;
                case ToastPosition.BottomCenter: SetBottomCenterPosition(); break;
                case ToastPosition.BottomRight: SetBottomRightPosition(); break;
            }
        }

        private void SetTopLeftPosition()
        {
            var workArea = SystemParameters.WorkArea;
            this.Left = workArea.Left + 20;
            this.Top = workArea.Top + 40;
        }

        private void SetTopCenterPosition()
        {
            var workArea = SystemParameters.WorkArea;
            this.Left = workArea.Left + (workArea.Width - this.ActualWidth) / 2;
            this.Top = workArea.Top + 40;
        }

        private void SetTopRightPosition()
        {
            var workArea = SystemParameters.WorkArea;
            this.Left = workArea.Right - this.ActualWidth - 20;
            this.Top = workArea.Top + 20;
        }

        private void SetBottomLeftPosition()
        {
            var workArea = SystemParameters.WorkArea;
            this.Left = workArea.Left + 20;
            this.Top = workArea.Bottom - this.ActualHeight - 20;
        }

        private void SetBottomCenterPosition()
        {
            var workArea = SystemParameters.WorkArea;
            this.Left = workArea.Left + (workArea.Width - this.ActualWidth) / 2;
            this.Top = workArea.Bottom - this.ActualHeight - 20;
        }

        private void SetBottomRightPosition()
        {
            var workArea = SystemParameters.WorkArea;
            this.Left = workArea.Right - this.ActualWidth - 20;
            this.Top = workArea.Bottom - this.ActualHeight - 20;
        }


        private void FadeOut_Completed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
