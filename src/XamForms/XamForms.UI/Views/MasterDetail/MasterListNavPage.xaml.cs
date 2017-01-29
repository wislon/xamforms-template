using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using XamForms.UI.ViewModels;

namespace XamForms.UI.Views.MasterDetail
{
  public partial class MasterListNavPage : ContentPage, IViewFor<MasterListNavViewModel>
  {

    private MasterListNavViewModel _vm;
    public MasterListNavViewModel ViewModel
    {
      get
      {
        return _vm;
      }

      set
      {
        _vm = value;
        BindingContext = _vm;
      }
    }

    object IViewFor.ViewModel
    {
      get
      {
        return _vm;
      }

      set
      {
        ViewModel = (MasterListNavViewModel)value;
      }
    }

    public MasterListNavPage()
    {
      InitializeComponent();
    }

  }
}
