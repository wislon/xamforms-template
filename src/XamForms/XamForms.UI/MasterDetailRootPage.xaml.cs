using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamForms.UI.ViewModels;

namespace XamForms.UI
{
  public partial class MasterDetailRootPage : MasterDetailPage
  {
    public MasterDetailRootPage()
    {
      InitializeComponent();

      listNav.ViewModel = new MasterListNavViewModel();
      normalOne.ViewModel = new NormalOneViewModel();
    }
  }
}
