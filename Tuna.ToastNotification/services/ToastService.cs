using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Tuna.ToastNotification.View;
using Tuna.ToastNotification.ViewModel;

namespace Tuna.ToastNotification.Services
{
    public class ToastService
    {
        private static readonly ToastService _instance = new ToastService();
        public static ToastService Instance => _instance;

        private Toast _currentToast = null;
        private readonly List<Toast> _activeToasts = new List<Toast>();
        private bool _isInitialized = false;

        private ToastService() { }

        public void ShowToast(string message,
                              ToastType type = ToastType.Info,
                              ToastStyleMode mode = ToastStyleMode.OnlyIconColor,
                              int duration = 2500,
                              double maxWidth = 500,
                              ToastPosition toastPosition = ToastPosition.TopCenter)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                if (!_isInitialized)
                {
                    Application.Current.Exit += (s, e) =>
                    {
                        CloseAllToasts();
                    };
                    _isInitialized = true;
                }

                _currentToast?.Close();

                var toast = new Toast(message, type, mode, duration, maxWidth, toastPosition);
              // CHỈ gán Owner nếu MainWindow đã được hiển thị
                if (Application.Current.MainWindow != null && Application.Current.MainWindow.IsVisible)
            {
                toast.Owner = Application.Current.MainWindow;
            }

                _activeToasts.Add(toast);
                toast.Closed += (s, e) => _activeToasts.Remove(toast);

                _currentToast = toast;
                toast.Show();

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
            }, DispatcherPriority.ApplicationIdle);
        }

        public void CloseAllToasts()
        {
            Application.Current?.Dispatcher.Invoke(() =>
            {
                foreach (var toast in _activeToasts.ToArray())
                {
                    toast.Close();
                }
                _activeToasts.Clear();
                _currentToast = null;
            });
        }

        public void Disconnect()
        {
            CloseAllToasts();
        }
    }
}