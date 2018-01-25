using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Atividade3_Xamarin
{
    public partial class App : Application
    {

        public static StudentViewModel StudentVM { get; set; }

        public App()
        {
            InitializeComponent();
            SetupStudent();

            MainPage = new NavigationPage(new StudentView() { BindingContext = App.StudentVM });
           
        }

        public void SetupStudent() {
            if (StudentVM == null) StudentVM = new StudentViewModel();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
