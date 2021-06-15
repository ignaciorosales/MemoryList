using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using memoryList.Models;
using memoryList.Views;
using System.Windows.Input;
using System.Linq;

namespace memoryList.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ICommand DeleteItemCommand { get; }

        public ItemsViewModel()
        {
            try
            {
                Title = "LISTADO";
                Items = new ObservableCollection<Item>();
                LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
                DeleteItemCommand = new XCommand<Item>(ExecuteDeleteItemCommand);

                MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
                {
                    var newItem = item as Item;
                    Items.Add(newItem);
                    await App.databaseService.InsertAsync(newItem);
                });
            }
            catch (Exception ex)
            {

            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await App.databaseService.GetAllAsync<Item>();
                foreach (var item in items)
                {
                    Items.Add(item);
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

        private async void ExecuteDeleteItemCommand(Item item)
        {
            Items.Remove(item);
            await App.databaseService.DeleteAsync(item);
        }
    }
}