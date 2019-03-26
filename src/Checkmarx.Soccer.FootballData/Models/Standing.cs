using System.Collections.Generic;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace Checkmarx.Soccer.FootballData.Models
{
    public class Standing
    {
        public string Group { get; set; }
        [DeserializeAs(Name = "table")]
        public IEnumerable<TableItem> Table { get; set; }
    }
}