using Microsoft.AspNetCore.Components;
using MvvmBlazor;

namespace WhatsInt.ViewModel
{
    [MvvmComponent]
    public abstract partial class CustomBaseComponent<T> : CustomBaseComponent
    {

    }

    [MvvmComponent]
    public abstract partial class CustomBaseComponent : ComponentBase
    {

    }
}
