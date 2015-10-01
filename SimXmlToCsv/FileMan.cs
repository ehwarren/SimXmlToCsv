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
        public String[] originalFile;
        public String[] fileList;

        public FileMan()
        {
            try
            {
                originalFile = Directory.GetFiles("./original/", "*.xml");
            }
            catch
            {
                Console.WriteLine("Could not open original xml file...");
            }
            try
            {
                fileList = Directory.GetFiles("./changes/", "*.xml");
            }
            catch
            {
                Console.WriteLine("Could not open changed xml file...");
            }
            
        }

        public void parseFiles()
        {
            XDocument orig = XDocument.Load(originalFile[0]);

            foreach (String fname in fileList)
            {
                Console.WriteLine("Parsing: " + fname);
                List<String> values = new List<String>();
                XDocument doc = XDocument.Load(fname);
                values.Add("ORIGINAL DATA,,,,,,,,CHANGED DATA");
                values.Add("shapeId, lugIndex, name, grade, price, volume,,shapeId, lugIndex, name, grade, price, volume,isDifferent");
                for (int j = 0; j < doc.Root.Elements().Count(); j++)
                {
                    bool isDifferent = false;
                    XElement orig_data = orig.Root.Elements().ElementAt(j);
                    XElement element = doc.Root.Elements().ElementAt(j);
                    string entry = "";
                    for (int i=0; i < element.Elements().Count(); i++)
                    {
                        try
                        {
                            //we only keep shapeId, lugIndex, name, grade, price and volume
                            if (element.Elements().ElementAt(i).Name.LocalName == "shapeId" || element.Elements().ElementAt(i).Name.LocalName == "lugIndex" || element.Elements().ElementAt(i).Name.LocalName == "name" || element.Elements().ElementAt(i).Name.LocalName == "grade" || element.Elements().ElementAt(i).Name.LocalName == "price")
                            {
                                if (element.Elements().ElementAt(i).Name.LocalName == "price" && orig_data.Elements().ElementAt(i).Value == "0.0")
                                    entry += ",,";
                                entry += (orig_data.Elements().ElementAt(i).Value + ",");
                            }
                            else if (element.Elements().ElementAt(i).Name.LocalName == "volume")
                            {
                                entry += (orig_data.Elements().ElementAt(i).Value + ",,");

                            }
                            //values.Add(el.Attribute("Value").Value.ToString());
                        }
                        catch { }
                    }
                    for (int i = 0; i < element.Elements().Count(); i++)
                    {
                        try
                        {
                            //we only keep shapeId, lugIndex, name, grade, price and volume
                            if (element.Elements().ElementAt(i).Name.LocalName == "shapeId" || element.Elements().ElementAt(i).Name.LocalName == "lugIndex" || element.Elements().ElementAt(i).Name.LocalName == "name" || element.Elements().ElementAt(i).Name.LocalName == "grade" || element.Elements().ElementAt(i).Name.LocalName == "price")
                            {
                                entry += (element.Elements().ElementAt(i).Value + ",");
                            }
                            else if (element.Elements().ElementAt(i).Name.LocalName == "volume")
                            {
                                entry += (element.Elements().ElementAt(i).Value);

                            }
                            if (element.Elements().ElementAt(i).Name.LocalName == "name")
                            {
                                if (element.Elements().ElementAt(i).Value != orig_data.Elements().ElementAt(i).Value)
                                    isDifferent = true;
                            }
                            //values.Add(el.Attribute("Value").Value.ToString());
                        }
                        catch { }
                    }
                    /*
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
                    */
                    if (isDifferent)
                        entry += ",1";
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
