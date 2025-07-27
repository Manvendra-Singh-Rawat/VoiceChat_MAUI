namespace MAUI_Client
{
    public partial class MainPage : ContentPage
    {
        private RTPClient _RTPClient;
        private AudioInputManager _AudioInputManager;
        private AudioOutputManager _AudioOutputManager;

        public MainPage()
        {
            InitializeComponent();
        }

        public void OnConnectClicked(object sender, EventArgs e)
        {
            _AudioInputManager = new AudioInputManager();
            _AudioOutputManager = new AudioOutputManager();

            _RTPClient = new RTPClient("127.0.0.1", 3000, _AudioInputManager, _AudioOutputManager);
        }
    }
}
