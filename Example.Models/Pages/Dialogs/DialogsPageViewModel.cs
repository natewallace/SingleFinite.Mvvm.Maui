using SingleFinite.Mvvm;
using SingleFinite.Mvvm.Services;

namespace SingleFinite.Example.Models.Pages.Dialogs;

public partial class DialogsPageViewModel(
    IAppDialog dialog
) : ViewModel
{
    public void ShowFirstDialog()
    {
        dialog.Show<FirstDialogViewModel>();
    }

    public void CloseDialog()
    {
        dialog.Close(this);
    }
}
