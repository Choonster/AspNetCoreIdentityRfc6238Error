using System;
using System.Reflection;

namespace AspNetCoreIdentityRfc6238Error.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}