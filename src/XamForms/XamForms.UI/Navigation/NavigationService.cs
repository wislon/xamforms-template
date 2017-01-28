using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using XamForms.UI.Interfaces;
using XamForms.UI.ViewModels;

namespace XamForms.UI.Navigation
{
  public class NavigationService : INavigationService
  {
    private INavigation FormsNavigation
    {
      get
      {
        var tabController = Application.Current.MainPage as TabbedPage;
        var masterController = Application.Current.MainPage as MasterDetailPage;

        // First check to see if we're on a tabbed page, then master detail, finally go to overall fallback
        return tabController?.CurrentPage?.Navigation ??
                   (masterController?.Detail as TabbedPage)?.CurrentPage?.Navigation ?? // special consideration for a tabbed page inside master/detail
                   masterController?.Detail?.Navigation ??
                   Application.Current.MainPage.Navigation;
      }
    }

    // View model to view lookup - making the assumption that view model to view will always be 1:1
    // building this at runtime will require a check for the device idiom (phone, tablet, etc.)
    private readonly Dictionary<Type, Type> _viewModelViewDictionary = new Dictionary<Type, Type>();

    // Because we're going to do a hard switch of the page, either return
    // the detail page, or if that's null, then the current main page		
    private Page DetailPage
    {
      get
      {
        var masterController = Application.Current.MainPage as MasterDetailPage;

        return masterController?.Detail ?? Application.Current.MainPage;
      }
      set
      {
        var masterController = Application.Current.MainPage as MasterDetailPage;

        if (masterController != null)
        {
          masterController.Detail = value;
          masterController.IsPresented = false;
        }
        else
        {
          Application.Current.MainPage = value;
        }
      }
    }

    public void SwitchDetailPage(BaseViewModel viewModel)
    {
      var view = InstantiateView(viewModel);

      Page newDetailPage;

      // Tab pages shouldn't go into navigation pages
      if (view is TabbedPage)
        newDetailPage = (Page)view;
      else
        newDetailPage = new NavigationPage((Page)view);

      DetailPage = newDetailPage;
    }

    public void SwitchDetailPage<T>(Action<T> initialize = null) where T : BaseViewModel
    {
      var viewModel = Activator.CreateInstance<T>();
      initialize?.Invoke(viewModel);
      SwitchDetailPage(viewModel);
    }

    public void RegisterViewModels(System.Reflection.Assembly asm)
    {
      // Loop through everything in the assembley that implements IViewFor<T>
      foreach (var type in asm.DefinedTypes.Where(dt => !dt.IsAbstract &&
        dt.ImplementedInterfaces.Any(ii => ii == typeof(IViewFor))))
      {

        // Get the IViewFor<T> portion of the type that implements it
        var viewForType = type.ImplementedInterfaces.FirstOrDefault(
          ii => ii.IsConstructedGenericType &&
          ii.GetGenericTypeDefinition() == typeof(IViewFor<>));

        // Register it, using the T as the key and the view as the value
        Register(viewForType.GenericTypeArguments[0], type.AsType());
      }
    }

    /// <summary>
    /// TODO move these registrations into Splat, then we can have multiple
    /// TODO View registrations for the same viewmodel using the "contract" parameter
    /// </summary>
    /// <param name="viewModelType"></param>
    /// <param name="viewType"></param>
    public void Register(Type viewModelType, Type viewType)
    {
      _viewModelViewDictionary.Add(viewModelType, viewType);
    }

    public async Task PopAsync()
    {
      await FormsNavigation.PopAsync(true);
    }

    public async Task PopModalAsync()
    {
      await FormsNavigation.PopModalAsync(true);
    }

    public async Task PopToRootAsync(bool animate)
    {
      await FormsNavigation.PopToRootAsync(animate);
    }

    public async Task PushAsync(BaseViewModel viewModel)
    {
      var view = InstantiateView(viewModel);

      await FormsNavigation.PushAsync((Page)view);
    }

    public async Task PushModalAsync(BaseViewModel viewModel)
    {
      var view = InstantiateView(viewModel);

      // Most likely we're going to want to put this into a navigation 
      // page so we can have a title bar on it
      var nv = new NavigationPage((Page)view);

      await FormsNavigation.PushModalAsync(nv);
    }

    public async Task PushAsync<T>(Action<T> initialize = null) where T : BaseViewModel
    {
      // Instantiate the view model & invoke the initialize method, if any
      var viewModel = Activator.CreateInstance<T>();
      initialize?.Invoke(viewModel);

      await PushAsync(viewModel);
    }

    public async Task PushModalAsync<T>(Action<T> initialize = null) where T : BaseViewModel
    {
      // Instantiate the view model & invoke the initialize method, if any
      var viewModel = Activator.CreateInstance<T>();
      initialize?.Invoke(viewModel);

      await PushModalAsync(viewModel);
    }

    private IViewFor InstantiateView(BaseViewModel viewModel)
    {
      var viewModelType = viewModel.GetType();
      var viewType = _viewModelViewDictionary[viewModelType];
      var view = (IViewFor)Activator.CreateInstance(viewType);

      view.ViewModel = viewModel;

      return view;
    }
  }
}