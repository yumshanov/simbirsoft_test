using System;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace simbirsoft_test
{
    
    public class ContentParser:IContentParser
    {
        private ILogger Logger;
        
        public ContentParser(ILogger _logger)
        {
            Logger = _logger;
            
        }
        public void Parse(string url)
        {
            try
            {
                Logger.Log(new LogMessage { Message = String.Format("Обработка адреса {0}", url)});
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (String.IsNullOrWhiteSpace(response.CharacterSet))
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                    string data = readStream.ReadToEnd();

                    data = Regex.Replace(data, @"<script[^>]*>[\s\S]*?</script>", string.Empty);
                    data = Regex.Replace(data, @"<style[^>]*>[\s\S]*?</style>", string.Empty);
                    data = Regex.Replace(data, @"<[^>]*>", string.Empty).ToUpper();

                    char[] delimiterChars = { ' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t' };
                    string[] words = data.Split(delimiterChars);
                    var result = words.GroupBy(x => x)
                              .Select(x => new { Word = x.Key, Frequency = x.Count() })
                              .Where(x => x.Word.Length > 0)
                              .OrderByDescending(x => x.Frequency);

                    Logger.Log(new LogMessage { Message = String.Format("Обработка адреса {0} завершена. Результаты обработки:", url) });
                    string msg;
                    foreach (var item in result)
                    {
                        msg = String.Format("Слово: {0}\tКоличество повторов: {1}", item.Word, item.Frequency);
                        Console.WriteLine(msg);
                        Logger.Log(new LogMessage { Message = msg });
                    }
                    Console.ReadKey();
                    response.Close();
                    readStream.Close();
                }
                else
                    Logger.Log(new LogMessage { Message = String.Format("Не удалось обработать адрес {0}. " +
                                                                            "Попробуйте выполнить запрос позднее.", url) });
                
            }
            catch (Exception ex)
            {
                Logger.Log(new LogMessage{Message = String.Format("Ошибка при обработке адреса {0}", url), Exception = ex });
            }
        }

    }
}
