using MVVMHelper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMHelper
{
    public class EditModePropagator : ObservableObject, IEditModeToggle
    {
        private bool _editmode;
        private IList<IEditModeToggle> descendants;
        private Action<bool> onChangeAction;

        public bool EditMode { get => _editmode; set { _editmode = value; RaisePropertyChangedEvent("EditMode"); } }

        public EditModePropagator(Action<bool> onChangeAction)
        {
            this.onChangeAction = onChangeAction;
            this.descendants = new List<IEditModeToggle>();
            this.PropertyChanged += EditModePropagator_PropertyChanged;
        }

        private void EditModePropagator_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            onChangeAction(this.EditMode);
            foreach(var d in this.descendants)
            {
                d.EditMode = this.EditMode;
            }
        }

        public void AddDescendant(IEditModeToggle descendant)
        {
            this.descendants.Add(descendant);
        }
    }
}
