using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace api.Helper
{
    public class QueryObject
    {
        public string? CompanyName {get;set;}=null;
        public string? Symbol {get;set;}=null;
        public string? SortBy {get;set;}=null;
        public bool IsDecending{get;set;}=false;
    }
}