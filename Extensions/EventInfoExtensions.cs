using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Starship.Unity.Extensions {
    internal static class EventInfoExtensions {

        public static void DisposeEvents(this object target) {
            foreach (var e in target.GetType().GetEvents()) {
                e.ClearEvents(target);
            }
        }

        public static List<Delegate> ExtractDelegates(this EventInfo eventInfo) {
            var delegates = new List<Delegate>();
            var events = GetEvents(eventInfo);

            if (events != null) {
                foreach (var each in events) {
                    delegates.Add(each);
                    eventInfo.RemoveEventHandler(null, each);
                }
            }

            return delegates;
        }

        internal static List<Delegate> GetEvents(this EventInfo eventInfo, object source = null) {
            var field = eventInfo.DeclaringType.GetField(eventInfo.Name, AllBindings);
            var delegates = field.GetValue(source) as Delegate;

            if (delegates != null) {
                return delegates.GetInvocationList().ToList();
            }

            return new List<Delegate>();
        }
        
        internal static void ClearEvents(this EventInfo eventInfo, object source = null) {
            var events = GetEvents(eventInfo, source);

            if (events != null) {
                foreach (var each in events) {
                    eventInfo.RemoveEventHandler(null, each);
                }
            }
        }

        internal static void InsertEvent(this EventInfo eventInfo, object target, Delegate del) {
            var field = eventInfo.DeclaringType.GetField(eventInfo.Name, AllBindings);
            var delegates = field.GetValue(target) as Delegate;

            if (delegates != null) {
                var invocationList = delegates.GetInvocationList();

                foreach (var ev in invocationList) {
                    eventInfo.RemoveEventHandler(target, ev);
                }

                eventInfo.AddEventHandler(target, del);

                foreach (var ev in invocationList) {
                    eventInfo.AddEventHandler(target, ev);
                }
            }
            else {
                eventInfo.AddEventHandler(target, del);
            }
        }

        internal static void AddDynamicHandler(this EventInfo eventInfo, object target, Action<object[]> callback) {
            var type = eventInfo.EventHandlerType;

            var invokeMethod = type.GetMethod("Invoke");
            var parameters = invokeMethod.GetParameters().Select(parm => Expression.Parameter(parm.ParameterType, parm.Name)).ToList();

            var instance = callback.Target == null ? null : Expression.Constant(callback.Target);
            var converted = parameters.Select(each => Expression.Convert(each, typeof(object))).ToList();
            converted.Insert(0, Expression.Convert(Expression.Constant(eventInfo), typeof(object)));

            var call = Expression.Call(instance, callback.Method, Expression.NewArrayInit(typeof(object), converted.Cast<Expression>()));
            var lambda = Expression.Lambda(type, call, parameters);

            eventInfo.InsertEvent(target, lambda.Compile());
        }

        private const BindingFlags AllBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
    }
}