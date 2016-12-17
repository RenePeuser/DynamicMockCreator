using System;
using System.Threading.Tasks;
using System.Windows;
using Extensions;
using ObjectCreator.Extensions;
using ObjectCreator.Helper;

namespace ObjectCreator.MarkupExtensions
{
    public class DesignTimeAttachedProperties
    {
        public static readonly DependencyProperty DesignTimeTypeProperty = DependencyProperty.RegisterAttached(
            "DesignTimeType", typeof(Type), typeof(DesignTimeAttachedProperties), new PropertyMetadata(default(Type), PropertyChangedCallbackAsync));

        private static readonly RandomDefaultData RandomDefaultData = new RandomDefaultData();

        private static async void PropertyChangedCallbackAsync(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            //if (!DesignerProperties.GetIsInDesignMode(dependencyObject))
            //{
            //    return;
            //}

            var frameworkElement = dependencyObject.Cast<FrameworkElement>();
            var newType = dependencyPropertyChangedEventArgs.NewValue.Cast<Type>();

            var taskResult = await Task.Run(() => newType.Create(RandomDefaultData, ObjectCreatorMode.WithProperties));
            frameworkElement.DataContext = taskResult;
        }

        public static void SetDesignTimeType(DependencyObject element, Type value)
        {
            element.SetValue(DesignTimeTypeProperty, value);
        }

        public static Type GetDesignTimeType(DependencyObject element)
        {
            return (Type)element.GetValue(DesignTimeTypeProperty);
        }
    }
}
