using System.Diagnostics;

using Microsoft.AspNetCore.Components;

namespace CorsairSolutions.RoundTable.Web.Components.Pages
{

    public partial class Error : ComponentBase
    {

        #region Properties

        [CascadingParameter]
        public HttpContext? HttpContext { get; set; }

        protected string? RequestId { get; set; } = string.Empty;

        protected bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        #endregion

        #region Protected Methods

        protected override void OnInitialized() =>
            RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;

        #endregion
    }
}
