namespace NumberPlace.Application.Abstracts
{
    public delegate void MatGeneratedFailedEventHandler(Exception ex);

    public interface INotifyMatGeneratedFailed
    {
        event MatGeneratedFailedEventHandler? OnMatGeneratedFailed;
    }
}
