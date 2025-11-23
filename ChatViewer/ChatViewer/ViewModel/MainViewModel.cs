using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Diagnostics;
using ChatViewer.Model;

namespace ChatViewer.ViewModel
{
    public partial class MainViewModel : ObservableObject 
    {
        [ObservableProperty]
        public string _fileName;

        [ObservableProperty]
        public List<ChatLog> _chatLog;

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
            ChatLog = new List<ChatLog>();
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
