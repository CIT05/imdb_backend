using DataLayer.TitlePrincipals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Bookmarkings
{
    public interface IBookmarkingDataService
    {
        TitleBookmarking? AddTitleBookmarking(int userid, string titleId);
        TitleBookmarking? DeleteTitleBookmarking(int userid, string titleId);
        List<TitleBookmarking> GetTitlesBookmarkedByUsers(int userId);

        PersonalityBookmarking? AddPersonalityBookmarking(int userid, string Nconst);
        PersonalityBookmarking? DeletePersonalityBookmarking(int userid, string Nconst);
        List<PersonalityBookmarking> GetPersonalitiesBookmarkedByUser(int userId);

    }
}
