using Ofella.Sandbox.FunctionalProgramming;
namespace Ofella.Sandbox.Test.FunctionalProgramming;

public class ResultTests
{
    [Fact]
    public void When_ResultIsFailed_TryConsumeShouldBeFalse()
    {
        var accountNameResult = GetAccountName(0);

        if (!accountNameResult.TryConsume(out var accountName, out var accountNameResultError))
        {
            Assert.NotNull(accountNameResultError);
            return;
        }

        Assert.NotNull(accountName);
        Assert.Fail("Code should be unreachable.");
    }

    [Fact]
    public void When_ResultIsSuccess_TryConsumeShouldBeTrue()
    {
        var accountNameResult = GetAccountName(1);

        if (!accountNameResult.TryConsume(out var accountName, out var accountNameResultError))
        {
            Assert.NotNull(accountNameResultError);
            Assert.Fail("Code should be unreachable.");
            return;
        }

        Assert.NotNull(accountName);
    }

    private static Result<string> GetAccountName(int accountId)
    {
        if (accountId <= 0)
        {
            return Errors.Account.InvalidId.AsResult<string>();
        }

        return Result<string>.Ok("John Doe");
    }

    private static class Errors
    {
        public static class Account
        {
            public static Error InvalidId = new("A001", "The provided account id is not valid.");
        }
    }
}