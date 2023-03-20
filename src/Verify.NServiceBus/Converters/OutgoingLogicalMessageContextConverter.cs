class OutgoingLogicalMessageContextConverter :
    WriteOnlyJsonConverter<TestableOutgoingLogicalMessageContext>
{
    public override void Write(VerifyJsonWriter writer, TestableOutgoingLogicalMessageContext context)
    {
        writer.WriteStartObject();

        writer.WriteMember(context, context.Message, "Message");
        writer.WriteMember(context, context.MessageId, "MessageId");
        writer.WriteMember(context, context.Headers.CleanedHeaders(), "Headers");
        writer.WriteMember(context, context.SentMessages, "SentMessages");
        writer.WriteMember(context, context.TimeoutMessages, "TimeoutMessages");
        writer.WriteMember(context, context.PublishedMessages, "PublishedMessages");
        writer.WriteMember(context, context.Extensions, "Extensions");

        writer.WriteEndObject();
    }
}