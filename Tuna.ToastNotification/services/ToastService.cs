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
        private static readonly List<Toast> _activeToasts = new List<Toast>();
        private static bool _isInitialized = false;

        public static void ShowToast(string message,
                                     ToastType type = ToastType.Info,
                                     ToastStyleMode mode = ToastStyleMode.OnlyIconColor,
                                     int duration = 2500,
                                     double maxWidth = 500,
                                     ToastPosition toastPosition = ToastPosition.TopCenter)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                // Khởi tạo listener khi thoát ứng dụng (chỉ 1 lần)
                if (!_isInitialized)
                {
                    Application.Current.Exit += (s, e) =>
                    {
                        foreach (var t in _activeToasts.ToArray())
                        {
                            t.Close();
                        }
                        _activeToasts.Clear();
                    };
                    _isInitialized = true;
                }

                // Đóng toast hiện tại nếu còn
                _currentToast?.Close();

                // Tạo mới toast
                var toast = new Toast(message, type, mode, duration, maxWidth, toastPosition)
                {
                    Owner = Application.Current.MainWindow
                };

                _activeToasts.Add(toast);
                toast.Closed += (s, e) => _activeToasts.Remove(toast);

                _currentToast = toast;
                toast.Show();

                // Tự xóa trạng thái sau khi hết thời gian
                _ = Task.Run(async () =>
                {
                    await Task.Delay(duration + 400);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (_currentToast == toast)
                        {
                            _currentToast = null;
                        }
                    });
                });
            });
        }
    }
}
