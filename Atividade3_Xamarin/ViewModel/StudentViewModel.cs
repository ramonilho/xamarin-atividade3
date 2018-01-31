using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Atividade3_Xamarin
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        // Properties
        public Student StudentModel { get; set; }
        public ObservableCollection<Student> Students
        {
            get
            {
                return new ObservableCollection<Student>(StudentModel.FetchStudentList().OrderBy(st => st.Name.ToLower())); // sorting list
            }
        }

        // UI Events
        public OnAddStudentCMD OnAddStudentCMD { get; }
        public OnDeleteStudentCMD OnDeleteStudentCMD { get; }
        public ICommand OnNewCMD { get; private set; }
        public ICommand OnExitCMD { get; private set; }

        // Helpers
        public bool isEditing { get; set; }

        public StudentViewModel()
        {
            StudentModel = new Student();
            OnAddStudentCMD = new OnAddStudentCMD(this);
            OnDeleteStudentCMD = new OnDeleteStudentCMD(this);
            OnNewCMD = new Command(OnNew);
            OnExitCMD = new Command(OnExit);
        }

        public void Add(Student paramStudent)
        {
            try
            {
                if (paramStudent == null)
                    throw new Exception("Invalid user");

                if (paramStudent.Id.Equals(Guid.Empty)) {
                    // If this is empty, means that is a new student
                    paramStudent.Id = Guid.NewGuid();
                }

                // Fetch student from database if exists
                Student fetchedStudent = App.StudentVM.StudentModel.GetStudent(paramStudent.Id);

                // Set properties
                fetchedStudent.Name = paramStudent.Name;
                fetchedStudent.RM = paramStudent.RM;
                fetchedStudent.Email = paramStudent.Email;
                fetchedStudent.Approved = paramStudent.Approved;

                // Save
                App.StudentVM.StudentModel.Save(fetchedStudent);

                // Back to previous page
                App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {
                App.Current.MainPage.DisplayAlert("Failed", "Unexpected error", "OK");
            }
        }

        public void Delete(Student paramStudent) {
            try {
                if (paramStudent == null)
                    throw new Exception("Invalid user");

                // Fetch student from database if exists
                App.StudentVM.StudentModel.DeleteStudent(paramStudent.Id);

                // Back to previous page
                App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {
                App.Current.MainPage.DisplayAlert("Failed", "Unexpected error", "OK");
            }
        }

        private async void OnExit()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void OnNew()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewStudentView() { BindingContext = App.StudentVM });
        }

        // Event that updates callers on changing some property
        public event PropertyChangedEventHandler PropertyChanged;
        private void EventPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class OnAddStudentCMD : ICommand
    {
        StudentViewModel studentVM;

        public OnAddStudentCMD(StudentViewModel paramVM)
        {
            studentVM = paramVM;
        }

        public event EventHandler CanExecuteChanged;

        public void DeleteCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter)
        {
            if (parameter != null) return true;

            return false;
        }

        public void Execute(object parameter)
        {
            studentVM.Add(parameter as Student);
        }
    }

    public class OnDeleteStudentCMD : ICommand
    {
        StudentViewModel studentVM;

        public OnDeleteStudentCMD(StudentViewModel paramVM)
        {
            studentVM = paramVM;
        }

        public event EventHandler CanExecuteChanged;

        public void DeleteCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter)
        {
            if (parameter != null) return true;

            return false;
        }

        public void Execute(object parameter)
        {
            studentVM.Delete(parameter as Student);
        }
    }



}
