using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace schedule_set_up_app.form;


internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        // Tạo một "Văn hóa" (Culture) mới là Tiếng Việt
        CultureInfo ci = new CultureInfo("vi-VN");

        // Cài đặt nó làm văn hóa mặc định cho luồng (thread) này
        Thread.CurrentThread.CurrentCulture = ci;
        Thread.CurrentThread.CurrentUICulture = ci;
    Form1: Application.Run(new Form1());
    }
}