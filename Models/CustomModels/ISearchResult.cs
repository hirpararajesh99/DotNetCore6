using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    public interface ISearchResult<T>
    {
        Meta Meta { get; set; }
        IList<T> Results { get; set; }
    }
}
