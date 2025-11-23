using ChatViewer.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace ChatViewer.ViewModel
{
    public partial class MainViewModel : ObservableObject 
    {
        [ObservableProperty]
        private string _fileName;

        [ObservableProperty]
        private ObservableCollection<ChatLog> _chatLog;

        private List<ChatLog> _fullLog;

        [ObservableProperty]
        private string _searchParam;

        [RelayCommand]
        public void FileOpen()
        {
            ExecuteFileOpen();
        }

        [RelayCommand]
        public void Search()
        {
            ExecuteSearch();
        }


        public MainViewModel()
        {
            ChatLog = new ObservableCollection<ChatLog>();
            _fullLog = new List<ChatLog>();
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

        private void ExecuteSearch()
        {
            string param = SearchParam.Trim();

            var logQuery = from log in _fullLog
                           where log.Sender.Contains(param) || log.Message.Contains(param)
                           select log;

            ChatLog.Clear();
            foreach (var log in logQuery)
            {
                ChatLog.Add(log);
            }
        }

        private void ReadFile(string path)
        {
            try
            {
                ChatLog.Clear();
                _fullLog.Clear();

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
                                _fullLog.Add(log);
                                Debug.WriteLine($"{log.Time}, {log.Sender}, {log.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
