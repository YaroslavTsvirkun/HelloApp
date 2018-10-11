using Hello.BL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.BL.Concrete.IoC
{
    public class Container : IContainer
    {
        private readonly IDictionary<Type, RegisteredObject> registeredObjects;

        public Container()
        {
            registeredObjects = new Dictionary<Type, RegisteredObject>();
        }

        public void Register<TType>() where TType : class
        {
            Register<TType, TType>(false, null);
        }
        public void Register<TType, TConcrete>() where TConcrete : class, TType
        {
            Register<TType, TConcrete>(false, null);
        }
        public void RegisterSingleton<TType>() where TType : class
        {
            RegisterSingleton<TType, TType>();
        }
        public void RegisterSingleton<TType, TConcrete>() where TConcrete : class, TType
        {
            Register<TType, TConcrete>(true, null);
        }
        public void RegisterInstance<TType>(TType instance) where TType : class
        {
            RegisterInstance<TType, TType>(instance);
        }
        public void RegisterInstance<TType, TConcrete>(TConcrete instance) where TConcrete : class, TType
        {
            Register<TType, TConcrete>(true, instance);
        }
        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve));
        }
        public object Resolve(Type type)
        {
            return ResolveObject(type);
        }
        private void Register<TType, TConcrete>(bool isSingleton, TConcrete instance)
        {
            Type type = typeof(TType);
            if (registeredObjects.ContainsKey(type))
                registeredObjects.Remove(type); registeredObjects.Add(type, new RegisteredObject(typeof(TConcrete), isSingleton, instance));
        }
        private object ResolveObject(Type type)
        {
            var registeredObject = registeredObjects[type];
            if (registeredObject.Equals(null))
            {
                throw new ArgumentOutOfRangeException(String.Format("The type {0} has not been registered", type.Name));
            }
            return GetInstance(registeredObject);
        }
        private Object GetInstance(RegisteredObject registeredObject)
        {
            Object instance = registeredObject.SingletonInstance;
            if (instance == null)
            {
                var parameters = ResolveConstructorParameters(registeredObject);
                instance = registeredObject.CreateInstance(parameters.ToArray());
            }
            return instance;
        }
        private IEnumerable<Object> ResolveConstructorParameters(RegisteredObject registeredObject)
        {
            var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
            return constructorInfo.GetParameters().Select(parameter => ResolveObject(parameter.ParameterType));
        }

        private class RegisteredObject
        {
            private readonly Boolean isSinglton;
            public RegisteredObject(Type concreteType, Boolean isSingleton, Object instance)
            {
                this.isSinglton = isSingleton;
                ConcreteType = concreteType;
                SingletonInstance = instance;
            }
            public Type ConcreteType { get; private set; }
            public Object SingletonInstance { get; private set; }
            public Object CreateInstance(params object[] args)
            {
                Object instance = Activator.CreateInstance(ConcreteType, args);
                if (isSinglton) SingletonInstance = instance; return instance;
            }
        }
    }
}