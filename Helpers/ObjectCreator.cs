using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FiveGears.Core.Helpers
{
    public delegate T ObjectActivator<T>(params object[] args);
    /// <summary>
    /// Object Creator provides ability to dynamically create a new instance of an object from its type.
    /// </summary>
    public static class ObjectCreator
    {
        public static TInterface NewInstance<TInterface>(Type objectType)
            => NewInstance<TInterface>(objectType, null);

        public static TInterface NewInstance<TInterface>(Type objectType, params object[] args)
        {
            ConstructorInfo ctor = objectType.GetConstructors().First();
            ObjectActivator<TInterface> createdActivator = GetActivator<TInterface>(ctor);
            return createdActivator(args);
        }

        public static TInterface NewInstance<TInterface, TObject>(params object[] args)
        {
            ConstructorInfo ctor = typeof(TObject).GetConstructors().First();
            ObjectActivator<TInterface> createdActivator = GetActivator<TInterface>(ctor);
            return createdActivator(args);
        }

        public static TObject NewInstance<TObject>(params object[] args)
        {
            ConstructorInfo ctor = typeof(TObject).GetConstructors().First();
            ObjectActivator<TObject> createdActivator = GetActivator<TObject>(ctor);
            return createdActivator(args);
        }

        private static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
        {
            Type type = ctor.DeclaringType;
            ParameterInfo[] paramsInfo = ctor.GetParameters();

            ParameterExpression param = Expression.Parameter(typeof(object[]), "args");

            Expression[] argsExp = new Expression[paramsInfo.Length];

            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExp = Expression.ArrayIndex(param, index);

                Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            NewExpression newExp = Expression.New(ctor, argsExp);

            LambdaExpression lambda = Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

            ObjectActivator<T> compiled = (ObjectActivator<T>)lambda.Compile();
            return compiled;
        }
    }
}
