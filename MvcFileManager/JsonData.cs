namespace MvcFileManager
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/11/06
	/// Changed in New Version: 1.1.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class JsonData : System.Object
	{
		public JsonData()
			: base()
		{
		}

		public object Data { get; set; }

		public string MessageText { get; set; }

		public string MessageTitle { get; set; }

		public bool DisplayMessage { get; set; }

		public Enums.JsonResultStates State { get; set; }

		public System.Web.Mvc.JsonResult GetJsonResult()
		{
			if (DisplayMessage)
			{
				if (string.IsNullOrWhiteSpace(MessageTitle))
				{
					switch (State)
					{
						case Enums.JsonResultStates.Error:
						{
							MessageTitle = Resources.Captions.ErrorState;

							break;
						}

						case Enums.JsonResultStates.Success:
						{
							MessageTitle = Resources.Captions.SuccessState;

							break;
						}

						case Enums.JsonResultStates.Warning:
						{
							MessageTitle = Resources.Captions.WarningState;

							break;
						}
					}
				}
			}

			System.Web.Mvc.JsonResult oJsonResult = new System.Web.Mvc.JsonResult();

			oJsonResult.Data = this;
			oJsonResult.RecursionLimit = null;
			oJsonResult.MaxJsonLength = System.Int32.MaxValue;
			oJsonResult.ContentEncoding = System.Text.Encoding.UTF8;
			oJsonResult.JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet;

			return (oJsonResult);
		}
	}
}
