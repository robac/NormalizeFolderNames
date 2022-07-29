using System;
using System.Windows.Forms;
using Serilog;

namespace RenameRecursivelly
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(Utils.Utils.getLogFilename(),
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true)
            .CreateLogger();
            Log.Information("App started.");

            try
            {
                Application.Run(new MainForm());
            } 
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                Log.Fatal(ex.StackTrace);
            }
            finally {
                Log.CloseAndFlush();
            }
        }
    }
}