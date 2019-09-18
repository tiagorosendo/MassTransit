namespace MassTransit.AmazonSqsTransport.Contexts
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Amazon.SimpleNotificationService;
    using Amazon.SQS;
    using Configuration.Configuration;
    using GreenPipes;
    using GreenPipes.Payloads;
    using Topology;
    using Transport;
    using Util;


    public class AmazonSqsConnectionContext :
        BasePipeContext,
        ConnectionContext
    {
        readonly IAmazonSqsHostConfiguration _configuration;
        readonly IAmazonSqsHostTopology _hostTopology;
        readonly LimitedConcurrencyLevelTaskScheduler _taskScheduler;

        public AmazonSqsConnectionContext(IConnection connection, IAmazonSqsHostConfiguration configuration, IAmazonSqsHostTopology hostTopology,
            CancellationToken cancellationToken)
            : base(new PayloadCache(), cancellationToken)
        {
            _configuration = configuration;
            _hostTopology = hostTopology;
            Connection = connection;

            _taskScheduler = new LimitedConcurrencyLevelTaskScheduler(1);
        }

        public IConnection Connection { get; }
        public IAmazonSqsHostTopology Topology => _hostTopology;
        public Uri HostAddress => _configuration.HostAddress;

        public Task<IAmazonSQS> CreateAmazonSqs()
        {
            return Task.Factory.StartNew(() => Connection.CreateAmazonSqsClient(), CancellationToken, TaskCreationOptions.None, _taskScheduler);
        }

        public Task<IAmazonSimpleNotificationService> CreateAmazonSns()
        {
            return Task.Factory.StartNew(() => Connection.CreateAmazonSnsClient(), CancellationToken, TaskCreationOptions.None, _taskScheduler);
        }
    }
}
