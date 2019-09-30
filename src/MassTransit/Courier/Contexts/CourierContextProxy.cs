namespace MassTransit.Courier.Contexts
{
    using System;
    using Context;
    using Contracts;
    using GreenPipes.Payloads;


    public abstract class CourierContextProxy :
        ConsumeContextProxy<RoutingSlip>,
        CourierContext
    {
        readonly CourierContext _courierContext;

        protected CourierContextProxy(CourierContext courierContext)
            : base(courierContext)
        {
            _courierContext = courierContext;
        }

        protected CourierContextProxy(CourierContext courierContext, IPayloadCache payloadCache)
            : base(courierContext, payloadCache)
        {
            _courierContext = courierContext;
        }

        DateTime CourierContext.Timestamp => _courierContext.Timestamp;
        TimeSpan CourierContext.Elapsed => _courierContext.Elapsed;
        Guid CourierContext.TrackingNumber => _courierContext.TrackingNumber;
        Guid CourierContext.ExecutionId => _courierContext.ExecutionId;
        string CourierContext.ActivityName => _courierContext.ActivityName;
    }
}
