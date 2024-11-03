using Social.Net.Core.Attributes.DependencyRegistrars;

namespace Social.Net.Data;

[ScopedDependency(typeof(ITransactionManager))]
public class TransactionManager(SocialDbContext socialDbContext) : ITransactionManager
{
    public async Task RunTransactionAsync(Func<Task> transactionOperation)
    {
        await using var transaction = await socialDbContext.Database.BeginTransactionAsync();

        try
        {
            await transactionOperation();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }
    }
}