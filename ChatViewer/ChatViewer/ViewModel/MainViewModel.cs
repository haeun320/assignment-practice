using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ChatViewer.ViewModel
{
    public partial class MainViewModel : ObservableObject 
    {
        [ObservableProperty]
        private string _title = "Test";

        private ICommand _buttonCommand;
        public ICommand ButtonCommand => _buttonCommand ??= new RelayCommand(ExecuteButtonCommand);
        public MainViewModel()
        {

        }

        private void ExecuteButtonCommand()
        {
            Title = "Button Clicked!";
        }
    }
}
