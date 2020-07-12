using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;

namespace DocumentSearchEngine.Models
{
    public class FileDataExtractor
    {
        public static string Extract(string path)
        {
            var reader = new PdfReader(path);
            var sb = new StringBuilder();
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                var streamBytes = reader.GetPageContent(i);
                var tokenizer = new PrTokeniser(new RandomAccessFileOrArray(streamBytes));

                while (tokenizer.NextToken())
                {
                    if (tokenizer.TokenType == PrTokeniser.TK_STRING)
                    {
                        var currentText = tokenizer.StringValue;
                        currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                        sb.Append(tokenizer.StringValue+ " ");
                    }
                }
            }
            return sb.ToString();
        }
    }
}
