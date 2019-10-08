// Copyright 2007-2018 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit
{
    using System.ComponentModel;
    using ConsumeConfigurators;
    using GreenPipes;
    using SagaConfigurators;


    public interface IConsumePipeConfigurator :
        IPipeConfigurator<ConsumeContext>,
        IConsumerConfigurationObserverConnector,
        ISagaConfigurationObserverConnector,
        IHandlerConfigurationObserverConnector,
        IConsumerConfigurationObserver,
        ISagaConfigurationObserver,
        IHandlerConfigurationObserver
    {
        /// <summary>
        /// Adds a type-specific pipe specification to the consume pipe
        /// </summary>
        /// <typeparam name="T">The message type</typeparam>
        /// <param name="specification"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void AddPipeSpecification<T>(IPipeSpecification<ConsumeContext<T>> specification)
            where T : class;

        /// <summary>
        /// Adds a pipe specification prior to the <see cref="GreenPipes.Filters.DynamicFilter{ConsumeContext}"/> so that a single
        /// instance is used for all message types
        /// </summary>
        /// <param name="specification"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void AddPrePipeSpecification(IPipeSpecification<ConsumeContext> specification);
    }
}
