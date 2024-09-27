using AppTask.Models;
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

    private void OnButtonClickedToAdd(object sender, EventArgs e)
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

	private void OnBorderClickedToFocusEntry(object sender, EventArgs e)
    {
		Entry_Search.Focus();
    }

    private async void OnImageClickedToDelete(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
		var task = (TaskModel)e.Parameter;
		var confirm = await DisplayAlert("Confirm a exclusão!",$"Tem certeza de que deseja excluir essa tarefa: {task.Name}?", "Sim", "Não");

		if (confirm)
		{
			_repository.Delete(task);
			LoadData();
		}
    }
}