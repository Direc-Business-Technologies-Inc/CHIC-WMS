namespace Application.BlazorServer.Controls.UI_Macros;

public partial class MacroInput : ComponentBase
{
	//Define the custom delegate with two parameters
	public delegate void MyEventCallback(string InputName, string Value);

	[Parameter] public InputParameterModel InputParameter { get; set; } = new InputParameterModel();
	[Parameter]	public string Title { get; set; }
	[Parameter]	public string InputName { get; set; }
	[Parameter]	public bool IsReadOnly { get; set; }
	[Parameter] public bool Required { get; set; } = false;
	[Parameter]	public bool IsSelectable { get; set; }
	[Parameter]	public string Type { get; set; }
	[Parameter] public string DefaultValue { get; set; }
	[Parameter]	public string Id { get; set; }

	// Step 2: Declare an event to notify the parent component about the input value change
	[Parameter]	public MyEventCallback ValueChanged { get; set; }

	// Step 1: Handle the input event to manually update the DefaultValue property
	private void HandleInput(ChangeEventArgs e)
	{
		InputParameter.DefaultValue = e?.Value?.ToString() ?? "";
		InputParameter.InputName = InputName == "" || InputName == null ? InputParameter.InputName : InputName;
		// Step 3: Raise the event to notify the parent component about the input value change
		ValueChanged.Invoke(InputParameter.InputName, InputParameter.DefaultValue);
	}

	protected override void OnInitialized()
	{
		Id = InputName?.ToLower().Replace(" ", "-") ?? null;
	}
}
