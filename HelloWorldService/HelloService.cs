using System;
using System.IO;
using System.Timers;
using Topshelf.Logging;

namespace HelloWorldService
{
  public  class HelloService
    {
        readonly Timer _timer;

        private static readonly LogWriter _log = HostLogger.Get<HelloService>();



        public HelloService()
        {
            _timer = new Timer(5000) { AutoReset = true };
            _timer.Elapsed += WriteHello;            
        }

        private void WriteHello(object sender, ElapsedEventArgs e)
        {

            _log.InfoFormat($"Executing WriteHello command at {DateTime.Now}");

            // sample code shamelessly stolen from here:
            // https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-open-and-append-to-a-log-file

            string filePath = @"c:\temp\hello.txt";          
       
            using (StreamWriter w = File.AppendText(filePath))
            {
                Log("Test1", w);
                Log("Test2", w);
            }

        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }
               
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }

        public void Pause() 
        {
            _log.InfoFormat($"HelloWorldService Paused at {DateTime.Now}");
        }

        public void Continue() 
        {
            _log.InfoFormat($"HelloWorldService Continued at {DateTime.Now}");
        }

        public void CustomCommand(int commandNumber) 
        {
            _log.InfoFormat("Hey, I got the command number '{0}'", commandNumber);
        }

    }
}
