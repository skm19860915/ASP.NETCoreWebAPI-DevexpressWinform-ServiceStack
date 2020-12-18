using ServiceStack;

namespace Xperters.Admin.ServiceModel
{
	public abstract class Response : IResponse
	{
		public ResponseStatus ResponseStatus { get; set; }
	}
}
