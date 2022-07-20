using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMHelper
{
    public interface ISelectableObject
    {
        bool IsSelected { get; set; }
        bool IsSelectable { get; set; }
    }
}
