using MVVMHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace MVVMHelper
{
    public class LVSortCollection<T>
    {
        public ICommand SortStringCommand { get; private set; }
        public CollectionView View { get => view; }

        private string _sortColumn;
        private ListSortDirection _sortDirection;
        private CollectionView view;

        public LVSortCollection(ICollection<T> itemCollection)
        {
            Init(itemCollection);
        }

        public LVSortCollection(IEnumerable<T> itemCollection)
        {
            Init(itemCollection.ToList());
        }

        private void Init(ICollection<T> itemCollection)
        {
            SortStringCommand = new RelayCommand(Sort);
            view = (CollectionView)CollectionViewSource.GetDefaultView(itemCollection);
        }

        private void Sort(object parameter)
        {
            string column = parameter as string;
            if (_sortColumn == column)
            {
                // Toggle sorting direction
                _sortDirection = _sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            }
            else
            {
                _sortColumn = column;
                _sortDirection = ListSortDirection.Ascending;
            }

            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription(_sortColumn, _sortDirection));
            view.Refresh();
        }
    }
}
