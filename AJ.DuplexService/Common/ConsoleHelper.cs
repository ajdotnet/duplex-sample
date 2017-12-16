using System;
using System.Globalization;
using System.Threading;

namespace AJ.DuplexService.Common
{
    /// <summary>Small helper for colored console output.</summary>
    public static class ConsoleHelper
    {
        private static object _lock = new object();
        private static int _contextWidth = 0;
        private static ConsoleColor _foregroundColor = Console.ForegroundColor;
        private static ConsoleColor _backgroundColor = Console.BackgroundColor;

        public static void WriteException(Exception exception)
        {
            Guard.AssertNotNull(exception, nameof(exception));
            lock (_lock)
            {
                WriteTime();
                WriteLine(ConsoleColor.Red, exception.Message);
                WriteLine(ConsoleColor.DarkYellow, exception.ToString());
            }
        }

        public static void WriteLine(this object context, ConsoleColor color, string message)
        {
            Guard.AssertNotNull(context, nameof(context));
            var c = context.GetType().Name;
            _contextWidth = Math.Max(_contextWidth, c.Length);
            c = c.PadRight(_contextWidth);
            WriteLine(color, c + ": " + message);
        }

        public static void WriteLine(ConsoleColor color, string line)
        {
            lock (_lock)
            {
                WriteTime();
                Console.ForegroundColor = color;
                Console.WriteLine(line);
                RevertColor();
            }
        }

        private static void WriteTime()
        {
            var time = DateTime.Now.ToString("HH:mm:ss.ffff", CultureInfo.InvariantCulture) + "   ";
            Console.Write(time);
        }

        public static void WaitForExit()
        {
            Console.WriteLine();
            WriteLine(ConsoleColor.Magenta, "Press <ESC> to exit.");

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                    break;
                if (key.Key == ConsoleKey.Delete)
                    Console.Clear();
                Thread.Sleep(100);
            }
        }

        private static void RevertColor()
        {
            Console.ForegroundColor = _foregroundColor;
            Console.BackgroundColor = _backgroundColor;
        }
    }
}