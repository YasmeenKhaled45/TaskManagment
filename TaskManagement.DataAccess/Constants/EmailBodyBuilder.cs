using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Constants
{
    public static class EmailBodyBuilder
    {
        public static string GenerateEmailBody(string templete, Dictionary<string, string> templeteModel)
        {
            var templetepath = Path.Combine(Directory.GetCurrentDirectory(), "Templetes", $"{templete}.html");

            if (!File.Exists(templetepath))
            {
                throw new FileNotFoundException($"The template file '{templetepath}' was not found.");
            }
            var streamreader = new StreamReader(templetepath);
            var body = streamreader.ReadToEnd();
            streamreader.Close();

            foreach (var item in templeteModel)
                body = body.Replace(item.Key, item.Value);

            return body;
        }
    }
}
