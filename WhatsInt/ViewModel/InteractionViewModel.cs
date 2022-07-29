using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MvvmBlazor.ViewModel;
using WhatsInt.Model;
using WhatsInt.Pages;

namespace WhatsInt.ViewModel
{
    public class InteractionViewModel : ViewModelBase
    {
        public static bool IsFormVisible = false;
        public string Quest;

        public void saveQuestClick()
        {
            if (string.IsNullOrEmpty(Quest))
                ;
        }
    }
}
