# Tuna.ToastNotification

Thư viện hiển thị thông báo toast đơn giản và dễ sử dụng cho ứng dụng WPF.

## Tính năng

- Hiển thị thông báo toast với 4 kiểu khác nhau:
  - Info (Thông tin)
  - Success (Thành công)
  - Warning (Cảnh báo)
  - Error (Lỗi)
- Hai chế độ hiển thị:
  - OnlyIconColor: Chỉ màu icon
  - FullColor: Màu đầy đủ
- Tùy chỉnh vị trí hiển thị:
  - TopLeft (Góc trên trái)
  - TopCenter (Giữa trên)
  - TopRight (Góc trên phải)
  - BottomLeft (Góc dưới trái)
  - BottomCenter (Giữa dưới)
  - BottomRight (Góc dưới phải)
- Tùy chỉnh thời gian hiển thị (mặc định: 2500ms)
- Tùy chỉnh độ rộng tối đa (mặc định: 500px)

## Cài đặt

```powershell
Install-Package Tuna.ToastNotification
```

## Sử dụng

```csharp
using Tuna.ToastNotification.Services;

// Hiển thị thông báo thông tin
ToastService.ShowToast("Đây là thông báo thông tin");

// Hiển thị thông báo thành công
ToastService.ShowToast("Thao tác thành công!", ToastType.Success);

// Hiển thị thông báo cảnh báo
ToastService.ShowToast("Cảnh báo!", ToastType.Warning);

// Hiển thị thông báo lỗi
ToastService.ShowToast("Có lỗi xảy ra!", ToastType.Error);

// Tùy chỉnh thời gian hiển thị (5 giây)
ToastService.ShowToast("Thông báo", duration: 5000);

// Tùy chỉnh vị trí hiển thị
ToastService.ShowToast("Thông báo", toastPosition: ToastPosition.BottomRight);

// Tùy chỉnh chế độ hiển thị
ToastService.ShowToast("Thông báo", mode: ToastStyleMode.FullColor);

// Tùy chỉnh độ rộng tối đa
ToastService.ShowToast("Thông báo", maxWidth: 300);
```

## Yêu cầu

- .NET Framework 4.8
- WPF

## Giấy phép

MIT License
