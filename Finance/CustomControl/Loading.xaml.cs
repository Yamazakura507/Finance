using CommunityToolkit.Maui.Views;
using SkiaSharp.Extended.UI.Controls;
using System.ComponentModel;

namespace Finance.CustomControl;

public partial class Loading : Popup
{
    public bool IsNetworking = false;
    public BackgroundWorker LoadingBackgorundWorker = new BackgroundWorker();

    private SKFileLottieImageSource[] loadingsFiles = 
		{ 
			new SKFileLottieImageSource() { File = "Loading.json" },
			new SKFileLottieImageSource() { File = "Loading2.json" },
			new SKFileLottieImageSource() { File = "Loading3.json" },
			new SKFileLottieImageSource() { File = "Loading4.json" },
			new SKFileLottieImageSource() { File = "Loading5.json" }
		};

    public Loading(bool isNetwork = false)
	{
		InitializeComponent();

        if (isNetwork)
        {
            loadingsFiles = new []{ new SKFileLottieImageSource() { File = "NoNetworking.json" } };
        }

        this.IsNetworking = isNetwork;

        Loaded();
	}

    private void Loaded()
    {
        loadingLottie.WidthRequest = loadingLottie.HeightRequest = DeviceDisplay.MainDisplayInfo.Width/5;

        LoadingBackgorundWorker.DoWork += new DoWorkEventHandler(bw_DoWork);
        LoadingBackgorundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
    }

    void bw_DoWork(object sender, DoWorkEventArgs e)
    {
        loadingLottie.Source = loadingsFiles[new Random().Next(0, loadingsFiles.Length)];
        Thread thread = (Thread)e.Argument;

        thread.Start();
        thread.Join();
    }

    void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (this.IsNetworking && Connectivity.NetworkAccess != NetworkAccess.Internet)
            return;

        this.Close();
    }
}