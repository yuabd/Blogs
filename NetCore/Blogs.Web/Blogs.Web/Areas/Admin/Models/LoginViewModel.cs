using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Blogs.Web.Areas.Admin.Models
{

    public class LoginViewModel : IdentityUser<int>
	{
		//[Required(ErrorMessage = "必填项")]
		//public int UserID { get; set; }

		//[Required(ErrorMessage = "必填项")]
		//public string Password { get; set; }

		//public bool RememberMe { get; set; }

		//public LoginViewModel()
		//{
		//}
	}

	public class LoginUserRole : IdentityRole<int>
	{

	}
}
