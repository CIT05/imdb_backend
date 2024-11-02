namespace DataLayer.TitleEpisodes
{
    public interface ITitleEpisodeDataService
    {
        List<TitleEpisode> GetTitleEpisodes(int pageSize, int pageNumber);
        TitleEpisode? GetTitleEpisode(string tconst);
        int NumberOfTitleEpisodes();
    }
}