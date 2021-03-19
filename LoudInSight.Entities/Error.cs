using System;
using System.Collections.Generic;
using System.Text;

namespace LoudInSight.Entities
{
	public class Error
	{
		public Error()
		{
			ErrorMessage = new List<string>();
			
		}
		
		public IList<string> ErrorMessage { set; get; }
		public ErrorCode ErrorCode { get; set; }
		public string ErrorType { get; set; }
	}
	

}
