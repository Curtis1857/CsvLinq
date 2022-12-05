using Microsoft.VisualBasic.FileIO;

namespace CsvLinq
{
    public class CsvContext
    {
        protected CsvContext() { }
        protected CsvContext(string filename)
        {
            using (var allEbayListingsParser = new TextFieldParser(filename))
            { 
            }
        }
        public  CsvContext(Stream stream)
        {
            var delimiter = ",";
            using (var file = new TextFieldParser(stream))
            {
                file.SetDelimiters(new string[] { "," });
                file.HasFieldsEnclosedInQuotes = true;

                string[] headers = file.ReadFields();
            }
        }

    }

    
}