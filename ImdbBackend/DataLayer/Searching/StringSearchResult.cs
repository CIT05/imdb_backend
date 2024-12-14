using DataLayer.Persons;
using DataLayer.Titles;

namespace DataLayer.Searching;

public class TitleStringSearchResult
{
    public string TitleId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}

public class ActorStringSearchResult
{
    public string ActorId { get; set; } = string.Empty;
    public string ActorName { get; set; } = string.Empty;
}

public class ArtistStringSearchResult
{
    public string Nconst { get; set; } = string.Empty;
    public Person? Person { get; set; }

    public decimal? Likelihood { get; set; }
}

public class ExactSearchResult
{
    public List<ArtistStringSearchResult> Persons { get; set; } = new List<ArtistStringSearchResult>();

    public List<TitleExactSearchResult> Titles { get; set; } = new List<TitleExactSearchResult>();
}

public class BestSearchResult
{
    public List<ArtistStringSearchResult> Persons { get; set; } = new List<ArtistStringSearchResult>();

    public List<TitleBestSearchResult> Titles { get; set; } = new List<TitleBestSearchResult>();
}


public class TitleExactSearchResult
{
    public string TConst { get; set; } = string.Empty;

    public Title? Title { get; set; }
}

public class TitleBestSearchResult
{

    public string TConst { get; set; } = string.Empty;

    public Title? Title { get; set; }

    public int MatchCount { get; set; }
}