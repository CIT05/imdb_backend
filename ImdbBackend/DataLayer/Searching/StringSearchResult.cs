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

public class ExactSearchResult
{
    public string TConst { get; set; } = string.Empty;

    public Title? Title { get; set; }
}

public class BestSearchResult
{

    public string TConst { get; set; } = string.Empty;

    public Title? Title { get; set; }

    public int MatchCount { get; set; }
}