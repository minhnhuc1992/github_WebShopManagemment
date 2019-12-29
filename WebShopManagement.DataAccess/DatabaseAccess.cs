using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebShopManagement.DataAccess
{
    public class DatabaseAccess
    {
        SqlConnection con;
        public DatabaseAccess()
        {
            var configuration = GetConfiguration();
            con = new SqlConnection(configuration.GetSection("Data").GetSection("NhucConnectionString").Value);
        }
        public IConfigurationRoot GetConfiguration()
        {
            var builer = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builer.Build();
        }
    }
}
