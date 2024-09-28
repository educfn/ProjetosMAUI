using AppTask.Models;

namespace AppTask.Views;

public partial class AddEditTaskPage : ContentPage
{
	private TaskModel _task;
	public AddEditTaskPage()
	{
		InitializeComponent();
		_task = new TaskModel();

		BindableLayout.SetItemsSource(BindableLayout_Steps, _task.SubTasks);
	}

    private void CloseModal(object sender, EventArgs e)
	{
		//Obter os dados
		GetDataFromForm();
		//Validar os dados

		//Salvar os dados

		//Fechar a tela

		//Solicitar a atualiza��o da listagem da tela anterior.
		Navigation.PopModalAsync();
	}

	private void GetDataFromForm()
	{
		_task.Name = Entry_TaskName.Text;
		_task.Name = Editor_TaskDescription.Text;
		_task.PrevisionDate = DatePicker_TaskDate.Date;
		_task.PrevisionDate = _task.PrevisionDate.AddHours(23);
		_task.PrevisionDate = _task.PrevisionDate.AddMinutes(59);
		_task.PrevisionDate = _task.PrevisionDate.AddSeconds(59);

		_task.Created = DateTime.Now;
		_task.IsCompleted = false;
	}

    private void SaveData(object sender, EventArgs e)
	{
		Navigation.PopModalAsync();
	}

	private async void AddStep(object sender, EventArgs e)
	{
		var stepName = await DisplayPromptAsync("Etapa(subtarefa)", "Digite o nome da etapa(subtarefa):", "Adicionar", "Cancelar");

		if (!string.IsNullOrWhiteSpace(stepName))
		{
			_task.SubTasks.Add(new SubTaskModel { Name = stepName, IsCompleted = false});
		}	
	}

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

		DatePicker_TaskDate.WidthRequest = width - 30;
    }
	
	private void OnTapToDelete(object width, TappedEventArgs e)
    {
        _task.SubTasks.Remove((SubTaskModel)e.Parameter);
    }
}