using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;

using Android.App;
using Android.Framework;
using Android.Views;
using Android.Widget;


namespace DatabindingSample
{
    public static class AndroidExtensions
    {
        #region Public methods

        public static void ApplyControlError(this Activity activity, string resourceId, ModelStateDictionary modelState)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }
            if (string.IsNullOrEmpty(resourceId))
            {
                throw new ArgumentNullException("resourceId");
            }
            if (modelState == null)
            {
                throw new ArgumentNullException("modelState");
            }

            var errors = modelState[resourceId];
            if (errors.Any() == false)
            {
                return;
            }

            var error = errors[0]; // Take just first one;

            var info = typeof(Resource.Id).GetField(resourceId);
            if (info == null)
            {
                throw new InvalidOperationException(string.Format("Could not find 'Resource.Id.{0}' resource.", resourceId));
            }

            var id = (int)info.GetValue(null);
            var targetView = activity.FindViewById(id);

            if (targetView == null)
            {
                throw new InvalidOperationException(string.Format("Could not find 'Resource.Id.{0}' with '{1}' value.", resourceId, id));
            }

            // Controls with 'SetError' method inherit from EditText control. Nice.

            var editText = targetView as EditText;

            if (editText != null)
            {
                editText.SetError(error, null);
            }
        }

        #endregion

        public static void Bind<T>(this object source, Expression<Func<T>> sourcePropertyExpression, object target, Expression<Func<T>> targetPropertyExpression)
        {
            var _sourcePropertyExpression = sourcePropertyExpression;
            var _targetPropertyExpression = targetPropertyExpression;

            var _topSource = new WeakReference(source);
            var _topTarget = new WeakReference(target);

            var _sourcePropertyName = GetPropertyName(sourcePropertyExpression);
            var _targetPropertyName = GetPropertyName(targetPropertyExpression);
        }

        public static void SetSourceEvent(string eventName)
        {

        }

        private static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                return null;
            }

            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }

            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            }

            return property.Name;
        }

        public static void BindOnFocusChanged(int a, int b, EventHandler<View.FocusChangeEventArgs> handler)
        {
            handler += EditText_FocusChange;

        }

        private static void EditText_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            //Bind(from, to, 

            var element = (View)sender;
            if (element.HasFocus == false)
            {
                //if (element.Id == Resource.Id.EditTextName)
                //{
                //    this.Model.Name = this.NameEditText.Text;
                //}

                //if (element.Id == Resource.Id.EditTextDescription)
                //{
                //    this.Model.Description = this.DescriptionEditText.Text;
                //}
            }
        }
    }
}