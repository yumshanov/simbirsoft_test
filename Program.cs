using System;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;


namespace simbirsoft_test
{
    class Program
    {
        static void Main(string[] args)
        {
            string urlAddressDefault = "http://simbirsoft.ru";
            Console.WriteLine("Введите адрес сайта для подсчета уникальных слов[http://simbirsoft.ru]");
            string urlAddress = Console.ReadLine();
            urlAddress = (urlAddress.Length == 0) ? urlAddressDefault : urlAddress;
            var parser = new ContentParser(new FileLogger());
            parser.Parse(urlAddress);
        }
    }
}
