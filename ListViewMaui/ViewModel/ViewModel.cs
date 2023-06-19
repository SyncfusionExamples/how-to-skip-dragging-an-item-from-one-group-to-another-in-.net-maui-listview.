using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewMaui
{
    public class ViewModel 
    {
        #region Fields

        private ObservableCollection<ToDoItem> toDoList;

        #endregion

        #region Constructor

        public ViewModel()
        {
            this.GenerateSource();
            MarkDoneCommand = new Command<object>(MarkItemAsDone);
        }

        #endregion

        #region Property

        public Command<object> MarkDoneCommand
        {
            get; set;
        }

        public ObservableCollection<ToDoItem> ToDoList
        {
            get
            {
                return toDoList;
            }
            set
            {
                this.toDoList = value;
            }
        }

        #endregion

        #region Method

        public void GenerateSource()
        {
            ToDoListRepository todoRepository = new ToDoListRepository();
            toDoList = todoRepository.GetToDoList();
        }

        private void MarkItemAsDone(object obj)
        {
            var item = obj as ToDoItem;
            item.IsDone = !item.IsDone;
        }

        #endregion
    }
}
