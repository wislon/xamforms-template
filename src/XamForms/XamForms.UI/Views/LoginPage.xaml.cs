using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using XamForms.UI.ViewModels;

namespace XamForms.UI.Views
{
  public partial class LoginPage : ContentPage, IViewFor<LoginViewModel>
  {
    private LoginViewModel _vm;

    public LoginViewModel ViewModel
    {
      get { return _vm; }
      set
      {
        _vm = value;
        BindingContext = _vm;
      }
    }

    object IViewFor.ViewModel
    {
      get { return _vm; }
      set { ViewModel = (LoginViewModel)value; }
    }


    public LoginPage()
    {
      InitializeComponent();
      // if this is the 'MainPage', loaded as view-first,
      // then you'll need to set the ViewModel.
      // If you're driving it with ViewModel first, then 
      // this should already be set by the time you get here.
      if (_vm == null)
      {
        ViewModel = new LoginViewModel();
      }
    }

  }
}
