using CcRep.Models.vk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CcRep.Components.Filtering
{
    public static class QueryBuilderProvider
    {
        private static QueryFilterGenerator Generator { get; set; }

        public static string FiltersJson
        {
            get
            {
                return Generator.GetFiltersJson();
            }
        }

        static QueryBuilderProvider()
        {
            Generator = new QueryFilterGenerator()
                .ParsForFilters<AddInfRep>()
                .ParsForFilters<BasicRep>()
                .ParsForFilters<ClientRep>()
                .ParsForFilters<IssuerRep>()
                .ParsForFilters<NoticeRep>()
                .ParsForFilters<AddRep>()
                .ParsForFilters<Rep406>()
                .ParsForFilters<SupdocRep>();

            Generator.Filters.RemoveAll(x =>
            x.id == "AddInfRep_Author" ||
            x.id == "AddInfRep_Source" ||
            x.id == "AddInfRep_DateCreate" ||
            x.id == "AddInfRep_DateRemove" ||
            x.id == "AddInfRep_IdRep" ||
            x.id == "AddInfRep_IdSource" ||
            x.id == "AddInfRep_SourceName" ||
            x.id == "AddInfRep_UserLock"
            );
        }

    }
}