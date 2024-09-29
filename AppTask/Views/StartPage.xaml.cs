using AppTask.Models;
using AppTask.Repositories;

namespace AppTask.Views;

public partial class StartPage : ContentPage
{
	private ITaskModelRepository _repository;
	private IList<TaskModel> _tasks;
	public StartPage()
	{
		InitializeComponent();

		//TODO - Ponto de melhoria -> Implementa usando D.I.
		_repository = new TaskModelRepository();

		LoadData();
	}

	public void LoadData()
	{
		_tasks = _repository.GetAll();
		CollectionVewTasks.ItemsSource = _tasks;
		LblEmptyText.IsVisible = _tasks.Count <= 0;
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

    private void OnTextChanged_FilterList(object sender, TextChangedEventArgs e)
    {
		var word = e.NewTextValue;
		CollectionVewTasks.ItemsSource = _tasks.Where(a => a.Name.ToLower().Contains(word.ToLower())).ToList();
    }
}