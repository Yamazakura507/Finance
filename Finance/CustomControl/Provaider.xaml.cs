using CommunityToolkit.Mvvm.ComponentModel;
using Finance.Classes;
using Finance.Classes.Enums;
using SkiaSharp.Extended.UI.Controls;

namespace Finance.CustomControl;

public partial class Provaider : ContentView
{
    private ProviderType providerType;

    private const int alfaBorder = 70;
    private const int alfaBackground = 30;

    private Dictionary<ProviderType, object[]> ProviderParametr = new Dictionary<ProviderType, object[]>()
    {
        {
            ProviderType.Alert,
            new object[]
            {
                new SKFileLottieImageSource() { File = "AlertProvader.json" },
                Color.FromRgba(255, 200, 0, alfaBorder),
                Color.FromRgba(255, 200, 0, alfaBackground)
            }
        },
        {
            ProviderType.Error,
            new object[]
            {
                new SKFileLottieImageSource() { File = "ErrorProvaider.json" },
                Color.FromRgba(179, 15, 15, alfaBorder),
                Color.FromRgba(179, 15, 15, alfaBackground)
            }
        },
        {
            ProviderType.Info,
            new object[]
            {
                new SKFileLottieImageSource() { File = "InfoProvader.json" },
                Color.FromRgba(0, 120, 194, alfaBorder),
                Color.FromRgba(0, 120, 194, alfaBackground)
            }
        }
    };

    public static readonly BindableProperty MessageProperty = BindableProperty.Create(nameof(Message), typeof(string), typeof(Provaider), string.Empty);
    
    public string Message
    {
        get => (string)GetValue(Provaider.MessageProperty);
        set => SetValue(Provaider.MessageProperty, value);
    }
    public ProviderType TypeProvider 
    { 
        get => providerType;
        set
        {
            SourceProvider = (SKLottieImageSource)ProviderParametr[value][0];
            BorderMessageBackgroundColor = (Color)ProviderParametr[value][1];
            BackgroundMessageColor = (Color)ProviderParametr[value][2];

            providerType = value;
        }
    }

    private SKLottieImageSource SourceProvider
    {
        set => LottieProvider.Source = value;
    }
    private Color BorderMessageBackgroundColor
    {
        set => BrMessage.Stroke = value;
    }
    private Color BackgroundMessageColor
    {
        set => BrMessage.BackgroundColor = value;
    }

    public Provaider()
	{
		InitializeComponent();
	}

    async private void ProviderOnTapped(object sender, TappedEventArgs e)
    {
        try
        {
            LottieProvider.Progress = new TimeSpan(0);

            #if ANDROID || IOS
                WindowMessage();
            #else
                BrMessage.IsVisible = true;
                while (BrMessage.Opacity < 1)
                {
                    await Task.Delay(10);
                    BrMessage.Opacity += 0.01;
                }

                await Task.Delay(3000);

                while (BrMessage.Opacity != 0)
                {
                    await Task.Delay(10);
                    BrMessage.Opacity -= 0.01;
                }
                BrMessage.IsVisible = false;
            #endif
        }
        catch (Exception){}
    }

    private void Loaded(object sender, EventArgs e)
    {
        if (this.Message.Length > 500)
        {
            this.IsVisible = false;

            WindowMessage();
        }
    }

    private void WindowMessage()
    {
        App.MyAppShell.Messege(Message, TypeProvider);
    }

    public void WorkProvider(ProviderType providerType, string message)
    {       
        this.IsVisible = true;
        this.TypeProvider = providerType;
        this.Message = message;
    }
}