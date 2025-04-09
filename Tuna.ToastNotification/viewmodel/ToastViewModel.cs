using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tuna.ToastNotification.ViewModel
{
    public enum ToastType
    {
        Info,
        Success,
        Warning,
        Error
    }
    public enum ToastStyleMode
    {
        OnlyIconColor,
        FullColor
    }
    public enum ToastPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    class ToastViewModel : INotifyPropertyChanged
    {
        private string _message;
        private ToastType _type;
        private Brush _background;
        private string _icon;
        private Brush _iconColor;
        private int _duration;
        private ToastStyleMode _styleMode;
        private double _maxWidth = 400;
        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(nameof(Message)); }
        }

        public ToastType Type
        {
            get => _type;
            set
            {
                _type = value;
                UpdateStyleByType();
                OnPropertyChanged();
            }
        }

        public Brush Background
        {
            get => _background;
            set { _background = value; OnPropertyChanged(); }
        }

        public string Icon
        {
            get => _icon;
            set { _icon = value; OnPropertyChanged(); }
        }
        public Brush IconColor
        {
            get => _iconColor;
            set { _iconColor = value; OnPropertyChanged(); }
        }

        public int Duration
        {
            get => _duration;
            set { _duration = value; OnPropertyChanged(); }
        }
        public ToastStyleMode StyleMode
        {
            get => _styleMode;
            set { _styleMode = value; UpdateStyleByType(); OnPropertyChanged(); }
        }
        public double MaxWidth
        {
            get => _maxWidth;
            set { _maxWidth = value; OnPropertyChanged(); }
        }
        public ToastViewModel(string message, ToastType type = ToastType.Info, ToastStyleMode styleMode = ToastStyleMode.OnlyIconColor, int duration = 2500, double maxWidth = 400)
        {
            Message = message;
            Type = type;
            StyleMode = styleMode;
            Duration = duration;
            MaxWidth = maxWidth;
        }

        private void UpdateStyleByType()
        {
            // Default values
            Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF323232"));
            IconColor = Brushes.White;

            switch (Type)
            {
                case ToastType.Success:
                    Icon = "✔";
                    IconColor = Brushes.LimeGreen;
                    if (StyleMode == ToastStyleMode.FullColor)
                        Background = new SolidColorBrush(Color.FromRgb(56, 142, 60));
                    break;
                case ToastType.Error:
                    Icon = "✖";
                    IconColor = Brushes.Red;
                    if (StyleMode == ToastStyleMode.FullColor)
                        Background = new SolidColorBrush(Color.FromRgb(211, 47, 47));
                    break;
                case ToastType.Warning:
                    Icon = "⚠";
                    IconColor = Brushes.Orange;
                    if (StyleMode == ToastStyleMode.FullColor)
                        Background = new SolidColorBrush(Color.FromRgb(255, 160, 0));
                    break;
                case ToastType.Info:
                default:
                    Icon = "ℹ";
                    IconColor = Brushes.DeepSkyBlue;
                    if (StyleMode == ToastStyleMode.FullColor)
                        Background = new SolidColorBrush(Color.FromRgb(33, 150, 243));
                    break;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
