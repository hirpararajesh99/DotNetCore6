using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SpDbContext
{
    /// <summary>
    /// 
    /// </summary>
    public class ExecutreStoreProcedureResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Result { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ExecutreStoreProcedureResultWithEntitySID
    {
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EntitiySID { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ExecutreStoreProcedureResultWithSID
    {
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SID { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class ExecuteStoreProcedureResultWithId
    {
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
    }

    public class ExecutreStoreProcedureResultList
    {
        public string ErrorMessage { get; set; }
        public string Result { get; set; }
        public int TotalCount { get; set; }
        public int PageFrom { get; set; }
        public int PageSize { get; set; }
    }
}
