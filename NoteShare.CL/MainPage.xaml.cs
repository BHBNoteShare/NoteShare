namespace NoteShare.CL
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            blazorWebView.StartPath = "/login";
        }
    }
}
