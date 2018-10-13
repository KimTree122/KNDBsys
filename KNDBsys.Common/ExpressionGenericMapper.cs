﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KNDBsys.Common
{
    /// <summary>

    /// 生成表达式目录树  泛型缓存

    /// </summary>

    public class ExpressionGenericMapper

    {

        private static object func;



        public static TDestination Map<TSource, TDestination>(TSource source)

        {

            if (func is null)//如果表达式不存在，则走一遍编译过程

            {

                ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "p");

                var memberBindingList = new List<MemberBinding>();//表示绑定的类派生自的基类，这些绑定用于对新创建对象的成员进行初始化

                foreach (var item in typeof(TDestination).GetProperties()) //遍历目标类型的所有属性

                {

                    MemberExpression property = Expression.Property(parameterExpression, typeof(TSource).GetProperty(item.Name));//获取到对应的属性

                    MemberBinding memberBinding = Expression.Bind(item, property);//初始化这个属性

                    memberBindingList.Add(memberBinding);

                }

                foreach (var item in typeof(TDestination).GetFields())

                {

                    MemberExpression property = Expression.Field(parameterExpression, typeof(TSource).GetField(item.Name));//获取到对应的字段

                    MemberBinding memberBinding = Expression.Bind(item, property);//同上

                    memberBindingList.Add(memberBinding);

                }

                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TDestination)), memberBindingList.ToArray());//初始化创建新对象

                Expression<Func<TSource, TDestination>> lambda = Expression.Lambda<Func<TSource, TDestination>>(memberInitExpression, parameterExpression);

                func = lambda.Compile();

            }

            return ((Func<TSource, TDestination>)func)(source); //拼装是一次性的

        }

    }
}
