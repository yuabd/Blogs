using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Blogs.Models
{
	public class Paginated<T> : List<T>
	{
		private IHttpContextAccessor _accessor;

		// Required
		public int PageIndex { get; private set; }
		public int TotalRecords { get; private set; }
		public int TotalPages { get; private set; }
		public int PageSize { get; private set; }

		// Optional
		public int PageRange { get; set; }
		public bool PreviousNext { get; set; }
		public bool Continued { get; set; }
		public bool Advanced { get; set; }

		// Private
		private HtmlString _pageList;

		public Paginated(IQueryable<T> source, int pageIndex, int pageSize, IHttpContextAccessor accessor)
		{
			_accessor = accessor;
			PageIndex = pageIndex;
			TotalRecords = source.Count();
			PageSize = pageSize;

			TotalPages = (int)Math.Ceiling(TotalRecords / (double)PageSize);
			PageRange = 10;

			PreviousNext = true;
			Continued = true;
			Advanced = true;

			this.AddRange(source.Skip((PageIndex - 1) * PageSize).Take(PageSize));
		}

		public HtmlString Pager()
		{
			return _pageList;
		}

		public HtmlString Pager(string pagerID)
		{
			string pageParamName = GetPageParameterName(pagerID);
			string pageParameter = "?" + pageParamName + "={0}";

			int currentPage = 1;
			
			if (!string.IsNullOrWhiteSpace(_accessor.HttpContext.Request.Query[pageParamName].ToString()))
				currentPage = Convert.ToInt32(_accessor.HttpContext.Request.Query[pageParamName].ToString());

			// calculate limits and parameters
			int prevLimit = 1;
			int nextLimit = prevLimit + PageRange - 1;

			// look for right range to display
			while (currentPage > nextLimit)
			{
				prevLimit += PageRange;
				nextLimit = prevLimit + PageRange - 1;
			}

			// adjust nextLimit in case not exact multiple
			if (nextLimit > TotalPages) nextLimit = TotalPages;

			// generate pages list
			string pagesList = "";
			string urlParameters = GetUrlParameterList(pageParamName); // get the rest of the querystring parameters

			//pagesList += "<div class=\"col-md-12\">";
			for (int i = prevLimit; i <= nextLimit; i++)
			{
				if (i != currentPage)
					pagesList += string.Format("<li><a href=\"" + pageParameter + "{1}\">{0}</a></li> ", i, urlParameters);
				else
					pagesList += string.Format("<li class=\"active\"><a>{0}</a></li> ", i);
			}
			//pagesList += "</div>";

			// set prev/next ...
			if (Continued)
			{
				if (prevLimit - PageRange > 0)
					pagesList = string.Format("<li><a href=\"" + pageParameter + "{2}\">...</a></li> {1}",
						prevLimit - PageRange, pagesList, urlParameters);

				if (prevLimit + PageRange <= TotalPages)
					pagesList = string.Format("{1} <li><a href=\"" + pageParameter + "{2}\">...</a></li>",
						prevLimit + PageRange, pagesList, urlParameters);
			}

			// set prev/next page
			if (PreviousNext)
			{
				if (currentPage > 1)
					pagesList = string.Format("<li class=\"PagedList-skipToPrevious\"><a href=\"" + pageParameter + "{2}\">Prev</a></li> {1}",
						currentPage - 1, pagesList, urlParameters);

				if (currentPage + 1 <= TotalPages)
					pagesList = string.Format("{1} <li class=\"PagedList-skipToNext\"><a href=\"" + pageParameter + "{2}\">Next</a></li>",
						currentPage + 1, pagesList, urlParameters);
			}

			// advanced
			//if (Advanced)
			//	pagesList = string.Format("{0}<span> 总计： {1}</span>", pagesList, TotalRecords, currentPage, TotalPages);

			pagesList = string.Format("<ul class=\"pagination\">{0}</ul>", pagesList);

			_pageList = new HtmlString(pagesList);

			return _pageList;
		}

		private string GetPageParameterName(string pagerID)
		{
			// if pageID is not the default means several pagers in one page then change param Page to ID_Page
			string pageParamName = "Page";

			if (pagerID.ToLower() != "pager")
				pageParamName = pagerID.ToString() + "_page";

			return pageParamName;
		}

		private string GetUrlParameterList(string pageParameterName)
		{
			var request = _accessor.HttpContext.Request;
			var parameters = new Dictionary<string, object>();
			//request.QueryString.(parameters);

			//foreach (var item in request.Query.Keys)
			//{
			//	parameters.Add(item, request.Query[item].ToString());
			//}

			string parameterList = "";
			foreach (var item in request.Query.Keys)
			{
				if (item != pageParameterName)
				{
					parameterList += string.Format("&{0}={1}", item, HttpUtility.HtmlEncode(request.Query[item].ToString()));
				}
			}

			if (parameterList != "")
				parameterList = "&amp;" + parameterList.Substring(1);

			return parameterList;
		}
	}
}
