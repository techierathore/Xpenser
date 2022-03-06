using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Xpenser.Models;
using Xpenser.UI.Services;
using Xpenser.UI.Shared;
using Xpenser.UI.ViewModels;

namespace Xpenser.UI.Pages
{
    public partial class SignUpPage : ComponentBase
    {
        public SvcData SignUpDetails { get; set; }
        public SignUpVm PageObj { get; set; }
        public string SignUpMesssage { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAuthService AuthSvc { get; set; }

        private bool vSignUpDone;
        bool AgreeToTerms;
        protected VerifyPopUp VerifyDialog;
        bool CheckIsDisabledSubmit = true;
        protected override Task OnInitializedAsync()
        {
            CheckIsDisabledSubmit = true;
            PageObj = new SignUpVm();
            return base.OnInitializedAsync();
        }

        private async Task RegisterUser()
        {
            CheckIsDisabledSubmit = true;
            PageObj.IsVerified = false;
            PageObj.IsFirstLogin = true;
            SignUpDetails = new SvcData()
            { ComplexData = JsonSerializer.Serialize(PageObj) };
            try
            {
                vSignUpDone = await AuthSvc.RegisterUserAsync(SignUpDetails);

                if (vSignUpDone)
                {
                    VerifyDialog.UserEmail = PageObj.UserEmail;
                    VerifyDialog.ShowPopUp();
                }
            }
            catch (Exception ex)
            {
                CheckIsDisabledSubmit = false;
                SignUpMesssage = ex.Message;
            }
        }

        void OnTermsCheckChanged(bool value)
        {
            AgreeToTerms = value;
            CheckIsDisabledSubmit = false;
            if (AgreeToTerms)
                CheckIsDisabledSubmit = false;
            else CheckIsDisabledSubmit = true;
        }
    }
}
