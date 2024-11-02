namespace Social.Net.Data;

public interface ITransactionManager
{
    Task RunTransactionAsync(Func<Task> transactionOperation);
}