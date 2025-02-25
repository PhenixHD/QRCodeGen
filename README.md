# QR Code & Barcode Generator

## Overview
This is a **WPF application** built using **.NET 9** that allows users to generate both **QR codes** and **barcodes** from text input. The application supports switching dynamically between **QR code mode** and **barcode mode**.

## Features
- Generate **QR codes** using QRCoder  
- Generate **barcodes** (Code128) using BarcodeStandard and SkiaSharp  
- Toggle between QR code and barcode modes  
- Validate input to prevent crashes when generating barcodes  
- Display generated codes within the application  

## Technologies Used
- **.NET Version:** .NET 9
- **Project Type:** WPF (Windows Presentation Foundation)
- **UI Framework:** XAML with custom styling
- **QR Code Library:** [QRCoder](https://github.com/codebude/QRCoder)
- **Barcode Library:** [BarcodeStandard](https://github.com)
- **Graphics Library:** [SkiaSharp](https://github.com/mono/SkiaSharp)

## How to Run
```sh
git clone https://github.com/PhenixHD/QRCodeGen.git
cd QRCodeGen
dotnet restore
dotnet run
```

## Usage
1. Enter text in the input field.
2. Click **Generate** to create a QR code or barcode.
3. Use the **toggle button** to switch between QR code and barcode generation.
4. The application prevents invalid barcode input to avoid errors.

## Customization
- Edit the `MainWindow.xaml` file for custom styles and colors.  
- Change `WindowStyle` and `AllowsTransparency` for a modern look.  
- Use [MaterialDesignInXAML](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit) or [MahApps.Metro](https://github.com/MahApps/MahApps.Metro) for pre-built UI components.  

## License
This project is open-source under the **MIT License**.

## Contributions
Contributions are welcome! Feel free to **fork** this repository and submit **pull requests**.

## Contact
For questions or feature requests, open an **issue** or reach out on GitHub.

## Repository
🔗 **GitHub Repository:** [QRCodeGen](https://github.com/PhenixHD/QRCodeGen)

