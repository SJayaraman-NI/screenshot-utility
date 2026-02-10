using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            //string processName = "mspaint";
            string processName = "InstrumentStudio";
            string outputPath = @"C:\Temp\Screenshot.png";

            ScreenshotUtility.CaptureToFile(processName, outputPath);

            Console.WriteLine("Screenshot captured successfully:");
            Console.WriteLine(outputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to capture screenshot:");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
