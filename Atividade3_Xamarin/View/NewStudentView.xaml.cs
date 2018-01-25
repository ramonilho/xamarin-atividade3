using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Atividade3_Xamarin
{
    public partial class NewStudentView : ContentPage
    {
        public NewStudentView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtName.Text = txtRM.Text = txtEmail.Text = string.Empty;
        }
    }
}
