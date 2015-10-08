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
            char input;
            do
            {
                Console.WriteLine("Would you like to output all results into one csv file? (y/n)");
                input = Console.ReadKey().KeyChar;
            }
            while (input != 'y' && input != 'n');
            if (input == 'y')
            {
                fileManager.parseFilesInOne();
            }
            else
            {
                do
                {
                    Console.WriteLine("Would you like to output only the differences? (y/n)");
                    input = Console.ReadKey().KeyChar;
                }
                while (input != 'y' && input != 'n');
                fileManager.parseFiles(input == 'y');
            }
           
            Console.Write("Press the anykey to exit...");
            Console.ReadKey();
        }
    }
}
