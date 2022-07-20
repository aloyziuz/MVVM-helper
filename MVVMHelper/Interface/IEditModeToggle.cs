using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMHelper.Interface
{
    public interface IEditModeToggle
    {
        bool EditMode { get; set; }
        void AddDescendant(IEditModeToggle descendant);
    }
}
