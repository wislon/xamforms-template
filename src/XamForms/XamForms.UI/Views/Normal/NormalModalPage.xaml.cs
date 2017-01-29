using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using XamForms.UI.ViewModels;

namespace XamForms.UI.Views.Normal
{
  public partial class NormalModalPage : ContentPage, IViewFor<NormalModalViewModel>
  {
    private NormalModalViewModel _vm;

    public NormalModalViewModel ViewModel
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
      set
      {
        ViewModel = (NormalModalViewModel)value;
      }
    }

    public NormalModalPage()
    {
      InitializeComponent();
    }
  }
}
