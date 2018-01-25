using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Atividade3_Xamarin
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        // -- Properties
        public Student StudentModel { get; set; }
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();

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

        public void Add(Student paramAluno)
        {
            try
            {
                if (paramAluno == null)
                    throw new Exception("Invalid user");

                paramAluno.Id = Guid.NewGuid();

                Students.Add(paramAluno);

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

        // Event that listens changes in property
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
