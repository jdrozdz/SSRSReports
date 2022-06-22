namespace Example;

public static class SSRSHelper
{
        public static Dictionary<string, string> ContentTypes => new Dictionary<string, string>()
        {
            { "PDF", "application/pdf" },
            { "Excel", "application/vnd.ms-excel" },
            { "CSV", "text/csv" }
        };

        public static Dictionary<string, string> FileExtensions => new Dictionary<string, string>()
        {
            { "PDF", "pdf" },
            { "Excel", "xls" },
            { "CSV", "csv" }
        };
        
        public static Task<byte[]> GetReport(T requestModel)
        {
            SSRSConfigurationModel config = ConfigurationHelper.LoadSSRSConfiguration();
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(config.Username, config.Password, config.Domain)
            };
            HttpClient client = new HttpClient(clientHandler);
            var data = await client.GetByteArrayAsync($"{config.Url}/{config.Catalog}/Pages/ReportViewer.aspx?%2f<CATALOG>%2f<REPORT_NAME>&<ARGS_LIST>&rs:Format={format}&rs:Command=Export");
            
            var folderName = reportRequest.ReportDate.ToString("MMMMyyyy");
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/" + folderName);
            } else
            {
                Directory.Delete($@".\{folderName}", true);
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/" + folderName);
            }

            if (File.Exists($@"{folderName}.zip"))
            {
                File.Delete($@".\{folderName}.zip");
            }
            
            string u = Directory.GetCurrentDirectory() + "/" + folderName;
            using var s = File.Create(u + "/report.pdf");
            await s.WriteAsync(f.Data);
            
            ZipFile.CreateFromDirectory(@".\" + folderName, $@".\{folderName}.zip");
            var zip = await File.ReadAllBytesAsync($@".\{folderName}.zip");
            
            Directory.Delete($@".\{folderName}", true);
            File.Delete($@".\{folderName}.zip");
        }
}
