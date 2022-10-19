﻿using Helpers;
using Microsoft.EntityFrameworkCore;
using Models.CustomModels;
using Models.SpDbContext;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Models.Constant.Constants;

namespace InfraStructure.Database.SpRepository
{
    public static class SpRepository
    {

        public static SearchPage<T> BindSearchList<T>(Dictionary<string, object> parameters, List<T> records)
        {
            SearchPage<T> result = new SearchPage<T>
            {
                List = records
            };


            int from = 0, size = 10;
            if (parameters != null)
            {
                if (parameters.ContainsKey(SearchParameters.PageSize) && parameters.ContainsKey(SearchParameters.PageStart))
                {
                    size = Convert.ToInt32(parameters[SearchParameters.PageSize]);
                    from = (Convert.ToInt32(parameters[SearchParameters.PageStart]) - 1) * size;
                    result.Meta.PageSize = size;
                    result.Meta.Page = Convert.ToInt32(parameters[SearchParameters.PageStart]);
                }
                else
                {
                    result.Meta.PageSize = size;
                    result.Meta.Page = from + 1;
                }
            }
            else
            {
                result.Meta.PageSize = size;
                result.Meta.Page = from + 1;
            }

            return result;
        }

        public static async Task<Page> ExecutreStoreProcedureResult(this SpContext catalogDbContext, string sqlQuery, object[] param)
        {
            Page page = new Page();
            var response = await catalogDbContext.Set<ExecutreStoreProcedureResultList>().FromSqlRaw(sqlQuery, param).ToListAsync();
            if (response != null && response.Count > 0)
            {
                var result = response.FirstOrDefault();

                if (result == null) return page;

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    //throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, result.ErrorMessage);
                }

                page.Meta.TotalResults = result.TotalCount;
                if (!string.IsNullOrWhiteSpace(result.Result))
                {

                    var list = (JArray)JsonConvert.DeserializeObject(result.Result);

                    //list.Descendants()
                    //    .OfType<JProperty>()
                    //    .Where(attr => attr.Name.StartsWith("count"))
                    //    .ToList()
                    //    .ForEach(attr => attr.Remove());

                    page.Result = list;
                    return page;
                }
            }
            return page;
        }



