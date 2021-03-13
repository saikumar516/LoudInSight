using System;
using System.Collections.Generic;
using System.Text;

namespace LoudInSight.Entities
{
    public interface IDatabaseSettings
    {
        List<string> CollectionNames { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
