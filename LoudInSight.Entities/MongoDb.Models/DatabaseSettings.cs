using System;
using System.Collections.Generic;
using System.Text;

namespace LoudInSight.Entities
{
    public class DatabaseSettings: IDatabaseSettings
    {
        public List<string> CollectionNames { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
