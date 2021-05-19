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
    }
    public class AppSettings : IAppSettings
    {
        public string ApiUrl { get; set; } = "UwU";
    }
}
