using System;
using System.Threading.Tasks;

namespace Covid19Stats.Interface
{
    public interface IDialogService
    {      
        Task ShowError(string message, string title, string buttonText);
        Task ShowError(Exception error, string title, string buttonText);
        Task ShowMessage(string message, string title);
        Task ShowAlertMessage(string message, string title);
        Task<bool> ShowMessage(string message, string title, string buttonText);
        Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText);
        Task ShowMessageBox(string message, string title);
        void ShowLoader(string message = "");
        void HideLoader();
    }
}
