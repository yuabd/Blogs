using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Model.ViewModels.Others
{
	public class xheditorModel
	{
		public string _err;
		public string _msg;
		public string err
		{
			get
			{
				if (_err == null)
					return string.Empty;
				return _err;
			}
			set
			{
				_err = value;
			}
		}

		public string msg
		{
			get
			{
				if (_msg == null)
					return string.Empty;
				return _msg;
			}
			set
			{
				_msg = value;
			}
		}
	}
}
