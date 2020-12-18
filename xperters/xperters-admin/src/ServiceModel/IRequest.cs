using ServiceStack;

namespace Xperters.Admin.ServiceModel
{
    public interface IRequest<T> : IReturn<T>, IGet, IHasVersion
    {
    }
}
