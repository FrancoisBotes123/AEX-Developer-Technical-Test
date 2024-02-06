using CSV.components;
using CSV.Interfaces;

namespace CSV.Services
{
    public class ConfirmDialogService : IConfirmDialogService
    {
        private ConfirmDialog _confirmDialog;

        public void Register(ConfirmDialog dialog)
        {
            _confirmDialog = dialog;
        }
       
        public Task<bool> ShowDialog(string title, string message)
        {
            if (_confirmDialog == null)
            {
                throw new InvalidOperationException("ConfirmDialog not registered.");
            }

            return _confirmDialog.Show(title, message);
        }
    }
}
