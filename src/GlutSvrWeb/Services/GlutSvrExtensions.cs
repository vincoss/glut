using GlutSvrWeb.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlutSvrWeb.Services
{
    public static class GlutSvrExtensions
    {
        public static decimal GetPercent(this decimal value, int totalItems)
        {
            if(totalItems <= 0)
            {
                return 0M;
            }
            return ((value * 100) / totalItems);
        }




        public static DataTableParameter GetDataTableArgs(this IFormCollection form)
        {
            if(form == null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            var draw = form["draw"].FirstOrDefault();
            var length = form["length"].FirstOrDefault();
            var start = form["start"].FirstOrDefault();
            var searchValue = form["search[value]"].FirstOrDefault();
            var sortColumn = form["columns[" + form["order[0][column]"].FirstOrDefault() + "][data]"].FirstOrDefault();
            var sortColumnDir = form["order[0][dir]"].FirstOrDefault();

            //Paging Size (10,20,50,100)    
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            return new DataTableParameter
            {
                Draw = draw,
                SortColumn = sortColumn,
                SortDirection = sortColumnDir ?? ViewConstants.SortDirectionAsc,
                Search = searchValue,

                Skip = skip,
                Take = pageSize
            };
        }
    }
}
