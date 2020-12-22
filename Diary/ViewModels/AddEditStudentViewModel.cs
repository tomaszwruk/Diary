﻿using Diary.Commands;
using Diary.Models;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    class AddEditStudentViewModel : ViewModelBase
    {
        //private Student student;

        private Student _student;

        public Student Student
        {
            get { return _student; }
            set
            {
                _student = value;
                OnPropertyChanged();
            }
        }
        
        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }
        }




        public AddEditStudentViewModel(Student student = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);


            if (student == null) //IsUpdate domyślnie jest false - wtedy jest dodawanie
            {
                Student = new Student();
            }
            else
            {
                Student = student;
                IsUpdate = true;
            }

            InitGroups();

        }

        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>
            {
                new Group
                {
                    ID = 0,
                    Nazwa = "-- brak --"
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
            Student.Group.ID = 0;
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



        private void Confirm(object obj)
        {
            if (!IsUpdate)
            {
                AddStudent();
            }
            else
            {
                UpdateStudent();
            }


            CloseWindow(obj as Window);
        }

        private void UpdateStudent()
        {
            // baa danych       
        }

        private void AddStudent()
        {
            //baza danych     
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

    }
}
