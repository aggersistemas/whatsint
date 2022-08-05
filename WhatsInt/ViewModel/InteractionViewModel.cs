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
        public string Asks1;
        public string Asks2;
        public string Asks3;

        public static List<string> listArray = new List<string>();


        public InteractionViewModel()
        {
            ; //Waiting for API adjustments for data request
        }

        public void saveQuestClick()
        {

            if (string.IsNullOrEmpty(Quest))
            {
                return;
            }
            else
            {
                listArray.Add(Quest);
                if (!string.IsNullOrEmpty(Asks1))
                    ;
                if (!string.IsNullOrEmpty(Asks2))
                    ;
                if (string.IsNullOrEmpty(Asks3))
                    ;
            }

            if (string.IsNullOrEmpty(Quest))
            {
                var isValid = "";

                return;
            }
            else
            {

            }
        }
    }
}
