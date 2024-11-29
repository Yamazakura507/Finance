using SkiaSharp.Extended.UI.Controls;
using System.Runtime.CompilerServices;

namespace Finance.Classes
{
    public static class LottieAnimation
    {
        async public static void HideAnimationStart(SKLottieView lottie, int time = 1000)
        {
            lottie.IsAnimationEnabled = true;
            await Task.Delay(time);
            HideAnimationStop(lottie);
        }

        public static void HideAnimationStop(SKLottieView lottie)
        {
            lottie.IsAnimationEnabled = false;
        }

        async public static void BackButtonInNavClick(this Page page)
        {
            await ((Page)((Page)page.Parent).Parent).Navigation.PopAsync();
        }

        async public static void BackButtonClick(this Page page)
        {
            await ((Page)page.Parent).Navigation.PopAsync();
        }
    }
}
