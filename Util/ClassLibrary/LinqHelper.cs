using System;
using System.Linq.Expressions;

namespace Util
{
    public static class LinqHelper
    {
        /// <summary>
        /// 创建初始条件为True的表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T,bool>> True<T>()
        {
            return x => true;
        }

        /// <summary>
        /// 创建初始条件为False的表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> False<T>()
        {
            return x => false;
        }

        /// <summary>
        /// 连接表达式与运算
        /// </summary>
        /// <typeparam name="T">参数</typeparam>
        /// <param name="one">原表达式</param>
        /// <param name="another">新的表达式</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> one, Expression<Func<T, bool>> another)
        {
            //创建新参数
            var newParameter = Expression.Parameter(typeof(T), "parameter");

            var parameterReplacer = new ParameterReplacer(newParameter);
            var left = parameterReplacer.Replace(one.Body);
            var right = parameterReplacer.Replace(another.Body);
            var body = Expression.And(left, right);

            return Expression.Lambda<Func<T, bool>>(body, newParameter);
        }

        /// <summary>
        /// 连接表达式或运算
        /// </summary>
        /// <typeparam name="T">参数</typeparam>
        /// <param name="one">原表达式</param>
        /// <param name="another">新表达式</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> one, Expression<Func<T, bool>> another)
        {
            //创建新参数
            var newParameter = Expression.Parameter(typeof(T), "parameter");

            var parameterReplacer = new ParameterReplacer(newParameter);
            var left = parameterReplacer.Replace(one.Body);
            var right = parameterReplacer.Replace(another.Body);
            var body = Expression.Or(left, right);

            return Expression.Lambda<Func<T, bool>>(body, newParameter);
        }

        /// <summary>
        /// 继承ExpressionVisitor类，实现参数替换统一
        /// </summary>
        class ParameterReplacer : ExpressionVisitor
        {
            //新的表达式参数
            private ParameterExpression Parameter { get;set; }

            public ParameterReplacer(ParameterExpression paramExpr)
            {
                Parameter = paramExpr;
            }

            public Expression Replace(Expression expr)
            {
                return this.Visit(expr);
            }

            /// <summary>
            /// 覆盖父方法，返回新的参数
            /// </summary>
            /// <param name="p"></param>
            /// <returns></returns>
            protected override Expression VisitParameter(ParameterExpression p)
            {
                return Parameter;
            }
        }
    }
}
