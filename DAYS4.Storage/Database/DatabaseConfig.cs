using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAYS4.Storage.Database
{
	public class DatabaseConfig
	{
		string _name;
		public string Name
		{
			get { return "Data Source=" + Environment.CurrentDirectory + "\\" + _name; }
			set { _name = value; }
		}
	}
}
