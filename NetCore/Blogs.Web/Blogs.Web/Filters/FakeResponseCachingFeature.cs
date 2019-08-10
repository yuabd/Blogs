using Microsoft.AspNetCore.ResponseCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Web.Filters
{
	public class FakeResponseCachingFeature : IResponseCachingFeature
	{
		public string[] VaryByQueryKeys
		{
			get => null;
			set { }
		}
	}
}
