namespace AppTask.Views;

public partial class StartPage : ContentPage
{
	public StartPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Navigation.PushModalAsync(new AddEditTaskPage());
    }

	private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
		Entry_Search.Focus();
    }
}