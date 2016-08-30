namespace MvcFileManager.Models
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/11/06
	/// Changed in New Version: 1.1.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class BaseEntity : System.Object
	{
		public BaseEntity()
			: base()
		{
		}

		public bool IsSelected { get; set; }

		public string Name { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		public System.DateTime CreationTime { get; set; }

		public string DisplayCreationTime
		{
			get
			{
				string strResult =
					CreationTime.ToString("yyyy/MM/dd - HH:mm:ss");

				return (strResult);
			}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		public System.DateTime LastWriteTime { get; set; }

		public string DisplayLastWriteTime
		{
			get
			{
				string strResult =
					LastWriteTime.ToString("yyyy/MM/dd - HH:mm:ss");

				return (strResult);
			}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		public System.DateTime LastAccessTime { get; set; }

		public string DisplayLastAccessTime
		{
			get
			{
				string strResult =
					LastAccessTime.ToString("yyyy/MM/dd - HH:mm:ss");

				return (strResult);
			}
		}

		public string Message { get; set; }
	}
}
