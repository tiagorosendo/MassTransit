﻿namespace MassTransit.Scoping
{
    using System.Threading.Tasks;
    using Courier;
    using GreenPipes;


    /// <summary>
    /// A factory to create an activity from Autofac, that manages the lifetime scope of the activity
    /// </summary>
    /// <typeparam name="TActivity"></typeparam>
    /// <typeparam name="TArguments"></typeparam>
    public class ScopeExecuteActivityFactory<TActivity, TArguments> :
        ExecuteActivityFactory<TActivity, TArguments>
        where TActivity : class, ExecuteActivity<TArguments>
        where TArguments : class
    {
        readonly IExecuteActivityScopeProvider<TActivity, TArguments> _scopeProvider;

        public ScopeExecuteActivityFactory(IExecuteActivityScopeProvider<TActivity, TArguments> scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        public async Task<ResultContext<ExecutionResult>> Execute(ExecuteContext<TArguments> context,
            IRequestPipe<ExecuteActivityContext<TActivity, TArguments>, ExecutionResult> next)
        {
            using (IExecuteActivityScopeContext<TActivity, TArguments> scope = _scopeProvider.GetScope(context))
            {
                return await next.Send(scope.Context).ConfigureAwait(false);
            }
        }

        public void Probe(ProbeContext context)
        {
            var scope = context.CreateScope("scopeExecuteActivityFactory");

            _scopeProvider.Probe(scope);
        }
    }
}
