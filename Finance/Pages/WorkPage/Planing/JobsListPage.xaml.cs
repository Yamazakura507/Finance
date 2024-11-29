using CommunityToolkit.Maui.Views;
using Finance.Classes;
using Finance.Classes.Enums;
using Finance.CustomControl;
using Finance.Pages.Tabbed;
using System.Collections.ObjectModel;

namespace Finance.Pages.WorkPage.Planing;

public partial class JobsListPage : ContentPage
{
    ObservableCollection<View.Jobs> ViewJobs;
    Loading loading { get; set; }

    public JobsListPage()
    {
        InitializeComponent();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() =>
        {
            try
            {
                ViewJobs = DBModel.GetCollectionModel<View.Jobs>($"SELECT j.*FROM `Jobs` j INNER JOIN `TaskJobs` tj ON(tj.`IdTask`, tj.IdJob) = ('{((Models.Tasks)this.BindingContext).Id}', j.`Id`)");

                if (ViewJobs is null || ViewJobs.Count() == 0) throw new Exception("У вас отсутствуют работы");
                else
                {
                    MainThread.BeginInvokeOnMainThread(() => BindableLayout.SetItemsSource(debVSL, ViewJobs));
                }
            }
            catch (Exception ex) 
            {
                MainThread.BeginInvokeOnMainThread(() => ErrProvider.WorkProvider(ProviderType.Error, ex.Message));
            }
        }));
    }
    private void DeleteJobs_Pressed(object sender, EventArgs e)
    {
        loading = new Loading();

        this.ShowPopup(loading);

        var job = (View.Jobs)((ImageButton)sender).BindingContext;

        loading.LoadingBackgorundWorker.RunWorkerAsync(new Thread(() => DBModel.GetModel<Models.Jobs>(job.Id).DeleteModel<Models.Jobs>()));

        ViewJobs.Remove(job);
    }
    async private void AddJobs_Pressed(object sender, EventArgs e) => await Navigation.PushAsync(new NavigationPage(new JobInfo() { BindingContext = null, IdTask = ((Models.Tasks)this.BindingContext).Id }));
    async private void Jobs_Tapped(object sender, TappedEventArgs e)
    {
        var job = (View.Jobs)((ContentView)sender).BindingContext;

        await Navigation.PushAsync(new NavigationPage(new JobInfo() { BindingContext = DBModel.GetModel<Models.Jobs>(job.Id), IdTask = ((Models.Tasks)this.BindingContext).Id }));
    }
}