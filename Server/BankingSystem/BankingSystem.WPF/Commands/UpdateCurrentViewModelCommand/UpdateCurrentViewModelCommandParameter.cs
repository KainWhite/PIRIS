using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Core.Enums;

namespace BankingSystem.WPF.Commands.UpdateCurrentViewModelCommand
{
    public class UpdateCurrentViewModelCommandParameter
    {
        public ViewType ViewType { get; set; }
        public object Parameter { get; set; }
    }
}
