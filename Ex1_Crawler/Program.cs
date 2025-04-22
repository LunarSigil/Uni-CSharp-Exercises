using System.Text.RegularExpressions;

namespace Crawler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentNullException();
            }

            Uri siteUri;
            try
            {
                siteUri = new Uri(args[0]);
            }
            catch
            {
                throw new ArgumentException("Incorrect url");
            }

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(siteUri);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error while loading page");
            }

            string result = await response.Content.ReadAsStringAsync();

            Regex emailRegex = new Regex("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");

            if (!emailRegex.IsMatch(result))
            {
                throw new Exception("No email addresses found");
            }

            HashSet<string> uniqueEmails = new();
            foreach (Match match in emailRegex.Matches(result))
            {
                uniqueEmails.Add(match.Value);
            }

            foreach (string email in uniqueEmails)
            {
                Console.WriteLine(email);
            }

        }
    }
}