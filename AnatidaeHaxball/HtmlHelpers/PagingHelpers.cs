using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Linq.Expressions;
using System.Web.Mvc.Html;

namespace AnatidaeHaxball.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static String CurrentAction(this HtmlHelper html)
        {
            return html.ViewContext.RouteData.Values["action"].ToString();
        }

        public static MvcHtmlString DisplayNameFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData);
            return MvcHtmlString.Create(metaData.GetDisplayName());
        }

        public static MvcHtmlString DisplayNameFor<TModel, TValue>(
            this HtmlHelper<IEnumerable<TModel>> html,
            Expression<Func<TModel, TValue>> expression)
        {
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<TModel>());
            return MvcHtmlString.Create(metaData.GetDisplayName());
        }

        public static MvcHtmlString InputImage(this HtmlHelper html)
        {
            //Upload new image: <input type="file" name="Image" accept="image/*" />
            StringBuilder result = new StringBuilder();
            result.Append("Nova Imagem: ");
            TagBuilder tag = new TagBuilder("input");
            tag.MergeAttribute("type", "file");
            tag.MergeAttribute("name", "Image");
            tag.MergeAttribute("accept", "image/*");
            result.Append(tag.ToString());

            return MvcHtmlString.Create(result.ToString());
            
        }
    }
}