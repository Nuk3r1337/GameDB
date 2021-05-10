using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace GameDB.Dto.DataClasses
{
    public interface IConfig
    {
        string ApiUrl { get; set; }
    }
    public class Config : IConfig
    {
        public string ApiUrl { get; set; } = "UwU";
    }
}
