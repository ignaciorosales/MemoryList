using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using memoryList.Models;
using memoryList.Views;
using Xamarin.Forms;

namespace memoryList.ViewModels
{
    public class OthersViewModel : BaseViewModel
    {
        public ObservableCollection<Other> Others { get; set; }
        public Command LoadOthersCommand { get; set; }
        public ICommand DeleteOtherCommand { get; }

        public OthersViewModel()
        {
            try
            {
                Title = "OTROS";
                Others = new ObservableCollection<Other>();
                LoadOthersCommand = new Command(async () => await ExecuteLoadOthersCommand());
                DeleteOtherCommand = new XCommand<Other>(ExecuteDeleteOtherCommand);

                MessagingCenter.Subscribe<NewOtherPage, Other>(this, "AddOther", async (obj, other) =>
                {
                    var newOther = other as Other;
                    Others.Add(newOther);
                    await App.databaseService.InsertAsync(newOther);
                });
            }
            catch (Exception ex)
            {

            }
        }

        async Task ExecuteLoadOthersCommand()
        {
            IsBusy = true;

            try
            {
                Others.Clear();
                var others = await App.databaseService.GetAllAsync<Other>();
                foreach (var other in others)
                {
                    Others.Add(other);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ExecuteDeleteOtherCommand(Other other)
        {
            Others.Remove(other);
            await App.databaseService.DeleteAsync(other);
        }
    }
}
