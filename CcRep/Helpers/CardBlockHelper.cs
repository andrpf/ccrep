using CcRep.Helpers.Card;
using System;
using System.IO;
using System.Web.Mvc;

namespace CcRep.Helpers
{
    internal class CardContainer : IDisposable
    {
        private readonly TextWriter _writer;

        public CardContainer(TextWriter writer)
        {
            _writer = writer;
        }

        public void Dispose()
        {
            _writer.WriteLine("</div>");
            _writer.WriteLine("</div>");
            _writer.WriteLine("</div>");
        }
    }

    public static class CardBlockHelper
    {
        public static IDisposable BeginCard(this HtmlHelper htmlHelper, string header, CardParams parameteres = null, string cssClass = "")
        {
            if(parameteres == null)
            {
                parameteres = new CardParams();
            }

            var writer = htmlHelper.ViewContext.Writer;
            writer.WriteLine(string.Format("<div class='card {0}'>", cssClass));

            string headingPart = HeaderElements(header, parameteres);

            writer.WriteLine(headingPart);
            writer.WriteLine("<div class='card-content collapse show'>");
            writer.WriteLine("<div class='card-body card-dashboard'>");

            return new CardContainer(writer);
        }

        private static string HeaderElements(string text, CardParams parameters)
        {
            TagBuilder divMain = new TagBuilder("div");
            divMain.AddCssClass("card-header");

            TagBuilder cardTitle = new TagBuilder("h4");
            cardTitle.AddCssClass("card-title");
            cardTitle.InnerHtml = text;

            TagBuilder aToggle = new TagBuilder("a");
            aToggle.AddCssClass("heading-elements-toggle");
            TagBuilder aToggleIcon = new TagBuilder("i");
            aToggleIcon.AddCssClass("la la-ellipsis-v font-medium-3");
            aToggle.InnerHtml = aToggleIcon.ToString();

            TagBuilder headingElements = new TagBuilder("div");
            headingElements.AddCssClass("heading-elements");
            TagBuilder hElemtesList = new TagBuilder("ul");
            hElemtesList.AddCssClass("list-inline mb-0");

            string lisElements = "";

            if (parameters.Hide)
            {
                lisElements += buildHeaderButton("ft-minus", "collapse");
            }
            if (parameters.FullScreen)
            {
                lisElements += buildHeaderButton("ft-maximize", "expand");
            }
            if (parameters.Close)
            {
                lisElements += buildHeaderButton("ft-x", "close");
            }

            hElemtesList.InnerHtml = lisElements;
            headingElements.InnerHtml = hElemtesList.ToString();

            divMain.InnerHtml = cardTitle.ToString() + aToggle.ToString() + headingElements.ToString();

            return divMain.ToString();
        }

        private static string buildHeaderButton(string iClass, string dataAction)
        {
            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");

            a.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new { @data_action = dataAction }));
            TagBuilder i = new TagBuilder("i");
            i.AddCssClass(iClass);
            a.InnerHtml = i.ToString();

            li.InnerHtml = a.ToString();

            return li.ToString();
        }
    }
}