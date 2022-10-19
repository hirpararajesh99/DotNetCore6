using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Constant
{
    public class Constants
    {
        public static class ErrorMessages
        {
            public const string LoginFailed = "Login Failed";
            public const string InvalidGrantType = "Invalid Grant Types";
            public const string UnAuthorized = "Not Authorized";
            public const string NotFound = "Not Found";
            public const string UnAuthorizedToAccessTenant = "You are not authorized to view account details. Kinly contact your system administrator.";
            public const string IsInUsed = "Is In Use";
            public const string SortingNotAllowedOnUniqueKey = "Sorting is not allowed on the unique key or json field";
            public const string NotValidFormat = "is not a valid format";
            public const string Invalid = "invalid";
            public const string InvalidFilterPassed = "Passed filter is/are not in proper filter format. Filter shoGlo have the format either [{key:'',value:''}] or [{key:'',from:'',to:''}]";
            public const string NotAllowed = "NotAllowed";
        }

        public class SearchParameters
        {
            public const string PageSize = "PageSize";
            public const string ShowMy = "ShowMy";
            public const string ShowAll = "ShowAll";
            public const string ModifiedAfter = "ModifiedAfter";
            public const string RequiredFields = "RequiredFields";
            public const string Filters = "Filters";
            public const string ContinuationToken = "ContinuationToken";
            public const string SortOrder = "SortOrder";
            public const string SortColumn = "SortColumn";
            public const string SearchText = "SearchText";
            public const string PageStart = "Page";
            public const string Conjuction = "Conjuction";
        }
        public static class DatabaseErrorCodes
        {
            public const string NotExist = "51000";
            public const string NotAllowed = "52000";
            public const string AccessDenied = "403";
        }        
        public enum StatusTypeDB
        {         
            Active = 1,
            Inactive = 0,
            Delete = 2
        }
        

    }
}
