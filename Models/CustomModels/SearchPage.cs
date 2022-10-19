﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.CustomModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchPage<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public SearchPage()
        {
            List = new List<T>();
            Meta = new Meta();
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
        public List<T> List { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// The current page number. the first page is 1.
        /// </summary>        
        [JsonPropertyName("page")]
        public int Page { get; set; }

        /// <summary>
        /// Page size of the result set.
        /// </summary>        
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }
        /// <summary>
        /// Resource key name.
        /// </summary>        
        [JsonPropertyName("key")]
        public string Key { get; set; }
        /// <summary>
        /// The URL of the current page.
        /// </summary>        
        [JsonPropertyName("url")]
        public string Url { get; set; }
        /// <summary>
        /// The URL for the first page of this list.
        /// </summary>        
        [JsonPropertyName("first_page_url")]
        public string FirstPageUrl { get; set; }
        /// <summary>
        /// The URL for the previous page of this list.
        /// </summary>        
        [JsonPropertyName("previous_page_url")]
        public string PreviousPageUrl { get; set; }
        /// <summary>
        /// The URL for the next page of this list.
        /// </summary>        
        [JsonPropertyName("next_page_url")]
        public string NextPageUrl { get; set; }
        /// <summary>
        /// Total Count of results.
        /// </summary>        
        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }

        /// <summary>
        /// Total No of pages of the result set.
        /// </summary>        
        [JsonPropertyName("total_page_num")]
        public int TotalPages { get; set; }


        [JsonIgnore]
        public bool NextPageExists { get; set; }
    }
}
