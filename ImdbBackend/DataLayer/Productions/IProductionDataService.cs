using DataLayer.KnownFors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Productions
{
    public interface IProductionDataService
    {
        List<Production> GetProductionsByTitleIds(List<string> titleIds);
    }
}
