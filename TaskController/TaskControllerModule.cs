using Prism.Regions;
using Prism.Modularity;
using TaskController.Views;
using System;

namespace TaskController
{
    public class TaskControllerModule : IModule
    {
        IRegionManager _rm;
        
        public TaskControllerModule(IRegionManager rm)
        {
            _rm = rm;
        }
        public void Initialize()
        {
            _rm.RegisterViewWithRegion("TaskControllerRegion", typeof(TaskControllerView));
        }
    }
}
