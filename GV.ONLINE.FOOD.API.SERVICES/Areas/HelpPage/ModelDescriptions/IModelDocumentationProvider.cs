using System;
using System.Reflection;

namespace GV.ONLINE.FOOD.API.SERVICES.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}