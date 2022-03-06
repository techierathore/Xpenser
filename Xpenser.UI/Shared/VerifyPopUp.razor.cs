using Blazorise;
using Microsoft.AspNetCore.Components;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Xpenser.Models;
using Xpenser.UI.Services;

namespace Xpenser.UI.Shared
{
    public partial class VerifyPopUp : ComponentBase
    {
        public Modal VerifyModal { get; set; }
        public Modal EditEmailPopUp { get; set; }

        [Inject]
        public IAuthService AuthSvc { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string UserEmail { get; set; }
        public string NewUserEmail { get; set; }

        int TriesCount = 0;
        const int MaxTries = 3;
        bool HasMoreTries => TriesCount < MaxTries;
        bool RequestInProgress = false;
        bool IsDisabled => !HasMoreTries || RequestInProgress;
        string ErrorMessage = "";
        public void ShowPopUp()
        {
            VerifyModal.Show();
        }
        public void ClosePopUp()
        {
            NavigationManager.NavigateTo("LoginPage");
        }
        public Task OnModalClosing(ModalClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                // just set Cancel to true to prevent modal from closing
                e.Cancel = true;
            }
            return Task.CompletedTask;
        }
        protected async Task ReSendEmail()
        {
            RequestInProgress = true;

            try
            {
                var result = await AuthSvc.ResendVerifiEmailAsync(new SvcData
                {
                    LoginEmail = UserEmail
                });

                if (result)
                {
                    TriesCount++;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                RequestInProgress = false;
            }
        }

        private void ShowEditEmailPopUp()
        {
            NewUserEmail = UserEmail;
            EditEmailPopUp.Show();
        }

        protected async Task UpdateNSendEmail()
        {
            try
            {
                var result = await AuthSvc.UpdateNSendVerifiEmailAsync(new SvcData
                {
                    LoginEmail = UserEmail,
                    ComplexData = JsonSerializer.Serialize(new AppUser { UserEmail = NewUserEmail })
                });

                if (result)
                {
                    UserEmail = NewUserEmail;
                    EditEmailPopUp.Hide();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
