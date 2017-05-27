using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Studio.Helpers
{
	public static class MenuHelper
	{
		public static IHtmlString Menu(this HtmlHelper helper, string menuId, string cssClass, string startingNodeUrl = "", bool showStartingNode = true)
		{
			var menuList = new StringBuilder();
			SiteMapNode rootNode;
			SiteMapNodeCollection childNodes;

			// Check for Starting Node (now only checks one level)
			rootNode = SiteMap.RootNode;

			if (startingNodeUrl != "")
			{
				foreach (SiteMapNode node in SiteMap.RootNode.ChildNodes)
				{
					if (node.Url.Equals(startingNodeUrl))
					{
						rootNode = node;
						childNodes = rootNode.ChildNodes;
					}
				}
			}
			
			childNodes = rootNode.ChildNodes;

			// Create opening unordered list tag
			if (menuId != "")
			{
				menuList.AppendFormat("<ul id=\"{0}\">", menuId);
			}
			else
			{
				menuList.AppendFormat("<ul class=\"{0}\">", cssClass);
			}

			menuList.AppendLine(); //add one crlf, just to keep code clean

			if (showStartingNode)
			{
				AddMenuItem(helper, menuList, rootNode, rootNode);
			}

			foreach (SiteMapNode node in childNodes)
			{
				AddMenuItem(helper, menuList, rootNode, node);
			}

			// Close unordered list tag
			menuList.Append("</ul>");

			return helper.Raw(menuList.ToString());
		}

		private static void AddMenuItem(HtmlHelper helper, StringBuilder menuList, SiteMapNode rootNode, SiteMapNode node)
		{
			if (SiteMap.CurrentNode != null)
			{
				
				if (SiteMap.CurrentNode.Equals(node))
					menuList.Append("<li class=\"current\">");
				else if (SiteMap.CurrentNode.IsDescendantOf(node) && !node.Equals(rootNode))
					menuList.Append("<li class=\"current\">");
				else
					menuList.Append("<li>");
			}
			else
			{
				menuList.Append("<li>");
			}

			
			string title = node.Title;

			//When printing second level menu, use the description for an alternative label (if provided)
			if (!SiteMap.RootNode.Equals(rootNode) && node.Description != "")
			{
				title = node.Description;
			}

			menuList.AppendFormat("<a href=\"{0}\">{1}</a>", node.Url, helper.Encode(title));
			menuList.AppendLine("</li>");
		}
	}
}