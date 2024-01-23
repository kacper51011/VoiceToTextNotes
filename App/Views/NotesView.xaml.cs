using App.ViewModels;

namespace App.Views;

public partial class NotesView : ContentPage
{
	private readonly NotesViewModel _viewModel;
	public NotesView(NotesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}
}