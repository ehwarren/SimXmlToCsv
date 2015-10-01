using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace SimXmlToCsv
{
    class FileMan
    {
        public String[] fileList;

        public FileMan()
        {
            fileList = Directory.GetFiles(".", "*.xml");
        }

        public void parseFiles()
        {

            foreach (String fname in fileList)
            {
                Console.WriteLine("Parsing: " + fname);
                List<String> values = new List<String>();
                XDocument doc = XDocument.Load(fname);
                values.Add("shapeId,, lugIndex,, name,, grade,, price,, volume");
                foreach (XElement element in doc.Root.Elements())
                {
                    string entry = "";
                    foreach (XElement el in element.Elements())
                    {
                        try
                        {
                            //we only keep shapeId, lugIndex, name, grade, price and volume
                            if (el.Name.LocalName == "shapeId" || el.Name.LocalName == "lugIndex" || el.Name.LocalName == "name" || el.Name.LocalName == "grade" || el.Name.LocalName == "price")
                                entry += (el.Value + ",,");
                            else if (el.Name.LocalName == "volume")
                                entry += (el.Value);
                            //values.Add(el.Attribute("Value").Value.ToString());
                        }
                        catch { }
                    }
                    values.Add(entry);
                }
                writeCSV(values, fname);
                //write the new csv file  
            }
        }

        private void writeCSV(List<String> values, string fname)
        {
            File.WriteAllLines(fname + ".csv", values);
            Console.WriteLine("Succesfully wrote file: " + fname + ".csv");
        }
        public void echoFileNames()
        {
            foreach(String fname in fileList){
                Console.WriteLine(fname);
            }
        }
    }
}
