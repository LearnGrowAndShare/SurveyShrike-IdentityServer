using FluentValidation.Validators;
using SurveyShrike_IdentityServer.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;


/// <summary>
/// @author Ankit
/// @date - 10/14/2019 11:05:52 AM 
/// </summary>
namespace SurveyShrike_IdentityServer.Application.Infrastructure.Validation
{
    public class UniqueIdentityDBValidator<T> : PropertyValidator where T : class
    {
        private readonly IIdentityServerDbContext _context;
        private readonly bool _isCreate;
        public UniqueIdentityDBValidator(IIdentityServerDbContext context, bool isCreate)
          : base("{PropertyName} must be unique")
        {
            _context = context;
            _isCreate = isCreate;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var editedItem = context.Instance as T;
            var newValue = context.PropertyValue as string;
            if (string.IsNullOrEmpty(newValue))
            {
                return true;
            }
            var property = typeof(T).GetTypeInfo().GetDeclaredProperty(context.PropertyName);

            var idValue = string.Empty;
            if (!_isCreate)
            {
                var idProperty = typeof(T).GetTypeInfo().GetDeclaredProperty("Id");
                idValue = idProperty.GetValue(editedItem).ToString();
            }

            var propertyValue = property.GetValue(editedItem) as string;
                var comparisionObject = _isCreate ? _context.Users.Select(x => x.NormalizedEmail)
                                                          : _context.Users.Where(x => x.Id != idValue)
                                                            .Select(x => x.NormalizedEmail);
                        return !comparisionObject.Any(x => x == propertyValue);
                 
        }
    }
}
