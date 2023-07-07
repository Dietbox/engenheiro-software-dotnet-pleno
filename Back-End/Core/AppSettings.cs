using Dietbox.ECommerce.Core.DTO.AppSettings;
using Dietbox.ECommerce.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;


namespace Dietbox.ECommerce.Core
{
    public class AppSettings : ISettings
    {

        public string CurrentDirectory { get; }
        public string CurrentBaseURL { get; }
        public string Environment { get; }
        public string ConnectionString { get; }  

        public AppSettings_GoogleRecaptcha Recaptcha { get; }

        public AppSettings_JsonWebToken JWT { get; }



        public AppSettings(IConfiguration configuration)
        {
            CurrentDirectory = Directory.GetCurrentDirectory();
            CurrentBaseURL = null;
            Environment = configuration.GetSection("Environment").Value;
            ConnectionString = configuration.GetSection("ConnectionString").Value;
        


            IConfigurationSection recaptchaSection = configuration.GetSection("GoogleRecaptcha");
            Recaptcha = new()
            {
                Enabled = bool.Parse(recaptchaSection["Enabled"]),
                API = recaptchaSection["API"],
                Secret = recaptchaSection["Secret"],
            };

            IConfigurationSection jwtSection = configuration.GetSection("JsonWebToken");
            JWT = new()
            {
                Key = jwtSection["Key"],
                HoursToExpire = int.Parse(jwtSection["HoursToExpire"])
            };

        }

        //public AppSettings(IHttpContextAccessor httpContext, IConfiguration configuration)
        //{
        //    CurrentDirectory = Directory.GetCurrentDirectory();
        //    CurrentBaseURL = $"https://{httpContext.HttpContext.Request.Host.Value}";
        //    Environment = configuration.GetSection("Environment").Value;
        //}

    }
}
