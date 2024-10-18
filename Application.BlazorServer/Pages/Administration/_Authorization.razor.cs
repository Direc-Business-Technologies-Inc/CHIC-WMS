namespace Application.BlazorServer.Pages.Administration
{
    public partial class _Authorization
    {
        [Parameter]
        public string Id { get; set; } = string.Empty;

        dynamic Breadcrumbs = new dynamic[]
        {
            "Administration",
            "Users",
            "User Authorization"
        };

        protected override void OnInitialized()
        {

        }
    }
}
