﻿using NServiceBus.Pipeline;

namespace VerifyTests;

public static class VerifyNServiceBus
{
    [Obsolete("Use Initialize")]
    public static void Enable(bool captureLogs = true) =>
        Initialize(captureLogs);

    public static bool Initialized { get; private set; }

    public static void Initialize(bool captureLogs = true)
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        if (captureLogs)
        {
            LogCapture.Initialize();
        }

        VerifierSettings.IgnoreMember<TestableMessageProcessingContext>(x => x.MessageHeaders);
        VerifierSettings.IgnoreMember<TestableInvokeHandlerContext>(x => x.Headers);
        VerifierSettings.IgnoreMember<TestableMessageProcessingContext>(x => x.MessageId);
        VerifierSettings.IgnoreMember<TestableInvokeHandlerContext>(x => x.MessageHandler);
        VerifierSettings.IgnoreMember<TestableInvokeHandlerContext>(x => x.MessageBeingHandled);
        VerifierSettings.IgnoreMember<TestableInvokeHandlerContext>(x => x.MessageMetadata);
        VerifierSettings.IgnoreMember<LogicalMessage>(x => x.Metadata);
        VerifierSettings.IgnoreMember<IMessageProcessingContext>(x => x.ReplyToAddress);
        VerifierSettings.IgnoreMember<TestableEndpointInstance>(x => x.EndpointStopped);
        VerifierSettings.IgnoreMember<TestableOutgoingLogicalMessageContext>(x => x.RoutingStrategies);
        VerifierSettings.IgnoreMember<TestableOutgoingPhysicalMessageContext>(x => x.RoutingStrategies);
        VerifierSettings.IgnoreMember<TestableRoutingContext>(x => x.RoutingStrategies);
        VerifierSettings.IgnoreInstance<ContextBag>(x => !ContextBagHelper.HasContent(x));
        VerifierSettings.AddExtraSettings(serializer =>
        {
            var converters = serializer.Converters;
            converters.Add(new IncomingMessageConverter());
            converters.Add(new ContextBagConverter());
            converters.Add(new UnicastSendRouterStateConverter());
            converters.Add(new RoutingToDispatchConnectorStateConverter());
            converters.Add(new SendOptionsConverter());
            converters.Add(new ExtendableOptionsConverter());
            converters.Add(new UnsubscriptionConverter());
            converters.Add(new TimeoutMessageConverter());
            converters.Add(new SagaConverter());
            converters.Add(new MessageToHandlerMapConverter());
            converters.Add(new SubscriptionConverter());
            converters.Add(new OutgoingMessageConverter());
        });
    }
}