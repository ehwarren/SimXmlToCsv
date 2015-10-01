using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimXmlToCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SimXmlToCsv Version " + Constants.version + "." + Constants.revision);
            Console.WriteLine("This will create a csv file for EVERY xml file in this directory");

            //Search for the files...
            FileMan fileManager = new FileMan();
            Console.WriteLine("Found Files: ");
            fileManager.echoFileNames();

            //Parse each of the files...
            fileManager.parseFiles();
            Console.Write("Press the anykey to exit...");
            Console.ReadKey();
        }
    }
}
