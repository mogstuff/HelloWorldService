using System;
using System.Timers;
using Topshelf;

namespace HelloWorldService
{
    class Program
    {
        static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>                                  
            {
                x.Service<HelloService>(s =>                                 
                {
                    s.ConstructUsing(name => new HelloService());             
                    s.WhenStarted(tc => tc.Start());                        
                    s.WhenStopped(tc => tc.Stop());                         
                });
                x.RunAsLocalSystem();                                     

                x.SetDescription("This service writes to a log file every few seconds");                 
                x.SetDisplayName("Hello World Service");                
                x.SetServiceName("HelloWorldService");

                x.EnableServiceRecovery(recoveryOption =>
                {
                    recoveryOption.RestartService(5); // wait5 minutes then restart service
                    recoveryOption.RestartComputer(60, "wait an hour then reboot");
                    recoveryOption.RunProgram(30,
                        @"c:\snafu.exe"); // run this program after 30 minutes
                });

                x.StartAutomatically();

                x.EnablePauseAndContinue();

            });

       

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  
            Environment.ExitCode = exitCode;

        }


    }
}
