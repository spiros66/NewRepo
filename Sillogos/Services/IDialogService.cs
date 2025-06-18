using Sillogos.ViewModels;

namespace Sillogos.Services;

public interface IDialogService
{
    bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : class;
}
