using Diary.Commands;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            RefreshStudentCommand = new RelayCommand(RefreshStudents, CanRefreshStudents);
        }

        private string _test;

        public string Test
        {
            get { return _test; }
            set 
            { 
                _test = value;
                OnPropertyChanged(); //odswieża na wikdoku
            }
        }


        public ICommand RefreshStudentCommand {get; set;}

        private void RefreshStudents(object obj)
        {
            MessageBox.Show("Refstudents");
        }

        private bool CanRefreshStudents(object obj)
        {
            return true;
        }



    }
}
