using BarcodeStandard;
using QRCoder;
using SkiaSharp;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace QRCodeGen;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private bool isQRMode = true;
    public MainWindow() {
        InitializeComponent();
    }

    private void GenerateQRCode(object sender, RoutedEventArgs e) {
        if (!string.IsNullOrWhiteSpace(textboxInput.Text)) {
            if (isQRMode) {
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(textboxInput.Text, QRCodeGenerator.ECCLevel.Q))
                using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData)) {
                    byte[] qrCodeImage = qrCode.GetGraphic(50);

                    BitmapImage bitmap = new BitmapImage();
                    using (var ms = new MemoryStream(qrCodeImage)) {
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();
                        bitmap.Freeze();
                    }

                    imageQRCode.Source = bitmap;
                }
            } else {

                foreach (char c in textboxInput.Text) {
                    if (!char.IsDigit(c))
                        return;
                }

                Barcode bc = new Barcode() {
                    IncludeLabel = true
                };
                SkiaSharp.SKImage barcodeImage = bc.Encode(BarcodeStandard.Type.Code128, textboxInput.Text);

                using (var data = barcodeImage.Encode(SKEncodedImageFormat.Png, 100))
                using (var ms = new MemoryStream(data.ToArray())) {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                    bitmap.Freeze();

                    imageQRCode.Source = bitmap;
                }
            }
        }
    }


    private void ChangeGenerationMode_Click(object sender, RoutedEventArgs e) {
        isQRMode = !isQRMode;

        if (isQRMode) {
            labelCurrentState.Content = "Current: QR-Mode";
        } else {
            labelCurrentState.Content = "Current: BAR-Mode";
        }
    }
}