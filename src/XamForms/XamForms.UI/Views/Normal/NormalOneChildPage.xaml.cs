﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;
using XamForms.UI.ViewModels;

namespace XamForms.UI.Views.Normal
{
  public partial class NormalOneChildPage : ContentPage, IViewFor<NormalOneChildViewModel>
  {
    private NormalOneChildViewModel _vm;
    public NormalOneChildViewModel ViewModel
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
      set { ViewModel = (NormalOneChildViewModel)value; }
    }

    public NormalOneChildPage()
    {
      InitializeComponent();
    }
  }
}
