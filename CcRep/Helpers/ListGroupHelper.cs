using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace CcRep.Helpers
{
    internal class ListGroupContainer : IDisposable
    {
        private readonly TextWriter _writer;

        public ListGroupContainer(TextWriter writer)
        {
            _writer = writer;
        }

        public void Dispose()
        {
            _writer.WriteLine("</div>");
        }
    }

    public static class ListGroupHelper
    {
        public static IDisposable BeginListGroup(this HtmlHelper htmlHelper, string cssClass = "")
        {
            var writer = htmlHelper.ViewContext.Writer;
            writer.WriteLine(string.Format("<div class='list-group {0}'>", cssClass));

            return new ListGroupContainer(writer);
        }

        public static MvcHtmlString ListGroupItem<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string href = null)
        {
            var prop = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, new ViewDataDictionary<TModel>(htmlHelper.ViewData));

            TagBuilder a = new TagBuilder("a");
            a.MergeAttributes(new Dictionary<string, string> { { "class", "list-group-item list-group-item-action flex-column align-items-start" } });

            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("d-flex w-100 justify-content-between");
            TagBuilder h5 = new TagBuilder("h5");
            h5.InnerHtml = prop.DisplayName ?? prop.PropertyName;
            h5.AddCssClass("text-bold-600");
            div.InnerHtml = h5.ToString();

            TagBuilder p = new TagBuilder("p");
            p.InnerHtml = prop.Model?.ToString();

            a.InnerHtml = div.ToString() + p.ToString();

            return new MvcHtmlString(a.ToString());
        }
    }
}