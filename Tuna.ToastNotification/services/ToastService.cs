using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Tuna.ToastNotification.View;
using Tuna.ToastNotification.ViewModel;

namespace Tuna.ToastNotification.Services
{
    public class ToastService
    {
        private static Toast _currentToast = null;
        private static readonly Queue<Toast> _toastQueue = new Queue<Toast>();
        private static bool _isDisplaying = false;

        public static void ShowToast(string message,
                                     ToastType type = ToastType.Info,
                                     ToastStyleMode mode = ToastStyleMode.OnlyIconColor,
                                     int duration = 2500,
                                     double maxWidth = 500,
                                     ToastPosition toastPosition = ToastPosition.TopCenter)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                // 1. Đóng Toast đang hiển thị
                _currentToast?.Close();

                // 2. Xóa queue cũ
                _toastQueue.Clear();

                // 3. Tạo và hiển thị Toast mới ngay
                var toast = new Toast(message, type, mode, duration, maxWidth, toastPosition);
                _currentToast = toast;
                _isDisplaying = true;

                toast.Show();

                // 4. Bắt đầu quá trình đóng sau thời gian hiển thị
                _ = Task.Run(async () =>
                {
                    await Task.Delay(duration + 400);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (_currentToast == toast)
                        {
                            _currentToast = null;
                            _isDisplaying = false;
                        }
                    });
                });
            });
        }
    }
}
