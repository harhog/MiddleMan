
# MiddleMan 🖱️

MiddleMan is a lightweight background utility that brings the seamless "Primary Selection" experience from Linux to Windows. It eliminates the need for manual Ctrl+C and Ctrl+V by automating the copy-paste workflow using your mouse.

✨ Select to Copy, Middle-click to Paste.

## ✨ Features

MiddleMan runs silently in your system tray and enhances your productivity with:

*    **Automatic Copy: Simply highlight any text with your left mouse button, and it's instantly added to your clipboard.**

 *   **Smart Middle-Click Paste: Paste your selection anywhere with a single middle-click.**

 *  **Auto-Focus Injection: Automatically focuses the window or text field under your cursor before pasting, removing the need for an initial left-click.**

 *   **Lightweight & Standalone: Built in C#, compiled as a single .exe with no external dependencies or .NET installation required.**

 *   **Tray Integration: Easily manage or exit the tool from the Windows system tray.**




### 🚀 Getting Started
Prerequisites

*    **Windows 10 or 11.**

*    **Note: For the tool to work in "protected" windows like Task Manager or Administrative Terminals, you should run MiddleMan as Administrator.**

**Installation & Usage**

*    Download the Release Go to the Releases page and download MiddleMan.exe.

*    Run the Program Simply double-click the file. An icon will appear in your system tray indicating that MiddleMan is active.

*    (Optional) Add to Startup To have the tool start automatically with Windows:

*    Press Win + R, type shell:startup, and hit Enter.

*    Create a shortcut to MiddleMan.exe in that folder.

## 🛠️ Building from Source

If you want to build the standalone executable yourself:
PowerShell

# Clone the repository
git clone https://github.com/harhog/middleman.git
cd middleman

# Build a standalone, single-file executable
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:DebugType=None



## ❤️ Support & Commercial Use

MiddleMan is open-source and free for personal use. If this tool saves you time in your daily workflow, please consider supporting the project.

If you are using this tool in a **commercial or business environment**, I kindly ask you to support the project.

* [Sponsor me on GitHub](https://github.com/sponsors/harhog)
* [Buy Me A Coffee](https://buymeacoffee.com/haraldhogli)
* [Donate via PayPal](https://paypal.me/haraldhog)

<p align="center">
  <a href="https://github.com/sponsors/harhog">
    <img alt="Sponsor on GitHub" src="https://img.shields.io/badge/GitHub%20Sponsors-Sponsor-ea4aaa?style=for-the-badge&logo=githubsponsors&logoColor=white">
  </a>
  <a href="https://buymeacoffee.com/haraldhogli">
    <img alt="Buy Me A Coffee" src="https://img.shields.io/badge/Buy%20Me%20a%20Coffee-Support-FFDD00?style=for-the-badge&logo=buymeacoffee&logoColor=000">
  </a>
  <a href="https://paypal.me/haraldhog">
    <img alt="Donate via PayPal" src="https://img.shields.io/badge/PayPal-Donate-00457C?style=for-the-badge&logo=paypal&logoColor=white">
  </a>
</p>


## 📝 License
This project is licensed under the MIT License. See the LICENSE file for the full text.

Created with 🦀 by Harald Höglund
