using System;
using System.Collections.Generic;
using AotAssemblyScan;
using Monitoring.Views.Abstractions;

namespace Monitoring.Common.Helpers;

public static partial class AssemblyHelper
{
    [AssemblyScan]
    [IsAssignableTo<IView>]
    [IsInterface(false)]
    [IsAbstract(false)]
    public static partial IReadOnlyList<Type> GetViewTypes();
}
