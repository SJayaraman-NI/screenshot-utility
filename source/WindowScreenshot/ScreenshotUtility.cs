using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

public static class ScreenshotUtility
{
    public static void CaptureToFile(string processName, string filePath)
    {
        IntPtr hwnd = GetMainWindowHandle(processName);
        if (hwnd == IntPtr.Zero)
            throw new InvalidOperationException($"Window not found for process '{processName}'.");

        using Bitmap bmp = CaptureWindow(hwnd);
        bmp.Save(filePath, ImageFormat.Png);
    }

    private static Bitmap CaptureWindow(IntPtr hwnd)
    {
        // ✅ Correct way for WPF / DPI-aware apps
        if (DwmGetWindowAttribute(
                hwnd,
                DWMWA_EXTENDED_FRAME_BOUNDS,
                out RECT rect,
                Marshal.SizeOf<RECT>()) != 0)
        {
            throw new InvalidOperationException("Failed to get window bounds via DWM.");
        }

        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;

        if (width <= 0 || height <= 0)
            throw new InvalidOperationException($"Invalid window size {width}x{height}");

        Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

        using (Graphics g = Graphics.FromImage(bitmap))
        {
            IntPtr hdc = g.GetHdc();

            if (!PrintWindow(hwnd, hdc, PW_RENDERFULLCONTENT))
            {
                g.ReleaseHdc(hdc);
                throw new InvalidOperationException("PrintWindow failed.");
            }

            g.ReleaseHdc(hdc);
        }

        return bitmap;
    }

    private static IntPtr GetMainWindowHandle(string processName)
    {
        foreach (var p in Process.GetProcessesByName(processName))
        {
            if (p.MainWindowHandle != IntPtr.Zero)
                return p.MainWindowHandle;
        }
        return IntPtr.Zero;
    }

    // ---------------- Win32 ----------------

    private const int PW_RENDERFULLCONTENT = 0x00000002;
    private const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;

    [DllImport("user32.dll")]
    private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, int nFlags);

    [DllImport("dwmapi.dll")]
    private static extern int DwmGetWindowAttribute(
        IntPtr hwnd,
        int dwAttribute,
        out RECT pvAttribute,
        int cbAttribute);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
