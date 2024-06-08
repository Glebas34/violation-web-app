namespace ViolationWebApplication.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IViolationRepository ViolationRepository { get; }
        ICarRepository CarRepository { get; }
        Task<int> Complete();
    }
}
