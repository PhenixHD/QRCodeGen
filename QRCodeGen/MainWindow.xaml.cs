using BarcodeStandard;
using Microsoft.Win32;
using QRCoder;
using SkiaSharp;
using System.IO;
using System.Windows;
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

    private void SaveQRCode_Click(object sender, RoutedEventArgs e) {

        if (imageQRCode.Source is BitmapSource bitmapSource) {
            SaveFileDialog saveFileDialog = new SaveFileDialog {
                Filter = "PNG Image|*.png|JPEG Image|*.jpg",
                Title = "Save QR Code or Barcode",
                FileName = "QRCode"
            };

            if (saveFileDialog.ShowDialog() == true) {
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create)) {
                    BitmapEncoder encoder;

                    if (Path.GetExtension(saveFileDialog.FileName).ToLower() == ".jpg") {
                        encoder = new JpegBitmapEncoder();
                    } else {
                        encoder = new PngBitmapEncoder();
                    }

                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                    encoder.Save(stream);
                }
            }
        } else {
            MessageBox.Show("No image to save.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}