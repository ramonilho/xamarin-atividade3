using System;
using System.Collections.Generic;

using Atividade3_Xamarin;
using Xamarin.Forms;

namespace Atividade3_Xamarin
{
    public partial class NewStudentView : ContentPage
    {
        private Guid studentId = Guid.NewGuid();

        #region Constructors
        public NewStudentView()
        {
            InitializeComponent();
        }

        public NewStudentView(Guid Id)
        {
            InitializeComponent();

            var student = App.StudentVM.StudentModel.GetStudent(Id);
            txtName.Text = student.Name;
            txtRM.Text = student.RM;
            txtEmail.Text = student.Email;
            isApproved.IsToggled = student.Approved;

            studentId = student.Id;
        }
        #endregion

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    txtName.Text = txtRM.Text = txtEmail.Text = string.Empty;
        //}

        public void onSaving(object sender, EventArgs args)
        {
            Student student = new Student()
            {
                Name = txtName.Text,
                RM = txtRM.Text,
                Email = txtEmail.Text,
                Approved = isApproved.IsToggled
            };

            App.StudentVM.StudentModel.Save(student);

            Navigation.PopAsync();
        }

        public void onCancel(object sender, EventArgs args) {
            resetFields();
            Navigation.PopAsync();
        }

        public void resetFields() {
            txtName.Text = txtRM.Text = txtEmail.Text = string.Empty;
            isApproved.IsToggled = false;
        }


    }
}

