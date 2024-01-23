using App.ViewModels;

namespace App.Views;

public partial class VoiceDetectorView : ContentPage
{
	private readonly VoiceDetectorViewModel _viewModel;
	public VoiceDetectorView(VoiceDetectorViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}
}