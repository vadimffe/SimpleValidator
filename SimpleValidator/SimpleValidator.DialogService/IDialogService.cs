using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleValidator.DialogService
{
    public interface IDialogService
    {
        void ShowDialogModal(DialogViewModelBase vm);
    }
}
