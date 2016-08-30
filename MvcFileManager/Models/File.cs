namespace MvcFileManager.Models
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/11/06
	/// Changed in New Version: 1.1.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class File : BaseEntity
	{
		public File()
			: base()
		{
		}

		public File(System.IO.FileInfo fileInfo)
		{
			Name = fileInfo.Name;
			Length = fileInfo.Length;

			CreationTime = fileInfo.CreationTime;
			LastWriteTime = fileInfo.LastWriteTime;
			LastAccessTime = fileInfo.LastAccessTime;

		}

		public long Length { get; set; }
	}
}
