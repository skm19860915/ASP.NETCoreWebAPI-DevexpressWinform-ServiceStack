using System.Linq;
using FluentValidation;

namespace Xperters.Admin.UI.Common
{
    public abstract class RootViewModel<T> : ViewModel<T>
    {
        internal abstract bool IsDirty { get; }

        internal RootViewModel()
        {
        }

        internal bool IsValid
            => PropertyErrors
                .Select(o => o.Value)
                .All(o => o.Severity != Severity.Error);
    }
}
