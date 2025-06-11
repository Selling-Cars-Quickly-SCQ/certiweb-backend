using CertiWeb.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CertiWeb.API.Shared.Infrastructure.Interfaces.ASP.Configuration;

/// <summary>
///     This class is used to replace the default route naming convention with a kebab-case naming convention.
/// </summary>
public class KebabCaseRouteNamingConvention : IControllerModelConvention
{
    /// <summary>
    ///     This method applies the kebab-case naming convention to the controller.
    /// </summary>
    /// <param name="controller">The <see cref="ControllerModel" /></param>
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);

        foreach (var selector in controller.Actions.SelectMany(a => a.Selectors))
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);
    }

    /// <summary>
    ///     This method replaces the default controller template with a kebab-case template.
    /// </summary>
    /// <param name="selector">The <see cref="SelectorModel" /></param>
    /// <param name="name">string containing the name</param>
    /// <returns></returns>
    private static AttributeRouteModel? ReplaceControllerTemplate(SelectorModel selector, string name)
    {
        return selector.AttributeRouteModel != null
            ? new AttributeRouteModel
            {
                Template = selector.AttributeRouteModel.Template?.Replace("[controller]", name.ToKebabCase())
            }
            : null;
    }
}