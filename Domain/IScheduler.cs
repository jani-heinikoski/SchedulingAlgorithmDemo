namespace Domain
{
    public interface IScheduler
    {
        void OnProcessArrived(Process process);

        void ProcessOneTimeStep();

    }
}
