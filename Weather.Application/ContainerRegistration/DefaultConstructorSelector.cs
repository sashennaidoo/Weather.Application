using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;

namespace Weather.Application.ConsoleApp.ContainerRegistration
{
    public class DefaultConstructorSelector : IConstructorSelector
    {
        
        public ConstructorParameterBinding SelectConstructorBinding(ConstructorParameterBinding[] constructorBindings
                                                                , IEnumerable<Parameter> parameters)
        {
            var defaultConstructor = constructorBindings
                .SingleOrDefault(c => c.TargetConstructor.GetParameters().Length == 0);
            if (defaultConstructor == null)
                //handle the case when there is no default constructor
                throw new InvalidOperationException();
            return defaultConstructor;
        }
    }
}
