using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Reservea.Common.Helpers
{
    public class CannonService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CannonService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Fire<T>(Action<T> bullet, Action<Exception> handler = null)
        {
            Task.Run(() =>
            {
                using var scope = _scopeFactory.CreateScope();
                var dependency = scope.ServiceProvider.GetRequiredService<T>();
                try
                {
                    bullet(dependency);
                }
                catch (Exception e)
                {
                    handler?.Invoke(e);
                }
                finally
                {
                    dependency = default;
                }
            });
        }

        public void FireAsync<T>(Func<T, Task> bullet, Action<Exception> handler = null)
        {
            Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var dependency = scope.ServiceProvider.GetRequiredService<T>();
                try
                {
                    await bullet(dependency);
                }
                catch (Exception e)
                {
                    handler?.Invoke(e);
                }
                finally
                {
                    dependency = default;
                }
            });
        }
    }
}
