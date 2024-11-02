using DataLayer.Persons;
using DataLayer.Titles;
using System.Text.Json.Serialization;


namespace DataLayer.KnownFors
{
    public class KnownFor
    {
        public string NConst { get; set; }
        public string TConst { get; set; }
        public int Ordering { get; set; }

        [JsonIgnore] 
        public  Person Person { get; set; }
        [JsonIgnore]
        public Title Title { get; set; }
    }
}
