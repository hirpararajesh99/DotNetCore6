using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace Models.CustomModels
{
    /// <summary>
    /// 
    /// </summary>
    public class Page
    {
        /// <summary>
        /// 
        /// </summary>
        public Page()
        {
            Meta = new Meta();
            Result = new JArray();
        }

        /// <summary>
        /// 
        /// </summary>        
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("results")]
        public Object Result { get; set; }

    }

}
