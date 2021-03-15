using BankingSystem.WPF.State.Navigators;

namespace BankingSystem.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigator navigator)
        {
            Navigator = navigator;
        }

        public INavigator Navigator { get; set; }
    }
}
