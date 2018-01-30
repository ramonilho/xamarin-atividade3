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
                return new ObservableCollection<Student>(StudentModel.FetchStudentList());
            }
        }

        // UI Events
        public OnAddStudentCMD OnAddStudentCMD { get; }
        public ICommand OnNewCMD { get; private set; }
        public ICommand OnExitCMD { get; private set; }

        public StudentViewModel()
        {
            StudentModel = new Student();
            OnAddStudentCMD = new OnAddStudentCMD(this);
            OnExitCMD = new Command(OnExit);
            OnNewCMD = new Command(OnNew);
        }

        public void Add(Student paramStudent)
        {
            try
            {
                if (paramStudent == null)
                    throw new Exception("Invalid user");

                paramStudent.Id = Guid.NewGuid();

                Students.Add(paramStudent);

                App.StudentVM.StudentModel.Save(paramStudent);

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



}
