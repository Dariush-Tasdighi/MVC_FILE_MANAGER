namespace MvcFileManager
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/11/06
	/// Changed in New Version: 1.1.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class Initializer : System.Object
	{
		public Initializer()
			: base()
		{
		}

		protected virtual string AreaName
		{
			get
			{
				object oAreaName =
					System.Web.HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"];

				if (oAreaName == null)
				{
					return (null);
				}
				else
				{
					return (oAreaName.ToString());
				}
			}
		}

		protected virtual string ControllerName
		{
			get
			{
				object oControllerName =
					System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values["controller"];

				if (oControllerName == null)
				{
					return (null);
				}
				else
				{
					return (oControllerName.ToString());
				}
			}
		}

		public string Path
		{
			get
			{
				string strResult = string.Empty;

				if (string.IsNullOrWhiteSpace(AreaName) == false)
				{
					strResult =
						string.Format("/{0}", AreaName);
				}

				if (string.IsNullOrWhiteSpace(ControllerName) == false)
				{
					strResult =
						string.Format("{0}/{1}", strResult, ControllerName);
				}

				return (strResult);
			}
		}
	}
}
