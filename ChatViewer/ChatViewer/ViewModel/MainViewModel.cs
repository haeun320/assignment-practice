using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace ChatViewer.ViewModel
{
    public partial class MainViewModel : ObservableObject 
    {
        [ObservableProperty]
        public string _fileName;

        [RelayCommand]
        public void FileOpen()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
            }
        }

        public MainViewModel()
        {

        }
    }
}
