using System.Collections.Generic;
using RestSharp.Serializers;

namespace Checkmarx.Soccer.FootballData.Models
{
    public class Standing
    {
        public string Group { get; set; }
        [SerializeAs(Name = "table")]
        public IList<TableItem> Table { get; set; }
    }
}