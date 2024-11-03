namespace Social.Net.Data;

public interface ITransactionManager
{
    Task RunTransactionAsync(Func<Task> transactionOperation);
    Task<TRes> RunTransactionAsync<TRes>(Func<Task<TRes>> transactionOperation);
}