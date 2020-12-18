using System;
using System.Threading.Tasks;

namespace Xperters.Admin.UI.Common.Mediator
{
	public interface IMediator
	{
		void Deregister<T>(Func<T, Task> handler);

		void DeregisterAll(object owner);

		void Register<T>(Func<T, Task> handler, object owner);
		void RegisterResponse<T, TOutput>(Func<T, Task<TOutput>> handler, object owner);

		Task SendAsync<T>(T message);
		Task<TResult> GetAsync<T, TResult>(T message);

	}
}
