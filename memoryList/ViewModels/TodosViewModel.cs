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
    public class TodosViewModel : BaseViewModel
    {
        public ObservableCollection<Todo> Todos { get; set; }
        public Command LoadTodosCommand { get; set; }
        public ICommand DeleteTodoCommand { get; }

        public TodosViewModel()
        {
            try
            {
                Title = "TO DO's";
                Todos = new ObservableCollection<Todo>();
                LoadTodosCommand = new Command(async () => await ExecuteLoadTodosCommand());
                DeleteTodoCommand = new XCommand<Todo>(ExecuteDeleteTodoCommand);

                MessagingCenter.Subscribe<NewTodoPage, Todo>(this, "AddTodo", async (obj, todo) =>
                {
                    var newTodo = todo as Todo;
                    Todos.Add(newTodo);
                    await App.databaseService.InsertAsync(newTodo);
                });
            }
            catch (Exception ex)
            {

            }
        }

        async Task ExecuteLoadTodosCommand()
        {
            IsBusy = true;

            try
            {
                Todos.Clear();
                var todos = await App.databaseService.GetAllAsync<Todo>();
                foreach (var todo in todos)
                {
                    Todos.Add(todo);
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

        private async void ExecuteDeleteTodoCommand(Todo todo)
        {
            Todos.Remove(todo);
            await App.databaseService.DeleteAsync(todo);
        }
    }
}
