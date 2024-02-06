using CSV.components;

namespace CSV.Interfaces
{
    public interface IConfirmDialogService
    {
        void Register(ConfirmDialog dialog);
        Task<bool> ShowDialog(string title, string message);
    }
}
