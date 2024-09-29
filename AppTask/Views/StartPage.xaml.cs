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

	public void LoadData()
	{
		var tasks = _repository.GetAll();
		CollectionVewTasks.ItemsSource = tasks;
		LblEmptyText.IsVisible = tasks.Count <= 0;
	}

    private void OnButtonClickedToAdd(object sender, EventArgs e)
    {
		Navigation.PushModalAsync(new AddEditTaskPage());
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

    private void OnCheckBoxClickedToComplete(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
		var task = (TaskModel)e.Parameter;
		task.IsCompleted = ((CheckBox)sender).IsChecked;
		_repository.Update(task);
    }

    private void OnTapToEdit(object sender, TappedEventArgs e)
    {
		var task = (TaskModel)e.Parameter;

		Navigation.PushModalAsync(new AddEditTaskPage(_repository.GetById(task.Id)));
    }
}