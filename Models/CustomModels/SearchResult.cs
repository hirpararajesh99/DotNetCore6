using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public class SearchResult<T> : ISearchResult<T>
    {
        public IList<T> Results { get; set; }
        public Meta Meta { get; set; }
    }
}
