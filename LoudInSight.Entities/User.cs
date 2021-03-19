using System;
using System.Collections.Generic;
using System.Text;

namespace LoudInSight.Entities
{
	public class User : Base
	{
		public string Email { get; set; }
		public string MobileNumber { get; set; }
		public string Password { get; set; }
	}
}
