using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Views;

namespace Application.MauiBlazor.Platforms.Android.PageRepositories
{
    public interface IOnPageKeyDown
    {
        bool OnPageKeyDown(Keycode keyCode);
    }
}
