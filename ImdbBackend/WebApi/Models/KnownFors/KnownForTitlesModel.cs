using System.Text.Json.Serialization;
using WebApi.Models.Titles;

namespace WebApi.Models.KnownFors
{
    public class KnownForTitlesModel
    {
        public string Url { get; set; }
        public TitlePosterDTO Title { get; set; }


    }
}
