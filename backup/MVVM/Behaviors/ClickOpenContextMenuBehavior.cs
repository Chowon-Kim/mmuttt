using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samsung.AppCommon.MVVM.Behaviors
{
    /// <summary>
    /// Show the context menu of AssociatedObject when AssociatedObject is left-clicked
    /// </summary>
    public class ClickOpenContextMenuBehavior : ClickCommandBehavior
    {
        public ClickOpenContextMenuBehavior()
        {
            Command = new RelayCommand<object>(ExecuteClickCommand);
        }

        private void ExecuteClickCommand(object obj)
        {
            if (AssociatedObject.ContextMenu != null)
            {
                // ContextMenuService.IsEnabled="False"이면, ContextMenu의 DataContext가 ContextMenu owner의 DataContext가 아니고, null임.
                // 그래서 명시적으로 set 해줌. ContextMenu owner의 DataContext가 바뀌면 오동작 가능성이 있음.
                if (AssociatedObject.ContextMenu.DataContext == null)
                    AssociatedObject.ContextMenu.DataContext = AssociatedObject.DataContext;
                AssociatedObject.ContextMenu.IsOpen = true;
            }
        }
    }

}
