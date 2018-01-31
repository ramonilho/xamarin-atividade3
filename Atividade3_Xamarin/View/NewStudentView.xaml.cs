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
            Title = App.StudentVM.isEditing ? "Edit student info" : "Add new student";
        }

        public NewStudentView(Guid Id)
        {
            InitializeComponent();

            Student student = App.StudentVM.StudentModel.GetStudent(Id);
            txtName.Text = student.Name;
            txtRM.Text = student.RM;
            txtEmail.Text = student.Email;
            isApproved.IsToggled = student.Approved;

            studentId = student.Id;
        }
        #endregion

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            resetFields();
        }

        public void resetFields() {
            txtName.Text = txtRM.Text = txtEmail.Text = string.Empty;
            isApproved.IsToggled = false;
            App.StudentVM.isEditing = false;
        }


    }
}

