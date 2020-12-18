using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xperters.Admin.UI.Common.Mediator
{
	public class BasicMediator : IMediator
	{
		private SynchronizedCollection<(Type type, object handler, object owner)> HandlersMap { get; } = new SynchronizedCollection<(Type type, object handler, object owner)>();
		private SynchronizedCollection<(Type type, object handler, object owner, Type output)> ResponseHandlersMap { get; } = new SynchronizedCollection<(Type type, object handler, object owner, Type output)>();

		public void DeregisterAll(object owner)
		{
			if (owner == null)
				throw new ArgumentNullException(nameof(owner));

			HandlersMap.RemoveAll(o => o.owner == owner);
			ResponseHandlersMap.RemoveAll(o => o.owner == owner);
		}

		public void Deregister<T>(Func<T, Task> handler)
		{
			if (handler == null)
				throw new ArgumentNullException(nameof(handler));

			HandlersMap.RemoveAll(o => o.handler.Equals(handler));
			ResponseHandlersMap.RemoveAll(o => o.handler.Equals(handler));
		}

		public void Register<T>(Func<T, Task> handler, object owner)
		{
			if (handler == null)
				throw new ArgumentNullException(nameof(handler));

			if (owner == null)
				throw new ArgumentNullException(nameof(owner));

			if (!HandlersMap.Select(o => o.handler).Contains(handler))
				HandlersMap.Add((typeof(T), handler, owner));
		}

		public void RegisterResponse<T, TOutput>(Func<T, Task<TOutput>> handler, object owner)
		{
			if (handler == null)
				throw new ArgumentNullException(nameof(handler));

			if (owner == null)
				throw new ArgumentNullException(nameof(owner));

			if (ResponseHandlersMap.Any(o => o.type == typeof(T) && o.output == typeof(TOutput)))
			{
				throw new Exception($"A handler is already registered for type '{typeof(T)}' and '{typeof(TOutput)}'");
			}

			if (!ResponseHandlersMap.Select(o => o.handler).Contains(handler))
				ResponseHandlersMap.Add((typeof(T), handler, owner, typeof(TOutput)));
		}

		public async Task SendAsync<T>(T message)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));

			foreach (var handlerMap in HandlersMap.Where(o => o.type == typeof(T)).ToList())
			{
				var handler = (Func<T, Task>)handlerMap.handler;
				await handler(message).ConfigureAwait(false);
			}
		}

		public async Task<TResult> GetAsync<T, TResult>(T message)
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));

			foreach (var handlerMap in ResponseHandlersMap.Where(o => o.type == typeof(T) && o.output == typeof(TResult)).ToList())
			{
				var handler = (Func<T, Task<TResult>>)handlerMap.handler;
				return await handler(message).ConfigureAwait(false);
			}

			return default(TResult);
		}
	}
}
