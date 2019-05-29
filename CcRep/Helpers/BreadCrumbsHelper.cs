using CcRep.Helpers.BreadCrumbs;
using System;
using System.Web.Mvc;

namespace CcRep.Helpers
{
    public static class BreadCrumbsHelper
    {
        public static MvcHtmlString BreadCrumbs(this HtmlHelper html, Crumbs crumbs, string header)
        {
            TagBuilder divMain = new TagBuilder("div");
            divMain.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { @class = "content-header row" }));

            TagBuilder headerPart = HeaderBuilder(header);

            TagBuilder contentPart = ContentBuilder(crumbs);

            divMain.InnerHtml = headerPart.ToString() + contentPart.ToString();

            return new MvcHtmlString(divMain.ToString());
        }

        private static TagBuilder HeaderBuilder(string header)
        {
            TagBuilder divHeader = new TagBuilder("div");
            divHeader.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { @class = "content-header-left col-md-6 col-12 mb-1" }));

            TagBuilder headerH = new TagBuilder("h3");
            headerH.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { @class = "content-header-title" }));
            headerH.SetInnerText(header);

            divHeader.InnerHtml = headerH.ToString();

            return divHeader;
        }

        private static TagBuilder ContentBuilder(Crumbs crumbs)
        {
            TagBuilder contentMain = new TagBuilder("div");
            contentMain.AddCssClass("content-header-right breadcrumbs-right breadcrumbs-top col-md-6 col-12");

            TagBuilder contentWrap = new TagBuilder("div");
            contentWrap.AddCssClass("breadcrumb-wrapper col-12");

            TagBuilder olList = new TagBuilder("ol");
            olList.AddCssClass("breadcrumb");

            foreach (Crumb item in crumbs)
            {
                TagBuilder li = new TagBuilder("li");
                li.AddCssClass("breadcrumb-item");

                TagBuilder a = new TagBuilder("a");
                a.InnerHtml = item.Name;

                if (!String.IsNullOrEmpty(item.Link))
                {
                    a.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { href = item.Link }));
                }

                li.InnerHtml = a.ToString();

                olList.InnerHtml += li.ToString();
            }

            contentWrap.InnerHtml = olList.ToString();
            contentMain.InnerHtml = contentWrap.ToString();

            return contentMain;
        }
    }
}