        public static async Task ExecuteStoreProcedureQuery(this SpContext catalogDbContext, string sqlQuery, object[] param)
        {
            var saveResult = await catalogDbContext.Set<ExecutreStoreProcedureResult>().FromSqlRaw(sqlQuery, param).ToListAsync();

            if (saveResult != null && saveResult.Count > 0)
            {
                var errorResult = saveResult.FirstOrDefault();
                if (!string.IsNullOrEmpty(errorResult.ErrorMessage))
                {
                    //test
                    if (errorResult.Result == DatabaseErrorCodes.NotExist) //not exist
                    {
                        //var codes = MessageHelper.ReadAutoResponderCodesMessage(errorResult.ErrorMessage, RequestStatus: ErrorMessages.NotFound);
                        //throw new HttpStatusCodeException(StatusCodes.Status404NotFound, errorResult.ErrorMessage, StatusCodes.Status404NotFound.ToString());
                    }
                    else if (errorResult.Result == DatabaseErrorCodes.NotAllowed)
                    {
                        //var codes = MessageHelper.ReadAutoResponderCodesMessage(errorResult.ErrorMessage, RequestStatus: ErrorMessages.NotAllowed);
                        //throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, errorResult.ErrorMessage, StatusCodes.Status400BadRequest.ToString());
                    }
                    //throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, errorResult.ErrorMessage);
                }
            }
        }
        public static async Task<string> ExecuteStoreProcedureQueryWithSID(this SpContext catalogDbContext, string sqlQuery, object[] param)
        {
            var saveResult = await catalogDbContext.Set<ExecutreStoreProcedureResultWithSID>().FromSqlRaw(sqlQuery, param).ToListAsync();

            if (saveResult != null && saveResult.Count > 0)
            {
                var result = saveResult.FirstOrDefault();
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    if (result.SID == DatabaseErrorCodes.NotExist) //not exist
                    {
                        //var codes = MessageHelper.ReadAutoResponderCodesMessage(result.ErrorMessage, RequestStatus: ErrorMessages.NotFound);
                        //throw new HttpStatusCodeException(StatusCodes.Status404NotFound, result.ErrorMessage, StatusCodes.Status404NotFound.ToString());
                    }
                    else if (result.SID == DatabaseErrorCodes.NotAllowed)
                    {
                        //var codes = MessageHelper.ReadAutoResponderCodesMessage(result.ErrorMessage, RequestStatus: ErrorMessages.NotAllowed);
                        //throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, result.ErrorMessage, StatusCodes.Status400BadRequest.ToString());
                    }
                    //throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, result.ErrorMessage);
                }
                return result.SID;
            }
            return null;
        }

        public static async Task<string> ExecuteStoreProcedure(this SpContext catalogDbContext, string sqlQuery, object[] param)
        {
            var response = await catalogDbContext.Set<ExecutreStoreProcedureResult>().FromSqlRaw(sqlQuery, param).ToListAsync(); ;

            if (response == null || response.Count <= 0) return string.Empty;

            var result = response.FirstOrDefault();

            if (result == null) return string.Empty;

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                if (result.Result == DatabaseErrorCodes.NotExist) //not exist
                {
                    //throw new HttpStatusCodeException(StatusCodes.Status404NotFound, result.ErrorMessage);
                }
                else if (result.Result == DatabaseErrorCodes.NotAllowed)
                {
                    //throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, result.ErrorMessage);
                }
                //throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, result.ErrorMessage);
            }
            return string.IsNullOrEmpty(result.Result) ? string.Empty : result.Result;

        }

        public static async Task<string> ExecuteStoreProcedureWithEntitySID(this SpContext catalogDbContext, string sqlQuery, object[] param)
        {
            var response = await catalogDbContext.Set<ExecutreStoreProcedureResultWithEntitySID>().FromSqlRaw(sqlQuery, param).ToListAsync();

            if (response == null || response.Count <= 0) return string.Empty;

            var result = response.FirstOrDefault();

            if (result == null) return string.Empty;

            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                if (result.Result == DatabaseErrorCodes.NotExist) //not exist
                {
                    if (string.IsNullOrWhiteSpace(result.EntitiySID))
                    {
                        var codes = MessageHelper.ReadGlodMessage(result.ErrorMessage, RequestStatus: ErrorMessages.NotFound);                        
                    }
                    else
                    {
                        var codes = MessageHelper.ReadGlodMessage(result.ErrorMessage, RequestStatus: ErrorMessages.NotFound);                        
                    }
                }
                else if (result.Result == DatabaseErrorCodes.NotAllowed)
                {
                    var codes = MessageHelper.ReadGlodMessage(result.ErrorMessage, RequestStatus: ErrorMessages.NotAllowed);                    
                }                
            }
            return string.IsNullOrEmpty(result.Result) ? string.Empty : result.Result;

        }

        public static async Task<Page> ExecuteStoreProcedureForSearchList(this SpContext catalogDbContext, string sqlQuery, object[] param)
        {
            Page page = new Page();
            var response = await catalogDbContext.Set<ExecutreStoreProcedureResultList>().FromSqlRaw(sqlQuery, param).ToListAsync();
            if (response != null && response.Count > 0)
            {
                var result = response.FirstOrDefault();

                if (result == null) return page;

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    //throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, result.ErrorMessage);
                }

                page.Meta.TotalResults = result.TotalCount;
                if (!string.IsNullOrWhiteSpace(result.Result))
                {

                    var list = (JArray)JsonConvert.DeserializeObject(result.Result);

                    //list.Descendants()
                    //    .OfType<JProperty>()
                    //    .Where(attr => attr.Name.StartsWith("count"))
                    //    .ToList()
                    //    .ForEach(attr => attr.Remove());

                    page.Result = list;
                    return page;
                }
            }
            return page;
        }

    }

}
