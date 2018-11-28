using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaitlynTextStats
{
    class Program
    {
        static void Main(string[] args)
        {
            //Attempted to make this readable. Proceed at your own risk

            string XMLFile = @"C:\Users\Mark\Documents\Visual Studio 2015\Projects\CaitlynTextStats\CaitlynTextStats\TextData.xml";
            //this is the data backup's XML file

            string fileWritten = @"C:\Users\Mark\Documents\Visual Studio 2015\Projects\CaitlynTextStats\CaitlynTextStats\OrderedTextData.csv";
            //this is the output file for the text data

            string parsedXML = @"C:\Users\Mark\Documents\Visual Studio 2015\Projects\CaitlynTextStats\CaitlynTextStats\CaitlynTextData.xml";
            //this is the file created by the SwitchXML() function

            string parsedXMLDate = "";
            //This is the file location that I used for the date file. Not automatically created, but a renamed file.

            string phoneNumber = "address=\"+19139401536\"";
            //the target phone number for the data



            
            //XML taken from phone by https://play.google.com/store/apps/details?id=com.riteshsahu.SMSBackupRestore&hl=en_US

            SwitchXML(XMLFile, phoneNumber, parsedXML);
            //Narrows code down to 


            CreateOrderedFile(fileWritten, parsedXML);

            Console.Read();
             }

        public static void SwitchXML(string XMLFile, string phoneNumber, string parsedXML)
        {
            List<String> caitlyn = new List<string>();
            List<String> mark = new List<string>();

            
            string[] lines = System.IO.File.ReadAllLines(XMLFile);
            
            foreach (string line in lines)
            {
                if (line.Trim().Split(' ')[2].Equals(phoneNumber))
                {

                    caitlyn.Add(line.Trim().Split('=')[6]);
                    //caitlyn.Add(line.Trim().Split('=')[3].Substring(1, 13));
                    //The above line collates date data (miliseconds from epoch) in "parsedXMLDate"
                }
            }

            Console.WriteLine("File Created");
            System.IO.File.WriteAllLines(parsedXML, caitlyn);

        }

        public static List<Word> CreateWordObjectList(string parsedXML)
        {
            List<Word> words = new List<Word>();
            words.Add(new Word("the"));
            

            string[] lines = System.IO.File.ReadAllLines(parsedXML);
            //string[] lines = System.IO.File.ReadAllLines(parsedXMLDate);
            //Above Line Tracks Date


            Console.WriteLine("Collecting words");
            float count = 0;
            float lineNum = lines.Count();
            foreach (String line in lines)
                
            {
                count += 1;
                float ratio = 100 * count * (1 / lineNum);
                Console.WriteLine($"% line done = {ratio:#.0}");



                foreach (String word in line.Split(' '))
                //foreach (String word in line.Split(','))
                //Above Line Tracks Date
                {
                    bool wordIncluded = false;
                    string testWord = RemoveChars(word);


                    foreach (Word obj in words.ToArray())
                    {
                        
                        if (obj.Text == testWord)
                        {
                            obj.Frequency += 1;
                            wordIncluded = true;
                        }

                    }
                    if (!wordIncluded)
                    {
                        words.Add(new Word(testWord));

                    }
                }

                
            }

            return words;
        }

        public static String RemoveChars(String word)
        {
            word = word.Replace(",", "").Replace('"', '!').Replace("!", "").Replace(".", "").Replace("*", "").Replace("(", "").Replace(")", "").Replace("?", "").Replace("?", "").Replace("?", "").ToLower();
            return word;
        }

        public static void CreateOrderedFile(string fileWritten, string parsedXML)
        {

            

            List<Word> words = CreateWordObjectList(parsedXML);

            List<Word> orderdWords = words.OrderByDescending(o => o.Frequency).ToList();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileWritten))
            {
                foreach (Word word in words)
                {
                    

                    file.WriteLine(word);
                }
                Console.WriteLine("Ordered File Created");
            }
        }

        public static string Reverse(string text)
        {
            //depriceated element for tracking date. Yuck.
            char[] cArray = text.ToCharArray();
            string reverse = String.Empty;
            for (int i = cArray.Length - 1; i > -1; i--)
            {
                reverse += cArray[i];
            }
            return reverse;
        }
    }



    
}
