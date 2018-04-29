using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Starship.Unity.Extensions {
    public static class ExpressionExtensions {

        public static M ToModel<T, M>(this Expression<Func<T, M>> expression, T context) {
            return expression.Compile().Invoke(context);
        }

        public static MemberInfo GetMember(this LambdaExpression expression) {
            if (expression.Body is MemberExpression)
                return expression.Body.As<MemberExpression>().Member;

            if (expression.Body is UnaryExpression) {
                var memberExpression = expression.Body.As<UnaryExpression>().Operand.As<MemberExpression>();
                return memberExpression.Member;
            }

            return null;
        }

        public static string GetFullName(this LambdaExpression expression) {
            return string.Join(".", SplitNames(expression).ToArray());
        }

        private static IEnumerable<string> SplitNames(Expression expression) {

            var memberExpression = expression as MemberExpression;
            var lambdaExpression = expression as LambdaExpression;
            var unaryExpression = expression as UnaryExpression;

            if (memberExpression != null) {
                foreach (var item in SplitNames(memberExpression.Expression)) {
                    yield return item;
                }

                yield return memberExpression.Member.Name;
            }
            else if (lambdaExpression != null) {
                foreach (var item in SplitNames(lambdaExpression.Body)) {
                    yield return item;
                }
            }
            else if (unaryExpression != null) {
                foreach (var item in SplitNames(unaryExpression.Operand)) {
                    yield return item;
                }
            }
        }

        public static Type GetMemberType<T>(this LambdaExpression expression) {

            if (expression.Body is MemberExpression)
                return expression.Body.Type;

            if (expression.Body is UnaryExpression)
                return expression.Body.As<UnaryExpression>().Operand.As<MemberExpression>().Type;

            return null;
        }

        public static MemberInfo GetMember<T>(this Expression<Func<T, object>> function) {
            return function.As<LambdaExpression>().GetMember();
        }

        public static string MemberName<T>(this Expression<Func<T, object>> function) {
            return function.GetMember().Name;
        }

        public static string MemberName<T, V>(this Expression<Func<T, V>> function) {
            return function.GetMember().Name;
        }

        public static string FQN<T>(this Expression<Func<T, object>> function) {
            var fqn = typeof(T).Name + "." + MemberName(function);

            return fqn;
        }
    }
}
