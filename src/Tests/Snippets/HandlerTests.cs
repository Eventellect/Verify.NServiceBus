﻿[UsesVerify]
public class HandlerTests
{
    #region HandlerTest

    [Fact]
    public async Task VerifyHandlerResult()
    {
        var handler = new MyHandler();
        var context = new TestableMessageHandlerContext();

        var message = new MyRequest();
        await handler.Handle(message, context);

        await Verify(context);
    }

    #endregion
}