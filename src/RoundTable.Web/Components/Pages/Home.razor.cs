using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace CorsairSolutions.RoundTable.Web.Components.Pages
{

    public partial class Home : ComponentBase
    {

        #region Properties

        protected string PromptText { get; set; } = string.Empty;

        protected bool IsRunning { get; private set; }

        protected string StatusMessage { get; private set; } = "Ready to convene the Round Table.";

        #endregion

        #region Public methods

        public async Task StartSessionAsync()
        {

            if (string.IsNullOrWhiteSpace(PromptText))
            {

                StatusMessage = "Enter a prompt before convening the council.";
                return;
            }

            IsRunning = true;
            StatusMessage = "Convening council seats...";

            try
            {

                await Task.CompletedTask;
            }
            finally
            {

                IsRunning = false;
            }
        }

        #endregion
    }
}