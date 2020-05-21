using Acr.UserDialogs;
using Covid19Stats.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Covid19Stats.Services.General
{
    public class DialogService : IDialogService
    {
        public Task ShowError(string message, string title, string buttonText)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonText);
        }

        public Task ShowError(Exception error, string title, string buttonText)
        {
            return UserDialogs.Instance.AlertAsync(error.Message, title, buttonText);
        }

        public Task ShowMessage(string message, string title)
        {
            return UserDialogs.Instance.AlertAsync(message, title, "OK");

        }
        public Task ShowAlertMessage(string message, string title)
        {
            var tcs = new TaskCompletionSource<bool>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Application.Current.MainPage.DisplayAlert(title, message, AlertConfig.DefaultOkText);
                tcs.SetResult(true);
            });

            return tcs.Task;
        }
        public Task<bool> ShowMessage(string message, string title, string buttonText)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, buttonText);
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, buttonConfirmText, buttonCancelText);
        }

        public Task ShowMessageBox(string message, string title)
        {
            return UserDialogs.Instance.AlertAsync(message, title, "OK");
        }

        public void ShowLoader(string message = "")
        {
            UserDialogs.Instance.ShowLoading(message);
        }

        public void HideLoader()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}

