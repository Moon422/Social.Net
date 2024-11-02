using Social.Net.Core.Attributes.DependencyRegistrars;

namespace Social.Net.Data;

[SingletonDependency(typeof(ITransactionManager))]
public class TransactionManager(SocialDbContext socialDbContext) : ITransactionManager
{
    public async Task RunTransactionAsync(Func<Task> transactionOperation)
    {
        await using var transaction = await socialDbContext.Database.BeginTransactionAsync();

        try
        {
            await transactionOperation();
            await socialDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
        }
    }
}