using ChatViewer.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ChatViewer.ViewModel
{
    public partial class MainViewModel : ObservableObject 
    {
        [ObservableProperty]
        private string _fileName;

        [ObservableProperty]
        private ObservableCollection<ChatLog> _chatLog;

        [RelayCommand]
        public void FileOpen()
        {
            ExecuteFileOpen();
        }


        public MainViewModel()
        {
            
        }

        private void ExecuteFileOpen()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
                ReadFile(FileName);
            }
        }

        private void ReadFile(string path)
        {
            ChatLog = new ObservableCollection<ChatLog>();
            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path, Encoding.UTF8))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        ChatLog log = new ChatLog(line);

                        if (log.IsValid)
                        {
                            ChatLog.Add(log);
                            Debug.WriteLine($"{log.Time}, {log.Sender}, {log.Message}");
                        }
                    }
                }
            }
        }
    }
}
