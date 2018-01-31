using System;
using System.Collections.Generic;
using Xamarin.Forms;

using Atividade3_Xamarin;

namespace Atividade3_Xamarin
{
    public partial class StudentView : ContentPage
    {
        public StudentView()
        {
            InitializeComponent();
            this.Title = "Exercise 3 - SQL";

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.lstStudents.ItemsSource = App.StudentVM.Students;
        }

        void OnSelectStudentCell(object sender, System.EventArgs e)
        {
            var student = ((Student) ((ViewCell)sender).BindingContext);

            App.StudentVM.isEditing = true;
            App.StudentVM.StudentModel = student;

            App.Current.MainPage.Navigation.PushAsync(new NewStudentView(){ BindingContext = App.StudentVM });
        }

    }
}
