using AppTask.Repositories;

namespace AppTask.Views;

public partial class StartPage : ContentPage
{
	private ITaskModelRepository _repository;
	public StartPage()
	{
		InitializeComponent();

		//TODO - Ponto de melhoria -> Implementa usando D.I.
		_repository = new TaskModelRepository();

		LoadData();
	}

	private void LoadData()
	{
		var tasks = _repository.GetAll();
		CollectionVewTasks.ItemsSource = tasks;
		LblEmptyText.IsVisible = tasks.Count <= 0;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		_repository.Add(new Models.TaskModel
		{
			Name = "Comprar Frutas",
			Description = "Comprar abacate, laranja, maçã...",
			IsCompleted = false,
			Created = DateTime.Now,
			PrevisionDate = DateTime.Now.AddDays(2),
		});

		LoadData();
		//Navigation.PushModalAsync(new AddEditTaskPage());
    }

	private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
		Entry_Search.Focus();
    }
}