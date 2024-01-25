using App.ViewModels;

namespace App.Views;

public partial class GuideView : ContentPage
{
	private readonly GuideViewModel _viewModel;
	public GuideView(GuideViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = _viewModel = viewModel;
	}
}