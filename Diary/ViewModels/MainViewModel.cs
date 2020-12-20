using Diary.Commads;
using Diary.Commands;
using Diary.Models;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentCommand = new RelayCommand(RefreshStudents);


            InitGroups();
            RefreshDiary();

        }



        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group
                {
                    ID = 0,
                    Nazwa = "Wsystkie"
                },

                new Group
                {
                    ID = 1,
                    Nazwa = "Gr1"
                },

                new Group
                {
                    ID = 1,
                    Nazwa = "Gr2"
                }
            };
            SelectedGroupId = 0;
        }

        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set 
            { 
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        //ObservableCollection w ModelViews używa się zamiast List, to te jest Lista
        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _groups;

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set 
            { 
                _groups = value;
                OnPropertyChanged();
            }
        }



        public ICommand DeleteStudentCommand {get; set;}
        public ICommand RefreshStudentCommand { get; }
        public ICommand AddStudentCommand { get; }
        public ICommand EditStudentCommand { get; }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            //await metroWindow.ShowMessageAsync("Usunąć ucznia", $"Czy usunąć ucznia {SelectStudent.FirstName} {SelectStudent.LastName}?");
            var dialog = await metroWindow.ShowMessageAsync("Usunąć ucznia", $"Czy usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
            {
                return;
            }

            //usuwanie ucznia 

            RefreshDiary();
        }

        private bool CanEditDeleteStudent(object obj)
        {

            return SelectedStudent != null;
        }

        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as Student);
            addEditStudentWindow.Closed += addEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void addEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<Student>
            {
                new Student
                {
                    FirstName = "Tomasz",
                    LastName = "Wruk",
                    Group = new Group { ID = 1 }
                },

                new Student
                {
                    FirstName = "Tymon",
                    LastName = "Wruk",
                    Group = new Group { ID = 1 }
                },

                new Student
                {
                    FirstName = "Mirek",
                    LastName = "Janicki",
                    Group = new Group { ID = 1 }
                },
            };
        }

    }
}
