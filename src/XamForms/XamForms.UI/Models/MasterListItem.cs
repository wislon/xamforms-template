using XamForms.UI.Interfaces;
using XamForms.UI.ViewModels;

namespace XamForms.UI.Models
{
  public class MasterListItem<T> : IMasterListItem<T> where T : BaseViewModel
  {
    public string DisplayName { get; set; }

    public MasterListItem(string displayName)
    {
      DisplayName = displayName;
    }
  }
}