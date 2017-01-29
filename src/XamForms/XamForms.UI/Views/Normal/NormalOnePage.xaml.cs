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
  public partial class NormalOnePage : ContentPage, IViewFor<NormalOneViewModel>
  {
    private NormalOneViewModel _vm;
    public NormalOneViewModel ViewModel
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
      set { ViewModel = (NormalOneViewModel)value; }
    }

    public NormalOnePage()
    {
      InitializeComponent();
    }
  }
}
