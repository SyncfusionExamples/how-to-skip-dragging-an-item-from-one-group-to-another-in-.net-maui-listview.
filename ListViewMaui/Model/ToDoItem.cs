using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewMaui
{
    public class ToDoItem : INotifyPropertyChanged
    {
        #region Fields

        private string name;
        private string categoryName;
        private bool isDone;

        #endregion

        #region Constructor

        public ToDoItem()
        {

        }

        #endregion

        #region Properties

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                OnPropertyChanged("CategoryName");
            }
        }

        public bool IsDone
        {
            get { return isDone; }
            set
            {
                if (isDone != value)
                {
                    isDone = value;
                    OnPropertyChanged("IsDone");
                }
            }
        }

        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
