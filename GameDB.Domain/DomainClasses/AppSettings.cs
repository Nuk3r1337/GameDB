using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace GameDB.Domain.DomainClasses
{
    public interface IAppSettings
    {
        string ApiUrl { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
    }
    public class AppSettings : IAppSettings
    {
        public string ApiUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
