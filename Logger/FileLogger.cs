using System;
using System.IO;
using System.Reflection;

namespace simbirsoft_test
{
    public class FileLogger : ILogger
    {
        public void Log(ILogMessage LogMessage)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\log.txt", true))
                {
                    sw.WriteLine(String.Format("{0} {1}", DateTime.Now.ToString() + ":", LogMessage.Message));
                    if (LogMessage.Exception != null)
                    {
                        sw.WriteLine(String.Format("Exception: {0}", LogMessage.Exception));
                    }

                }
            }
            catch (Exception ex)
            { }


        }
    }
}
