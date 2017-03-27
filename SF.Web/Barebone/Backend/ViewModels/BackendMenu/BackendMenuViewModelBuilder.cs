
using SF.Core;
using SF.Core.Abstraction;
using SF.Core.Abstraction.UI.Backends;
using SF.Core.Infrastructure.Modules;
using SF.Web.Modules;
using System.Collections.Generic;
using System.Linq;

namespace SF.Web.Barebone.Backend.ViewModels
{
  public class BackendMenuViewModelBuilder : ViewModelBuilderBase
  {
    public BackendMenuViewModelBuilder( )
      : base()
    {
    }

    public BackendMenuViewModel Build()
    {
      List<BackendMenuGroupViewModel> backendMenuGroupViewModels = new List<BackendMenuGroupViewModel>();

      foreach (IModuleInitializer extension in ExtensionManager.Extensions)
      {
        if (extension is IModuleInitializer)
        {
          if ((extension as IModuleInitializer).BackendMetadata != null && (extension as IModuleInitializer).BackendMetadata.BackendMenuGroups != null)
          {
            foreach (BackendMenuGroup backendMenuGroup in (extension as IModuleInitializer).BackendMetadata.BackendMenuGroups)
            {
              List<BackendMenuItemViewModel> backendMenuItemViewModels = new List<BackendMenuItemViewModel>();

              foreach (BackendMenuItem backendMenuItem in backendMenuGroup.BackendMenuItems)
                backendMenuItemViewModels.Add(new BackendMenuItemViewModelBuilder().Build(backendMenuItem));

              BackendMenuGroupViewModel backendMenuGroupViewModel = this.GetBackendMenuGroup(backendMenuGroupViewModels, backendMenuGroup);

              if (backendMenuGroupViewModel.BackendMenuItems != null)
                backendMenuItemViewModels.AddRange(backendMenuGroupViewModel.BackendMenuItems);

              backendMenuGroupViewModel.BackendMenuItems = backendMenuItemViewModels.OrderBy(bmi => bmi.Position);
            }
          }
        }
      }

      return new BackendMenuViewModel()
      {
        BackendMenuGroups = backendMenuGroupViewModels.OrderBy(bmg => bmg.Position)
      };
    }

    private BackendMenuGroupViewModel GetBackendMenuGroup(List<BackendMenuGroupViewModel> backendMenuGroupViewModels, BackendMenuGroup backendMenuGroup)
    {
      BackendMenuGroupViewModel backendMenuGroupViewModel = backendMenuGroupViewModels.FirstOrDefault(bmg => bmg.Name == backendMenuGroup.Name);

      if (backendMenuGroupViewModel == null)
      {
        backendMenuGroupViewModel = new BackendMenuGroupViewModelBuilder().Build(backendMenuGroup);
        backendMenuGroupViewModels.Add(backendMenuGroupViewModel);
      }

      return backendMenuGroupViewModel;
    }
  }
}